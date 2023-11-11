using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : MonoBehaviour
{
    private Collider2D _collider2D;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        TextManager.Instance.OnCreatingPotionText(other.transform.position, "Speed Potion!");
        PotionEvent.OnGettingSpeedPotion.Invoke();
    }
}
