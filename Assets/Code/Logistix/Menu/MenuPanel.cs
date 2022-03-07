using Audio;
using UnityEngine;

namespace UI
{
    public abstract class MenuPanel : MonoBehaviour
    {

        [Header("Overlays")]
        [SerializeField] private MenuPanel[] Overlays;

        public void Show()
        {
            foreach (MenuPanel panel in Overlays)
                panel.Show();
            gameObject.SetActive(true);
            SoundPlayer.Instance.Play(SoundPlayer.Instance.touch);
            OnOpen();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            OnClose();
        }

        public virtual void Back() { }

        protected abstract void OnOpen();

        protected abstract void OnClose();

    }
}