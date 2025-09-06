using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNotifier : MonoBehaviour
{
    public delegate void TriggerHandler(GameObject a, GameObject b);
    public event TriggerHandler onTriggerEvent;

    private void OnDestroy()
    {
        onTriggerEvent = null;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        onTriggerEvent?.Invoke(this.gameObject, col.gameObject);
    }

}
