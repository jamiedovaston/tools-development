const express = require('express');

const app = express();
const port = 3000;

const functions = require('./Functions/OpenWeatherMap');

app.get('/weather', async (req, res) => {
    try {
        const data = await functions.getData(req.query.lat, req.query.lon);

        res.json({
            id: data.current.weather[0].id,
            main: data.current.weather[0].main,
            description: data.current.weather[0].description,
            icon: data.current.weather[0].icon
        });
    } catch (error) {
        res.status(500).json({ error: 'Failed to fetch weather data' });
    }
});

app.get('/unix-time', async (req, res) => {
   try {
       const data = await functions.getData(req.query.lat, req.query.lon);

       res.json({
           timezone_offset: data.timezone_offset,
           unix_time: data.current.dt,
           unix_sunrise: data.current.sunrise,
           unix_sunset: data.current.sunset,
       });
   } catch (error) {
       res.status(500).json({error: 'Failed to fetch time data'});
   }
});

app.get('/geo-location', async (req, res) => {
    try {
        const data = await functions.getGeographicalData(req.query.location);

        res.json({
            name: data[0].name,
            lat: data[0].lat,
            lon: data[0].lon,
            country: data[0].country
        });
    } catch (error) {
        res.status(500).json({error: 'Failed to fetch location data'})
    }
})

app.get('/', (req, res) => {
    res.send('Welcome to the weather server!');
});

app.listen(port, () => {
    console.log(`App listening at http://localhost:${port}`);
});