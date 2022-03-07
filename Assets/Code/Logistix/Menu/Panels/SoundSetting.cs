using Audio;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SoundSetting : MenuPanel
    {

        [Header("Dependecies")]
        [SerializeField] private Image music;
        [SerializeField] private Image sound;

        [Header("Properties")]
        [SerializeField] private Sprite MusicOn;
        [SerializeField] private Sprite MusicOff;
        [SerializeField] private Sprite SoundOn;
        [SerializeField] private Sprite SoundOff;

        private int live;

        #region UNITY

        private void Awake() { live = 0; }

        #endregion

        #region UI

        protected override void OnClose() { }

        protected override void OnOpen() { if (live++ <= 0) Set(); }

        public void OnMusicTouch()
        {
            MusicPlayer.Instance.Toggle();
            Set();
        }

        public void OnSoundTouch()
        {
            SoundPlayer.Instance.Toggle();
            Set();
        }

        #endregion


        private void Set()
        {
            music.sprite = MusicPlayer.Instance.IsMuted() ? MusicOff : MusicOn;
            sound.sprite = SoundPlayer.Instance.IsMuted() ? SoundOff : SoundOn;
        }

    }
}