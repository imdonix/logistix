const report = document.querySelector('#report')
const thank = document.querySelector('#thank')
const main = document.querySelector('#main')

const device = document.querySelector('#device')
const ram = document.querySelector('#ram')
const player = document.querySelector('#player')
const bug = document.querySelector('#bug')
const size = document.querySelector('#size')
const send = document.querySelector('#send')

const url = new URLSearchParams(window.location.search);
const min = 20
const max = 200

function init()
{
    send.addEventListener('click', sendReport)
    bug.addEventListener('keyup', checkBugReportSize)

    const player = url.get('player');
    if(player)
    {
        report.classList.toggle('hidden', false);
        main.classList.toggle('hidden', true);
        loadURLData();
    }
    else
    {
        report.classList.toggle('hidden', true);
        main.classList.toggle('hidden', false);
    }
}

function checkBugReportSize()
{
    const textSize = bug.value.length
    if(textSize < min)
    {
        size.innerHTML = `${min - textSize} more`
        size.classList.toggle('badge-secondary', true)
        size.classList.toggle('badge-warning', false)
        size.classList.toggle('badge-success', false)
    }
    else if(textSize > max)
    {
        size.innerHTML = `to long`
        size.classList.toggle('badge-warning', true)
        size.classList.toggle('badge-secondary', false)
        size.classList.toggle('badge-success', false)
    }
    else
    {
        size.innerHTML = `${textSize}`
        size.classList.toggle('badge-success', true)
        size.classList.toggle('badge-warning', false)
        size.classList.toggle('badge-secondary', false)
    }
}

function loadURLData()
{
    device.value = url.get('device')
    ram.value = url.get('ram')
    player.value = url.get('player')
}

function getData()
{
    return {
        player: player.value,
        device: device.value ,
        ram: ram.value,
        send: bug.value,
    }
}

function swichToThank()
{
    report.classList.toggle('hidden', true);
    thank.classList.toggle('hidden', false);
}

function sendReport()
{

    fetch('/bug', 
        {
            method: 'POST', 
            body: JSON.stringify(getData()),
            headers: {'Content-Type': 'application/json'}
        }).then(res => console.log(`Bug sent (${res.status})`))
    swichToThank();
}


init();