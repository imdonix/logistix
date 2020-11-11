using UnityEngine;

public class ManyBox : Box
{
    [SerializeField] private Item[] items;
    [SerializeField] private int canBeLost;

    #region PUBLIC

    public override void OnEject()
    {
        foreach (Item item in items)
            item.transform.SetParent(null);
    }

    public override bool IsLost()
    {
        int c = 0;
        foreach (Item item in items)
            if (item.IsLost())
                c++;
        return c > canBeLost;
    }

    public override Vector2 GetPosition()
    {
        Vector3 pos = transform.position;
        foreach (Item item in items)
            pos += item.transform.position;
        return pos / (items.Length + 1);
    }

    public override Vector2 GetSize()
    {
        return new Vector2(1, 1);
    }

    public override void Destroy()
    {
        foreach (Item item in items)
            Destroy(item, 1f);
        base.Destroy();
    }

    #endregion


}
