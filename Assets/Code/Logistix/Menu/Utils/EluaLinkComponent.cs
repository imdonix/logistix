using Logistix;
using UnityEngine;
using Utils;

namespace UI
{
    public class EluaLinkComponent : MonoBehaviour
    {
        public void OnClick()
        {
            Application.OpenURL(Util.CreateURLEndcoded(GameManager.Instance.API.GetEULA(), new string[] { }));
        }

    }
}