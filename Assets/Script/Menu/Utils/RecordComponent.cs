using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public class RecordComponent : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] public Color Simple;
    [SerializeField] public Color Premium;
    [SerializeField] public Sprite PremiumBanner;
    [SerializeField] public Sprite SimpleBanner;
    [SerializeField] public Sprite RecorderBanner;


    [Header("Dependecies")]
    [SerializeField] public Image PlaceImage;
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
        PlaceImage.sprite = record.Premium ? PremiumBanner : SimpleBanner;
        Name.color = record.Premium ? Premium : Simple;
        Place.text = $"{place}";
        Name.text = record.Name;
        Score.text = CreateNiceScore(record.Score.ToString());
        
        if (place == 1) 
            MakeRecorder();
    }

    public RectTransform Get()
    {
        return Rect;
    }

    #endregion
 
    #region PRIVATE

    private void MakeRecorder()
    {
        transform.localScale *= 1.2f;
        Place.text = string.Empty;
        PlaceImage.sprite = RecorderBanner;
    }

    private static string CreateNiceScore(string score)
    {
        if (score.Length > 7)
            return '+' + (Math.Round(Math.Pow(10, 7)) - 1).ToString();
        return score;

    }

    #endregion

}
