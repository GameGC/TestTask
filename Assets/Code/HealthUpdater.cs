using System;
using System.Collections;
using UnityEngine;

public class HealthUpdater : MonoBehaviour
{
    public event Action<int> OnDamage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            StartCoroutine(OneSecondUpdate());
        }
    }

    private IEnumerator OneSecondUpdate()
    {
        var wait = new WaitForSecondsRealtime(1);
        while (true)
        {
            OnDamage?.Invoke(1);
            yield return wait;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            StopCoroutine(OneSecondUpdate());
        }
    }

    private void OnDisable()
    {
        StopCoroutine(OneSecondUpdate());
    }
}