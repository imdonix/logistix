using Networking.Models;
using UnityEngine;

namespace UI
{
    public class RowUIComponent : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private LevelUIComponent LevelComp;

        [Header("View")]
        public RectTransform Rect;
        public LevelUIComponent[] Levels;

        private void Awake()
        {
            Rect = GetComponent<RectTransform>();
        }

        public void LoadData(LevelModel[] levels)
        {
            Levels = new LevelUIComponent[levels.Length];
            float delta = LevelPanel.WIDTH / (1 + levels.Length);
            for (int i = 0; i < levels.Length; i++)
            {
                LevelUIComponent comp = Instantiate(LevelComp);
                comp.Rect.SetParent(Rect);
                comp.Rect.localScale = Vector3.one;
                comp.Rect.localPosition =
                    new Vector2(delta * (i + 1) - (LevelPanel.WIDTH / 2), 0);
                comp.Load(levels[i]);
                Levels[i] = comp;
            }
        }
    }
}