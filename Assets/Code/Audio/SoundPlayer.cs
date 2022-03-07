using UnityEngine;

namespace Audio
{
    public class SoundPlayer : FXPlayer<SoundPlayer>
    {
        [SerializeField] public AudioClip win;
        [SerializeField] public AudioClip lose;
        [SerializeField] public AudioClip touch;

        public void Play(AudioClip audio)
        {
            sorce.clip = audio;
            sorce.Play();
        }

    }
}