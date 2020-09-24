using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public class RecordComponent : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] public Color Simple;
    [SerializeField] public Color Premium;

    [Header("Dependecies")]
    [SerializeField] public Text Place;
    [SerializeField] public Text Name;
    [SerializeField] public Text Score;

    private Image Base;
    private RectTransform Rect;

    #region UNITY
    private void Awake()
    {
        Base = GetComponent<Image>();
        Rect = GetComponent<RectTransform>();
    }

    #endregion

    #region PUBLIC

    public void Set(int place, RecordModel record)
    {
        Base.color = record.Premium ? Premium : Simple;
        Place.text = $"#{place}";
        Name.text = record.Name;
        Score.text = record.Score.ToString();
    }

    public RectTransform Get()
    {
        return Rect;
    }

    #endregion

}
