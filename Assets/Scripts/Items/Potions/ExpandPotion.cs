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
        if (!TagUtil.ComparePlayerTag(other.gameObject)) return;
        
        PlayerStatUtil.IncreasePlayerHp(maxHpIncreaseAmount);
        TextManager.Instance.OnCreatingPotionText(other.transform.position, "Max Health + " + maxHpIncreaseAmount 
            + "!");
        
        Destroy(gameObject);
    }
}
