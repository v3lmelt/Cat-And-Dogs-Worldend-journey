using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ExpandPotion : MonoBehaviour
{
    public int maxHpIncreaseAmount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!Util.ComparePlayerTag(other.gameObject)) return;
        var damageable = other.GetComponent<Damageable>();
        if (damageable == null)
        {
            Debug.LogError("Hey damageable can't be found! wtf");
            return;
        }
        damageable.MaxHealth += maxHpIncreaseAmount;
        damageable.Health += maxHpIncreaseAmount;
        TextManager.Instance.OnCreatingPotionText(other.transform.position, "Max Health + " + maxHpIncreaseAmount 
            + "!");
        
        Destroy(gameObject);
    }
}
