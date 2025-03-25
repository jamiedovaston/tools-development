using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using JD.LookOutside.Location;

namespace JD.LookOutside.Time
{
    public static class TimeServices
    {
        public async static Task<Models.Time> GetTime()
        {
            DownloadHandlerBuffer downloadHandler = new DownloadHandlerBuffer();
            UnityWebRequest request = new UnityWebRequest(string.Format("http://localhost:3000/unix-time?lat={0}&lon={1}", LocationServices.CurrentLocation.m_Latitude, LocationServices.CurrentLocation.m_Longitude), "GET");
            request.downloadHandler = downloadHandler;

            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Models.UnixTime unix = JsonUtility.FromJson<Models.UnixTime>(request.downloadHandler.text);
                Models.Time time = new Models.Time(unix.unix_time);
                return time;
            }
            return null;
        }
    }
}
