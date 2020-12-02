const dataContainer = document.querySelector('#data')
const doneContainer = document.querySelector('#done')

function createPost(bug, container)
{
    const card = document.createElement('div')
    const header = document.createElement('div')
    const h2 = document.createElement('h2')
    const headerButton = document.createElement('button')
    const collapse = document.createElement('div')
    const cardbody = document.createElement('div')
    const fix = document.createElement('button')
    card.classList.add('card')

    header.classList.add('card-header')
    header.id = `post-${bug.id}`

    h2.classList.add('mb-0')

    headerButton.classList.add('btn')
    headerButton.classList.add('btn-link')
    headerButton.classList.add('btn-block')
    headerButton.classList.add('text-left')
    headerButton.addEventListener('click', () => collapse.classList.toggle('show'))
    headerButton.innerHTML = `#${bug.id}`


    const test = document.createElement('button')
    test.classList.add('btn')
    test.classList.add('btn-link')
    test.classList.add('btn-block')

    h2.appendChild(headerButton)
    header.appendChild(h2)
    card.appendChild(header)

    collapse.classList.add('collapse')
    collapse.id = `collapse-${bug.id}`

    cardbody.classList.add('card-body')
    cardbody.innerHTML = `<h4>${bug.device} - ${bug.ram}MB</h4> <hr> ${bug.bug}`
    
    fix.classList.add('btn')
    fix.classList.add('btn-info')
    fix.classList.add('btn-block')
    fix.innerHTML = 'Set as fixed'
    fix.addEventListener('click', () => fixBug(bug.id))
    
    if(!bug.fixed)
        collapse.appendChild(fix) 

    collapse.appendChild(cardbody)
    card.appendChild(collapse)
  

    container.appendChild(card)
    
    return [bug.id]
}

function getAll()
{
    fetch('bug/all', {method : 'post'})
    .then(res => res.json())
    .then(generateBugReport)
    .catch(err => console.log('Error while trying to get bugs - ' + err))
}

function generateBugReport(res)
{
    dataContainer.innerHTML = ''
    doneContainer.innerHTML = ''
    let data = sortOutByDone(res)
    data[0].map(bug => createPost(bug, dataContainer))
    if(data[0].length == 0)
        dataContainer.innerHTML = '<h4>There is no new issue</h4>'

    data[1].map(bug => createPost(bug, doneContainer))
}

function sortOutByDone(arr)
{
    let tmp = [[],[]];
    arr.forEach(elem => tmp[elem.fixed ? 1 : 0].push(elem));
    return tmp;
}

function fixBug(id)
{
    let pass = prompt("Enter the master password");
    let hashed = cyrb53(pass);

    fetch(`bug/fix/${id}`, {method : 'post', headers: {'Content-Type': 'application/json'}, body : JSON.stringify({'pass' : hashed})})
    .then((res) =>
    {
        if(res.status == 200)
            getAll();
        else
            alert('Bad password');
    })
    .catch(err => console.log('Cant fix the update - ' + err))
}

function cyrb53(str) 
{
    let h1 = 0xdeadbeef ^ 14, h2 = 0x41c6ce57 ^ 14;
    for (let i = 0, ch; i < str.length; i++) 
    {
        ch = str.charCodeAt(i);
        h1 = Math.imul(h1 ^ ch, 2654435761);
        h2 = Math.imul(h2 ^ ch, 1597334677);
    }
    h1 = Math.imul(h1 ^ (h1>>>16), 2246822507) ^ Math.imul(h2 ^ (h2>>>13), 3266489909);
    h2 = Math.imul(h2 ^ (h2>>>16), 2246822507) ^ Math.imul(h1 ^ (h1>>>13), 3266489909);
    return 4294967296 * (2097151 & h2) + (h1>>>0);
}

getAll()

