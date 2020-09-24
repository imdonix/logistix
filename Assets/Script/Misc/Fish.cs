using UnityEditor;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private const float WATER_LEVEL = -1.5f;
    private const float BOTTOM = -7;
    private const float TOP = 0;
    private const float WIDTH = 5;

    [Header("Properties")]
    [SerializeField] private float speed;
    [SerializeField] private float boost;
    [SerializeField] private float time;

    private Rigidbody2D rigid;
    private SpriteRenderer render;

    private Vector2 target;
    private float stateTime;

    #region UNITY

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, target);
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        CheckGravity();
        AddForce();
        SetRotation();
        CheckTimer();
    }

    #endregion

    public void Init(Sprite sprite)
    {
        render.sprite = sprite;
        SetNextPosition();
    }

    private void SetNextPosition()
    {
        target = GetRandomPos();
        stateTime = time * Random.Range(0.5f, 3);
    }

    private void CheckGravity()
    {
        bool isInAir = transform.position.y > WATER_LEVEL;
        rigid.gravityScale = isInAir ? 1 : 0;
        if (isInAir && target.y > WATER_LEVEL)
            SetNextPosition();
    }

    private void AddForce()
    {
        Vector3 force = GetDirection() * speed * Time.deltaTime * GetRandomMultiplier();
        if (target.y > WATER_LEVEL)
            force *= boost;
        rigid.AddForce(force);
    }

    private Vector3 GetDirection()
    {
        return ((Vector3)target - transform.position).normalized;
    }

    private float GetRandomMultiplier()
    {
        return UnityEngine.Random.Range(0.5f, 1.5f);
    }

    private void CheckTimer()
    {
        stateTime -= Time.deltaTime;
        if (stateTime < 0)
            SetNextPosition();
    }

    private void SetRotation()
    {
        float x = GetDirection().x;
        float rad = Mathf.Acos(x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(Vector3.forward * rad);
    }

    public static Vector2 GetRandomPos()
    {
        float x = UnityEngine.Random.Range(-WIDTH, WIDTH);
        float y = UnityEngine.Random.Range(BOTTOM, TOP);
        return new Vector2(x, y);

    }


}

