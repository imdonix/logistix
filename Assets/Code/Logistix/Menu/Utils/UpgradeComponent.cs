using UnityEngine;
using UnityEngine.UI;
using Utils;
using TMPro;

namespace UI
{
    public class UpgradeComponent : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] public Text Wood;
        [SerializeField] public Text Iron;
        [SerializeField] public TMP_Text Status;
        [SerializeField] public TMP_Text Description;

        private Button button;

        #region UNITY

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        #endregion

        public void Set(int max)
        {
            Status.text = $"{max} of {max}";
            SetText("Maxed out");
            SetCostVisible(false);
            SetEnabled(false);
        }

        public void Set(int wood, int iron, int level, int max)
        {
            gameObject.SetActive(true);
            Wood.text = Util.ToReadableNumber(wood);
            Iron.text = Util.ToReadableNumber(iron);
            Status.text = $"{level} of {max}";
            SetCostVisible(true);
            SetText("Upgrade");
            SetEnabled(true);
        }

        private void SetCostVisible(bool visible)
        {
            Wood.gameObject.transform.parent.gameObject.SetActive(visible);
            Iron.gameObject.transform.parent.gameObject.SetActive(visible);
        }

        private void SetText(string text)
        {
            Description.text = text;
        }

        private void SetEnabled(bool enabled)
        {
            button.interactable = enabled;
        }

    }
}