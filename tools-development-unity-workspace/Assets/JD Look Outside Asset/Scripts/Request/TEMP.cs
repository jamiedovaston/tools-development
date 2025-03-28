using UnityEngine;
using JD.LookOutside;

namespace JD.Temp
{
    public class TEMP : MonoBehaviour
    {
        public LocationEasySO m_FrmwrkLocation;

        private void Start()
        {
            LocationServices.SetLocation(m_FrmwrkLocation.Location, async ()=>
            {
                LookOutside.Models.Weather weather = await WeatherServices.GetWeather();
                LookOutside.Models.Time time = await TimeServices.GetTime();

                Debug.Log(weather.description);
                Debug.Log(time.time);
            });
        }
    }
}
