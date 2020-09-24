using UnityEngine;
using UnityEngine.UI.Extensions;

public class DependencyUIComponent : MonoBehaviour
{
    public RectTransform Rect;
    private UILineRenderer render;

    private void Awake()
    {
        Rect = GetComponent<RectTransform>();
        render = GetComponent<UILineRenderer>();
    }

    public void Show(Vector2 start, Vector2 end)
    {
        render.Points[0] = start;
        render.Points[1] = end;
    }
}