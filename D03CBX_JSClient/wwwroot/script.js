function displayTopAlbumCount() {
    fetch('http://localhost:59244/noncrud/Top10AlbumCount')
        .then(response => response.json())
        .then(data => {
            const writerList = document.getElementById('writerList');
            writerList.innerHTML = '';
            data.forEach(writer => {
                const writerItem = document.createElement('li');
                writerItem.textContent = `${writer.writerName}`;
                writerList.appendChild(writerItem);
            });
        })
        .catch(error => console.error('Error:', error));
}

let records = [];
let albums = [];
let writers = [];
let connection = null;

let recordIDToUpdate = -1;
let albumIDToUpdate = -1;
let writerIDToUpdate = -1;

getdata();
getdataAlbum();
getdataWriter();
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


    connection.on("AlbumCreated", (user, message) => {
        getdata();
    });
    connection.on("AlbumDeleted", (user, message) => {
        getdata();
    });
    connection.on("AlbumUpdated", (user, message) => {
        getdata();
    });


    connection.on("WriterCreated", (user, message) => {
        getdata();
    });
    connection.on("WriterDeleted", (user, message) => {
        getdata();
    });
    connection.on("WriterUpdated", (user, message) => {
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
        document.getElementById('updateformdivAlbum').style.display = 'none';
        document.getElementById('updateformdivWriter').style.display = 'none';
        
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
async function getdataAlbum() {
    await fetch('http://localhost:59244/album')
        .then(x => x.json())
        .then(y => {
            albums = y;
            console.log(albums);
            displayAlbum();
        });
}
async function getdataWriter() {
    await fetch('http://localhost:59244/writer')
        .then(x => x.json())
        .then(y => {
            writers = y;
            console.log(writers);
            displayWriter();
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
function displayAlbum() {
    document.getElementById('resultareaAlbum').innerHTML = "";
    albums.forEach(t => {
        document.getElementById('resultareaAlbum').innerHTML +=
            "<tr><td>" + t.albumID + "</td><td>"
            + t.albumName + "</td><td>" +
            `<button type="button" onclick="removeAlbum(${t.albumID})">Delete</button>` +
            `<button type="button" onclick="showupdateAlbum(${t.albumID})">Update</button>`
            + "</td></tr>";
    });
}
function displayWriter() {
    document.getElementById('resultareaWriter').innerHTML = "";
    writers.forEach(t => {
        document.getElementById('resultareaWriter').innerHTML +=
            "<tr><td>" + t.writerID + "</td><td>"
            + t.writerName + "</td><td>" +
            `<button type="button" onclick="removeWriter(${t.writerID})">Delete</button>` +
            `<button type="button" onclick="showupdateWriter(${t.writerID})">Update</button>`
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
function showupdateAlbum(id) {
    document.getElementById('albumNameUpdate').value = albums.find(t => t['albumID'] == id)['albumName'];
    document.getElementById('albumReleaseUpdate').value = albums.find(t => t['albumID'] == id)['releaseYear'];
    document.getElementById('albumGenreUpdate').value = albums.find(t => t['albumID'] == id)['genre'];
    document.getElementById('updateformdivAlbum').style.display = 'flex';
    document.getElementById('updateformdivAlbum').style.flexDirection = 'column';
    albumIDToUpdate = id;
}

function showupdateWriter(id) {
    document.getElementById('writerNameUpdate').value = writers.find(t => t['writerID'] == id)['writerName'];
    document.getElementById('writerYearUpdate').value = writers.find(t => t['writerID'] == id)['yearOfBirth'];
    document.getElementById('updateformdivWriter').style.display = 'flex';
    document.getElementById('updateformdivWriter').style.flexDirection = 'column';
    writerIDToUpdate = id;
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
function removeAlbum(id) {
    fetch('http://localhost:59244/album/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdataAlbum();
        })
        .catch((error) => { console.error('Error:', error); });
}
function removeWriter(id) {
    fetch('http://localhost:59244/writer/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdataWriter();
        })
        .catch((error) => { console.error('Error:', error); });
}



function create() {
    
    let recordTitle = document.getElementById('recordTitleInput').value.trim();
    let recordGenre = parseInt(document.getElementById('recordGenre').value.trim(), 10);
    let recordPlays = parseInt(document.getElementById('recordPlaysInput').value.trim(), 10);
    let recordRating = parseFloat(document.getElementById('recordRatingInput').value.trim());
    let recordDuration = parseInt(document.getElementById('recordDurationInput').value.trim(), 10);
    let recordAlbumID = parseInt(document.getElementById('recordAlbumIDInput').value.trim(), 10);

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

function createAlbum() {

    let albumName = document.getElementById('albumNameInput').value.trim();
    let albumGenre = parseInt(document.getElementById('albumGenre').value.trim(), 10);
    let albumYear = parseInt(document.getElementById('albumReleaseInput').value.trim(), 10);

    let jsonDataAlbum = {
        albumName: albumName,
        genre: albumGenre,
        releaseYear: albumYear
    }

    console.log(JSON.stringify(jsonDataAlbum));

    fetch('http://localhost:59244/album', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(jsonDataAlbum)
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdataAlbum();
        })
        .catch((error) => { console.error('Error:', error); });
}
function createWriter() {

    let writerName = document.getElementById('writerNameInput').value.trim();
    let writerYear = parseInt(document.getElementById('writerYearInput').value.trim(), 10);

    let jsonDataWriter = {
        writerName: writerName,
        yearOfBirth: writerYear
    }

    console.log(JSON.stringify(jsonDataWriter));

    fetch('http://localhost:59244/writer', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(jsonDataWriter)
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdataWriter();
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
function updateAlbum() {
    document.getElementById('updateformdivAlbum').style.display = 'none';
    let albumName = document.getElementById('albumNameUpdate').value.trim();
    let albumGenre = parseInt(document.getElementById('albumGenreUpdate').value.trim(), 10);
    let albumYear = parseInt(document.getElementById('albumReleaseUpdate').value.trim(), 10);

    let jsonDataAlbum = {
        albumName: albumName,
        genre: albumGenre,
        releaseYear: albumYear
    }

    console.log(JSON.stringify(jsonData));

    fetch('http://localhost:59244/album', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(jsonDataAlbum)
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function updateWriter() {
    document.getElementById('updateformdivWriter').style.display = 'none';
    let writerName = document.getElementById('writerNameUpdate').value.trim();
    let writerYear = parseInt(document.getElementById('writerYearUpdate').value.trim(), 10);

    let jsonDataWriter = {
        name: writerName,
        yearOfBirth: writerYear
    }

    console.log(JSON.stringify(jsonData));

    fetch('http://localhost:59244/writer', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(jsonDataWriter)
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

