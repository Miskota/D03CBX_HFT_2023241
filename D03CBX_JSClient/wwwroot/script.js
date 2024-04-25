let records = [];
let connection = null;

let recordIDToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:59244/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("RecordCreated", (user, message) => {
        getdata();
    });

    connection.on("RecordDeleted", (user, message) => {
        getdata();
    });

    connection.on("RecordUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected");
        document.getElementById('updateformdiv').style.display = 'none';
        
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
            //console.log(records);
            display();
        });
}
function display() {
    document.getElementById('resultarea').innerHTML = "";
    records.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.recordID + "</td><td>"
            + t.title + "</td><td>" +
             `<button type="button" onclick="remove(${t.recordID})">Delete</button>` +
             `<button type="button" onclick="showupdate(${t.recordID})">Update</button>`
            + "</td></tr>";
    });
}

function showupdate(id) {
    document.getElementById('recordTitleUpdate').value = records.find(t => t['recordID'] == id)['title'];
    document.getElementById('recordPlaysUpdate').value = records.find(t => t['recordID'] == id)['plays'];
    document.getElementById('recordRatingUpdate').value = records.find(t => t['recordID'] == id)['rating'];
    document.getElementById('recordDurationUpdate').value = records.find(t => t['recordID'] == id)['duration'];
    document.getElementById('recordGenreUpdate').value = records.find(t => t['recordID'] == id)['genre'];
    document.getElementById('updateformdiv').style.display = 'flex';
    document.getElementById('updateformdiv').style.flexDirection = 'column';
    recordIDToUpdate = id;
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

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let recordTitle = document.getElementById('recordTitleUpdate').value.trim();
    let recordGenre = parseInt(document.getElementById('recordGenreUpdate').value.trim(), 10);
    let recordPlays = parseInt(document.getElementById('recordPlaysUpdate').value.trim(), 10);
    let recordRating = parseFloat(document.getElementById('recordRatingUpdate').value.trim());
    let recordDuration = parseInt(document.getElementById('recordDurationUpdate').value.trim(), 10);
    //let recordAlbumID = parseInt(document.getElementById('recordAlbumIDUpdate').value.trim(), 10);

    let jsonData = {
        recordID: recordIDToUpdate,
        title: recordTitle,
        duration: recordDuration,
        genre: recordGenre,
        plays: recordPlays,
        rating: recordRating
    }
    console.log(JSON.stringify(jsonData));

    fetch('http://localhost:59244/record', {
        method: 'PUT',
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