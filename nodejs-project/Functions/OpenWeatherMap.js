require('dotenv').config();
const axios = require('axios');

async function getData(lat, lon) {
    try {
        const response = await axios.get('https://api.openweathermap.org/data/3.0/onecall', {
            params: {
                lat: lat,
                lon: lon,
                exclude: 'minutely,hourly,daily',
                appid: process.env.APPID
            }
        });

        console.debug(response.data);

        return response.data;
    } catch (error) {
        console.error(error);
        return {};
    }
}

async function getGeographicalData(location)
{
    try {
        const response = await axios.get('http://api.openweathermap.org/geo/1.0/direct', {
            params: {
                q: location,
                limit: 1,
                appid: process.env.APPID
            }
        });

        return response.data;
    } catch (error) {
        console.error(error);
        return {};
    }
}

module.exports = { getData, getGeographicalData };