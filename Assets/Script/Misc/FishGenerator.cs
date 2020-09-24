using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGenerator : MonoBehaviour
{
    [Header("Prefhab")]
    [SerializeField] private Fish fish;

    [Header("Properties")]
    [SerializeField] private Sprite[] fishes;
    [SerializeField] private int count;

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            Fish f = Instantiate(fish, (Vector3)Fish.GetRandomPos(), Quaternion.identity);
            f.name = $"Fish{i}";
            f.transform.SetParent(transform);
            f.Init(fishes[Random.Range(0, fishes.Length)]);
        }
    }
}
