using UnityEngine;

public class Cloud : MonoBehaviour 
{
    [SerializeField] public float Speed = 1;
    [SerializeField] private float MaxOut = 10;
    private float Direction;
    private float RandomMulti;



    private void Start()
    {
        NewRandom();
        Direction = Random.Range(0f, 1f) > 0.5f ? 1 : -1;
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x) > MaxOut) 
        {
            NewRandom();
            Direction *= -1;
        }
    }

    private void Update()
    {
        transform.position += Vector3.right * Speed * RandomMulti * Direction * Time.deltaTime;
    }

    private void NewRandom()
    {
        RandomMulti = Random.Range(0.5f, 1.5f);
    }
}