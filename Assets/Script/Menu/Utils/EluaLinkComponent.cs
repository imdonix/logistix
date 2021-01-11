using System.Collections;
using UnityEngine;


public class EluaLinkComponent : MonoBehaviour
{
    private const string EULA_ENDPOINT = "privacy";

    public void OnClick()
    {
        Application.OpenURL(Utils.CreateURLEndcoded(LogisticAPI.Instance.GetServerURI() + EULA_ENDPOINT, new string[] { }));
    }

}
