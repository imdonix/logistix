using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class AudioPlayer<T> : Singleton<T>
{
    [SerializeField] private string key;

    protected AudioSource sorce;

    #region UNITY

    protected virtual void Start()
    {
        sorce = GetComponent<AudioSource>();
        Maintain();
    }

    #endregion

    #region PUBLIC

    public void Toggle()
    {
        PlayerPrefs.SetInt($"audio_{key}", IsMuted() ? 0 : 1);
        PlayerPrefs.Save();
    }

    public bool IsMuted()
    {
        return PlayerPrefs.GetInt($"audio_{key}", 0) > 0;
    }

    #endregion

    private void Maintain()
    {
        sorce.mute = IsMuted();
    }
}