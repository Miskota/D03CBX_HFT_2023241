fetch('http://localhost:59244/record')
    .then(x => x.json())
    .then(y => console.log(y));