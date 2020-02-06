using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFloorMaterial : MonoBehaviour
{
    [SerializeField] private Material[] floors;

    private MeshRenderer floor;

    private void Start()
    {
        floor = GetComponent<MeshRenderer>();

        Material[] instance = floor.materials;

        instance[0] = floors[Random.Range(0, floors.Length)];

        floor.materials = instance;
    }
}
