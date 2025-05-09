using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System;
using JD.LookOutside.Utilities;

namespace JD.LookOutside
{
    public static class JDLOServices
    {
        public static bool Initialised { get; private set; }
        public async static Task<bool> Init()
        {
            UnityWebRequest request = new UnityWebRequest($"{JDLOConfig.Domain}", "GET");
            request.downloadHandler = new DownloadHandlerBuffer();

            try
            {
                await request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log(DebugFormatting.Format("Successful connection to weather server!"));
                    TimeServices.Initialise();
                    WeatherServices.Initialise();
                    Initialised = true;
                    return true;
                }
                else
                {
                    Debug.LogError(DebugFormatting.FormatError($"Connection to weather server was unsuccessful. Error: {request.error}"));
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"An error occurred: {ex.Message}");
                return false;
            }

        }
    }
}