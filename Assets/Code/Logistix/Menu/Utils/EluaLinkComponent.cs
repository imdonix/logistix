using Logistix;
using UnityEngine;
using Utils;

namespace UI
{
    public class EluaLinkComponent : MonoBehaviour
    {
        private const string EULA_ENDPOINT = "privacy";

        public void OnClick()
        {
            Application.OpenURL(Util.CreateURLEndcoded(LogisticAPI.Instance.GetServerURI() + EULA_ENDPOINT, new string[] { }));
        }

    }
}