using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using UnityEditor;
using JetBrains.Annotations;

namespace JD.LookOutside
{
    public static class TimeServices
    {
        [RuntimeInitializeOnLoadMethod]
        private async static void Initialise()
        {
            await GetTime();
        }

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

    public class TimeKeeperComponent : JDLO_Component
    {
        public void Initialise()
        {

        }
    }

    public class JDLO_Component : MonoBehaviour 
    {
        public JDLO_Component() {
            gameObject.hideFlags = HideFlags.HideInInspector;
        }
    }

}
