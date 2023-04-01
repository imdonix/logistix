using System.Collections;
using UnityEngine;

namespace Audio
{
    public class MusicPlayer : FXPlayer<MusicPlayer>
    {
        [SerializeField] private AudioClip menu;
        [SerializeField] private AudioClip game;

        private Song state;
        private int times;

        protected override void Awake()
        {
            base.Awake();
            times = 0;
        }

        public void Play(Song song)
        {
            if (state != song || times == 0)
            {
                times++;
                PlaySong(song);
                state = song;
            }
        }

        private void PlaySong(Song song)
        {
            AudioClip audio = null;
            if (song == Song.Game) audio = game;
            if (song == Song.Menu) audio = menu;
            StartCoroutine(SafePlay(audio, times));
        }

        private IEnumerator SafePlay(AudioClip clip, int validity)
        {
            yield return new WaitUntil(() => clip.loadState == AudioDataLoadState.Loaded);
            if (validity == times)
            {
                sorce.clip = clip;
                sorce.Play();
            }
        }

    }
}