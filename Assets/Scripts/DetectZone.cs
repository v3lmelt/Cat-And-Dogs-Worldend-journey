using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectZone : MonoBehaviour
{
    public UnityEvent NoCollidersRemain;

    public List<Collider2D> detectedColliders;
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColliders.Add(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision);

        if (detectedColliders.Count <= 0)
        {
            NoCollidersRemain.Invoke();
        }
    }
}
