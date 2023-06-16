using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Eraser : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
   
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private VFXActivator vfxActivator;

    private void Start()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        StartCoroutine(OscillateRoutine());
    }

    private IEnumerator OscillateRoutine()
    {
        while (true)
        {
            // Mover desde spawnPoint hasta targetPoint
            float distance = Vector3.Distance(spawnPoint.position, targetPoint.position);
            float travelTime = distance / moveSpeed;

            yield return StartCoroutine(MoveToPoint(spawnPoint.position, targetPoint.position, travelTime));

            // Mover desde targetPoint hasta spawnPoint
            distance = Vector3.Distance(targetPoint.position, spawnPoint.position);
            travelTime = distance / moveSpeed;

            yield return StartCoroutine(MoveToPoint(targetPoint.position, spawnPoint.position, travelTime));
        }
    }

    private IEnumerator MoveToPoint(Vector3 startPos, Vector3 targetPos, float travelTime)
    {
        float elapsedTime = 0f;
        while (elapsedTime < travelTime)
        {
            float t = elapsedTime / travelTime;
            transform.position = Vector3.Lerp(startPos, targetPos, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        vfxActivator.ActivateVFX(); // Activa el VFX desde el GameObject aparte
        if (collision.gameObject.CompareTag("Sid") || collision.gameObject.CompareTag("BlanquiNegro") || collision.gameObject.CompareTag("Terraneitor"))
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


