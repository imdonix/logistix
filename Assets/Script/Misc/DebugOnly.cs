using UnityEngine;

class DebugOnly : MonoBehaviour
{

    private void Awake()
    {
        gameObject.SetActive(GameManager.Instance.IsDebugMode());        
    }

}
