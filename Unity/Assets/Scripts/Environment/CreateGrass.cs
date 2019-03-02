using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrass : MonoBehaviour
{
    public GameObject GrassPrefab = null;
    public Transform Ground = null;
    public float Width = 8;
    public int GrassQuantity = 100;

    private const float GroundHeight = 0.25f;

    private void Start()
    {
        if (GrassPrefab == null || Ground == null)
        {
            return;
        }

        while (GrassQuantity-- > 0)
        {
            Instantiate(GrassPrefab,
                new Vector3(Random.Range(-Width, Width), GroundHeight, Random.Range(-Width, Width)), 
                Quaternion.Euler(0, Random.Range(0, 360), 0), 
                Ground);
        }
    }
}
