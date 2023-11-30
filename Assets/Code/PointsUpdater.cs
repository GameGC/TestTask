using System;
using UnityEngine;

public class PointsUpdater : MonoBehaviour
{
    public event Action OnPointCollected;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Item"))
        {
            OnPointCollected?.Invoke();
            Destroy(collision.gameObject);
        }
    }
}