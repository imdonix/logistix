using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Utils;

namespace Networking.Core
{
    public abstract class WebRequestEngine<L> : Singleton<L>, IEngineStatus
    {
        [Header("Properties")]
        [SerializeField] private bool IsDebug;
        [SerializeField] private string OnlineURI;
        [SerializeField] private string TestURI;
        [SerializeField] private int Timeout = 25;

        private int RequestsPendingCount;
        private string ServerURI => IsDebug ? TestURI : OnlineURI;

        #region UNITY

        protected override void Awake()
        {
            base.Awake();
            RequestsPendingCount = 0;
        }

        #endregion

        #region PUBLIC

        public int GetPendingCount()
        {
            return RequestsPendingCount;
        }

        public bool IsRequestPendig()
        {
            return GetPendingCount() > 0;
        }

        public string GetServerURI()
        {
            return ServerURI;
        }

        #endregion

        #region PROTECTED

        protected void Send<T>(
            string api, 
            Action<T> response, 
            Action<string> error)
        {
            UnityWebRequest request = UnityWebRequest.Get(ServerURI + api);
            StartCoroutine(SendViaUnity(request, response, error));
        }

        protected void Send<T>(
            string api, 
            string json, 
            Action<T> response, 
            Action<string> error)
        {
            var request = new UnityWebRequest(ServerURI + api, "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            StartCoroutine(SendViaUnity(request, response, error));
        }

        protected void Send<T>(
            string api,
            Dictionary<string,string> form, 
            Action<T> response, 
            Action<string> error)
        {
            UnityWebRequest request = UnityWebRequest.Post(ServerURI + api, form);
            StartCoroutine(SendViaUnity(request, response, error));
        }

        #endregion

        private IEnumerator SendViaUnity<T>(UnityWebRequest req, Action<T> response, Action<string> error)
        {
            RequestsPendingCount++;
            req.timeout = Timeout;
            yield return req.SendWebRequest();

            if (req.isNetworkError || req.isHttpError)
                error.Invoke(req.error);
            else
                response.Invoke(JsonConvert.DeserializeObject<T>(req.downloadHandler.text));
            RequestsPendingCount--;
        }

    }
}

