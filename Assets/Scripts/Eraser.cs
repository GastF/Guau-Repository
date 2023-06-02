using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Eraser : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private VFXActivator vfxActivator;

    private void Start()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        transform.position = spawnPoint.position;

        float distance = Vector3.Distance(spawnPoint.position, targetPoint.position);
        float travelTime = distance / moveSpeed;

        StartCoroutine(MoveRoutine(travelTime));
    }

    private IEnumerator MoveRoutine(float travelTime)
    {
        yield return new WaitForSeconds(spawnInterval);

        float elapsedTime = 0f;
        while (elapsedTime < travelTime)
        {
            float t = elapsedTime / travelTime;
            transform.position = Vector3.Lerp(spawnPoint.position, targetPoint.position, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        MoveObject();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        vfxActivator.ActivateVFX(); // Activa el VFX desde el GameObject aparte
        if (collision.gameObject.CompareTag("Dog"))
        {
            Debug.Log("dog");
            Destroy(collision.gameObject);
            GameManager.Instance.DecreseScore(30);
           
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}


