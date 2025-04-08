using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

namespace JD.LookOutside
{
    public static class WeatherServices
    {
        public static bool IsNull(this LocationEasy frmwrkData)
        {
            LocationAdvanced data = frmwrkData as LocationAdvanced;

            if (data == null) return true;

            if (data.m_Longitude == float.NaN || data.m_Latitude == float.NaN) return true;
            
            return false;
        }

        public async static Task<Models.Weather> GetWeather()
        {
            DownloadHandlerBuffer downloadHandler = new DownloadHandlerBuffer();
            UnityWebRequest request = new UnityWebRequest(string.Format("http://localhost:3000/weather?lat={0}&lon={1}", LocationServices.CurrentLocation.m_Latitude, LocationServices.CurrentLocation.m_Longitude), "GET");
            request.downloadHandler = downloadHandler;

            await request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.Success)
            {
                Models.Weather weather = JsonUtility.FromJson<Models.Weather>(request.downloadHandler.text);
                return weather;
            }
            return null;
        }
    }
}