let records = [];
const connection;

getdata();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:59244/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
} 
async function getdata() {
    await fetch('http://localhost:59244/record')
        .then(x => x.json())
        .then(y => {
            records = y;
            console.log(records);
            display();
        });
}
function display() {
    document.getElementById('resultarea').innerHTML = "";
    records.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.recordID + "</td><td>"
            + t.title + "</td><td>"
            + `<button type="button" onclick="remove(${t.recordID})">Delete</button>`
            + "</td></tr>";
    });
}

function remove(id) {
    fetch('http://localhost:59244/record/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}


//function getGenreString(genreNumber) {
//    switch (genreNumber) {
//        case "1":
//            return "Classic";
//        case "2":
//            return "Jazz";
//        case "3":
//            return "Country";
//        case "4":
//            return "Pop";
//        case "5":
//            return "Rock";
//        case "6":
//            return "Metal";
//        case "7":
//            return "Electro";
//        case "8":
//            return "Punk";
//        case "9":
//            return "Folk";
//        case "10":
//            return "Disco";
//        case "11":
//            return "Funk";
//        case "12":
//            return "Synth";
//        case "13":
//            return "HipHop";
//        default:
//            return "";
//    }
//}


function create() {
    //let recordTitle = document.getElementById('recordTitleInput').value;
    //let recordGenre = document.getElementById('recordGenre').value;
    //let recordPlays = document.getElementById('recordPlaysInput').value;
    //let recordRating = document.getElementById('recordRatingInput').value;
    //let recordDuration = document.getElementById('recordDurationInput').value;
    //let recordAlbumID = document.getElementById('recordAlbumIDInput').value;

    let recordTitle = document.getElementById('recordTitleInput').value.trim();
    let recordGenre = parseInt(document.getElementById('recordGenre').value.trim(), 10);
    let recordPlays = parseInt(document.getElementById('recordPlaysInput').value.trim(), 10);
    let recordRating = parseFloat(document.getElementById('recordRatingInput').value.trim());
    let recordDuration = parseInt(document.getElementById('recordDurationInput').value.trim(), 10);
    let recordAlbumID = parseInt(document.getElementById('recordAlbumIDInput').value.trim(), 10);

    console.log(recordGenre);
    //console.log(getGenreString(recordGenre));

    let jsonData = {
        title: recordTitle,
        duration: recordDuration,
        genre: recordGenre,
        plays: recordPlays,
        rating: recordRating
    }

    console.log(JSON.stringify(jsonData));

    fetch('http://localhost:59244/record', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(jsonData)
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}