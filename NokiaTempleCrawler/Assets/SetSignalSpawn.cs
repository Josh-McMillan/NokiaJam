using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSignalSpawn : MonoBehaviour
{
    private void Start()
    {
        SignalReception.OnLocationUpdated(transform.position);
    }
}
