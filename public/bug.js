const report = document.querySelector('#report')
const thank = document.querySelector('#thank')
const main = document.querySelector('#main')

const device = document.querySelector('#device')
const ram = document.querySelector('#ram')
const player = document.querySelector('#player')
const bug = document.querySelector('#bug')
const send = document.querySelector('#send')

const url = new URLSearchParams(window.location.search);
const min = 20
const max = 200

function init()
{
    send.addEventListener('click', sendReport)
    report.classList.toggle('hidden', false);
    loadURLData();
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
        device: device.value,
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