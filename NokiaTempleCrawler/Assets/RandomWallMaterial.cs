using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWallMaterial : MonoBehaviour
{
    [SerializeField] private Material[] walls;

    private MeshRenderer wall;

    private void Start()
    {
        wall = GetComponent<MeshRenderer>();

        Material[] instance = wall.materials;

        instance[1] = walls[Random.Range(0, walls.Length)];

        wall.materials = instance;
    }
}
