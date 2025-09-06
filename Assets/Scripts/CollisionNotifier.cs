using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionNotifier : MonoBehaviour
{
    public delegate void CollisionHandler(GameObject a, GameObject b);
    public event CollisionHandler onCollisionEvent;

    private void OnDestroy()
    {
        onCollisionEvent = null;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        onCollisionEvent?.Invoke(this.gameObject, col.gameObject);
    }
}
