using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXActivator : MonoBehaviour
{
    [SerializeField] private GameObject vfxPrefab;
    [SerializeField] private Transform spawnPoint;

    public void ActivateVFX()
    {
        Instantiate(vfxPrefab, spawnPoint.position, Quaternion.identity);
    }
}
