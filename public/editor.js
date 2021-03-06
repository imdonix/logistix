const padding = 50
const size = 30
const canvas = document.querySelector('#levels')

const id = document.querySelector('#id')
const leveltb = document.querySelector('#level')
const unlock = document.querySelector('#unlock')
const name = document.querySelector('#name')
const wood = document.querySelector('#wood')
const iron = document.querySelector('#iron')
const boxes = document.querySelector('#boxes')
const lost = document.querySelector('#lost')
const premium = document.querySelector('#premium')
const btnAdd = document.querySelector('#add')
const btnRemove = document.querySelector('#remove')
const btnReload = document.querySelector('#reload')
const btnUpload = document.querySelector('#upload')
const unlockcount = document.querySelector('#unlockcount')
const boxcount = document.querySelector('#boxcount')
const btnnew = document.querySelector('#new');

let map = null
let selected = null
let scrollState = 0

id.addEventListener('change', onChange)
btnAdd.addEventListener('click', onAdd)
btnRemove.addEventListener('click', onRemove)
btnReload.addEventListener('click', reload)
btnUpload.addEventListener('click', onSubmit);
btnnew.addEventListener('click', (e) => init([]))
canvas.addEventListener('wheel', scroll);

unlock.addEventListener('input', (_) => arrayChange(unlock, unlockcount))
boxes.addEventListener('input', (_) => arrayChange(boxes, boxcount))

reload();

function reload(reset)
{
    if(map != null)
        if (!confirm("Any unsaved progress will be lost"))
            return;

    map = null;
    update();
    fetch('/levels')
    .then(res => res.json())
    .catch(err => {console.log("Data cant be loaded from /levels"); init([])}).then(data => init(data))
}

function onChange()
{
    if(map == null) return;
    let resoult = findRecord(id.value)

    if(resoult)
        loadToEditorRecord(resoult.record, resoult.level);
    else
        createNew();
}

function onAdd()
{
    if(selected == null) return;

    selected.unlocks = makeArray(unlock.value);
    selected.name = name.value;
    selected.reward_wood = wood.value;
    selected.reward_iron = iron.value;
    selected.boxes = makeArray(boxes.value),
    selected.maxlost = lost.value;
    selected.premium = premium.checked;

    if(selected.prototype)
    {   
        delete selected.prototype;
        let index = leveltb.value;
        if(map.length <= index)
        {
            const tmp = {'color': randomColor(), 'levels' : []};
            tmp.levels.push(selected);
            map.push(tmp);
        }
        else
            map[index].levels.push(selected);
    }
    onChange();
    update();
}

function onRemove()
{
    if(map == null) return;
    let resoult = findRecord(id.value)
    if(resoult)
    {
        const index = resoult.container.levels.indexOf(resoult.record);
        if (index > -1) 
            resoult.container.levels.splice(index, 1);
    }
    onChange();
    update();
}

function onSubmit()
{
    let pass = prompt("Enter the master password");
    let hashed = cyrb53(pass);
    let post = {'password' : hashed, 'data' : map};

    fetch('/levels', {method: 'POST', headers: {'Content-Type': 'application/json'} , body: JSON.stringify(post),})
    .then(res =>
        {
            if(res.status === 200)
                alert("Levelmap is updated");
            else if(res.status === 403)
                alert("Bad password");
            else
                alert("Something went wrong!");
        })
    .catch((error) => {console.error('Error:', error);});
}

function createNew()
{
    newRecord = 
    {
        'id': id.value,
        'unlocks': "",
        'name': "?",
        'reward_wood': 0,
        'reward_iron': 0,
        'boxes': "",
        'maxlost': 0,
        'premium': false,
        'prototype': true
    }

    loadToEditorRecord(newRecord, 0);
}

function arrayChange(input, count)
{
    count.innerHTML = makeArray(input.value).length.toString()
}

function loadToEditorRecord(record, level)
{
    selected = record;
    leveltb.value = level;

    unlock.value = selected.unlocks;
    arrayChange(unlock, unlockcount)
    name.value = selected.name;
    wood.value = selected.reward_wood;
    iron.value = selected.reward_iron;
    boxes.value = selected.boxes,
    arrayChange(boxes, boxcount)
    lost.value = selected.maxlost;
    premium.checked = selected.premium;
    
    if(!selected.prototype)
    {
        btnAdd.innerHTML = "Update";
        btnRemove.innerHTML = "Remove";
        btnRemove.classList.toggle('hidden', false);
    }
    else
    {
        btnAdd.innerHTML = "Add";
        btnRemove.classList.toggle('hidden', true);
    }
}

function init(obj)
{
    map = obj;
    update();
}

function makeArray(inp)
{
    return inp.toString().split(',').filter(f => f);
}

function findRecord(id)
{
    let c = 0;
    for (const level of map)
    {
        for(const record of level.levels)
            if(record.id == id)
                return {'container': level, 'record' : record, 'level' : c};
        c++
    }
               
    return null;
}

function update()
{
    if(map == null) return;
    let ctx = canvas.getContext('2d');
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    ctx.font = "12px Verdana";
    let center = canvas.clientWidth / 2;
    let y = 0;

    let prev = []
    map.forEach(level => 
    {
        let posY = padding + scrollState + (y * (size + size/2))
        let x = 0

        ctx.fillStyle = "black"
        ctx.fillText(y.toString(), (size + size/2), posY + 18)

        let temp = [];
        level.levels.forEach(record =>
        {
            let back = (level.levels.length / 2) * (size + size/2)
            let posX = center + (x * (size + size/2)) - back
            let color = toColor(level.color)

            ctx.fillStyle = color;
            ctx.fillRect(posX, posY, size, size)
            ctx.fillStyle = invertColor(color)
            ctx.fillText(record.id, posX + 11 - (record.id.toString().length), posY + 19)
            record.unlocks.forEach(i =>
                {
                    let to = prev.find(p => p[0] == i);
                    console.log(to)
                    if(to)
                    {
                        ctx.beginPath();
                        ctx.moveTo(posX+size/2, posY);
                        ctx.lineTo(to[1]+size/2, to[2]+size);
                        ctx.stroke();
                    }
                })
            temp.push([record.id,posX,posY])
            x++
        })
        prev = temp
        y++
    });
}

function invertColor(hex) 
{
    if (hex.indexOf('#') === 0) hex = hex.slice(1);
    if (hex.length === 3) hex = hex[0] + hex[0] + hex[1] + hex[1] + hex[2] + hex[2];
    if (hex.length !== 6)  throw new Error('Invalid HEX color.');
    let r = (255 - parseInt(hex.slice(0, 2), 16)).toString(16),
        g = (255 - parseInt(hex.slice(2, 4), 16)).toString(16),
        b = (255 - parseInt(hex.slice(4, 6), 16)).toString(16);
    return '#' + padZero(r) + padZero(g) + padZero(b);
}

function padZero(str, len) 
{
    len = len || 2;
    let zeros = new Array(len).join('0');
    return (zeros + str).slice(-len);
}

function randomColor()
{
    return '#' + Math.floor(Math.random()*16777215).toString(16);
}

function toColor(color)
{  
    while(color.length < 6) color += '0'
    if(color.indexOf('#') === 0) return color
    return '#' + color
}

function scroll(event)
{
    if(map == null) return;

    event.preventDefault();
    scrollState += -event.deltaY;
    if(scrollState > 0) scrollState = 0
    update();
}

function cyrb53(str) 
{
    let h1 = 0xdeadbeef ^ 14, h2 = 0x41c6ce57 ^ 14;
    for (let i = 0, ch; i < str.length; i++) {
        ch = str.charCodeAt(i);
        h1 = Math.imul(h1 ^ ch, 2654435761);
        h2 = Math.imul(h2 ^ ch, 1597334677);
    }
    h1 = Math.imul(h1 ^ (h1>>>16), 2246822507) ^ Math.imul(h2 ^ (h2>>>13), 3266489909);
    h2 = Math.imul(h2 ^ (h2>>>16), 2246822507) ^ Math.imul(h1 ^ (h1>>>13), 3266489909);
    return 4294967296 * (2097151 & h2) + (h1>>>0);
};