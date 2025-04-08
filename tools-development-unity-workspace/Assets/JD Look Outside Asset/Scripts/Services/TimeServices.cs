using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using UnityEditor;

namespace JD.LookOutside
{
    public static class TimeServices
    {
        private static ITimeKeeperable TimeKeeperable;

        [RuntimeInitializeOnLoadMethod]
        private static void Initialise()
        {
            GameObject timeKeeperComponent = Resources.Load<GameObject>("JD/Components/JDLOTimeKeeper");
            timeKeeperComponent = Object.Instantiate(timeKeeperComponent);
            TimeKeeperable = timeKeeperComponent.GetComponent<ITimeKeeperable>();
        }

        public async static void StartListening()
        {
            Models.Time time = await GetTimeFromServices();

            TimeKeeperable.TimeToTrack(time.time);
        }

        private async static Task<Models.Time> GetTimeFromServices()
        {
            DownloadHandlerBuffer downloadHandler = new DownloadHandlerBuffer();
            UnityWebRequest request = new UnityWebRequest(string.Format("http://localhost:3000/unix-time?lat={0}&lon={1}", LocationServices.CurrentLocation.m_Latitude, LocationServices.CurrentLocation.m_Longitude), "GET");
            request.downloadHandler = downloadHandler;

            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Models.UnixTime unix = JsonUtility.FromJson<Models.UnixTime>(request.downloadHandler.text);
                Models.Time time = new Models.Time(unix.unix_time + unix.timezone_offset);
                return time;
            }
            return null;
        }
    }
}