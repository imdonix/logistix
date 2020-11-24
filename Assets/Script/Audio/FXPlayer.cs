using UnityEngine;

public class FXPlayer : AudioPlayer<FXPlayer>
{
    [SerializeField] public AudioClip win;
    [SerializeField] public AudioClip lose;
    [SerializeField] public AudioClip touch;

    public void Play(AudioClip audio)
    {
        Debug.Log(audio);
        sorce.clip = audio;
        sorce.Play();
    }

}
