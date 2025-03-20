const express = require('express');
const { graphqlHTTP } = require('graphql-http');

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
           unix_time: data.current.dt,
           unix_sunrise: data.current.sunrise,
           unix_sunset: data.current.sunset,
       });
   } catch (error) {
       res.status(500).json({error: 'Failed to fetch time data'});
   }
});

app.listen(port, () => {
    console.log(`App listening at http://localhost:${port}`);
});