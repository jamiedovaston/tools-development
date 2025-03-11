const express = require('express');
const app = express();
const port = 3000;

const { DateTime } = require('luxon');

app.get('/', (req, res) => {
    const utcTime = DateTime.utc().toISO();
    // const utcTime = new Date(Date.UTC(2018, 11, 1, 0, 0, 0)).toISOString();
    res.send('Hello World! ' + utcTime);
});

app.listen(port, () => {
    console.log(`App listening at http://localhost:${port}`);
});