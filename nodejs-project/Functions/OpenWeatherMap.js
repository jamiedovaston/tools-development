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

        return response.data; // Here you would map it to your Models.Weather structure if needed
    } catch (error) {
        console.error(error); // Log the error for debugging
        return {}; // Return an empty object or appropriate fallback
    }
}

module.exports = { getData };