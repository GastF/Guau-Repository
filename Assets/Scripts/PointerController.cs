using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    [SerializeField] private float offsetDistance;
    [SerializeField] private Transform Pointer;
    private GameObject currentObject;
    private FirstObject firstObjectScript;

    private void Start()
    {
        firstObjectScript = FindObjectOfType<FirstObject>();
    }

    private void Update()
    {
        currentObject = firstObjectScript.GetFirstElement();

        if (currentObject != null)
        {
            Vector3 newPosition = currentObject.transform.position + Vector3.up * offsetDistance;
            Pointer.transform.position = newPosition;

           
        }
    }
}   
