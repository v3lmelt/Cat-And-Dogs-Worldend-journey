using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Collider2D))]
public class MagicPotion : MonoBehaviour
{
    public int maxMpIncreaseAmount = 10;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!TagUtil.ComparePlayerTag(other.gameObject)) return;
        
        PlayerStatUtil.IncreasePlayerMp(maxMpIncreaseAmount);
        TextManager.Instance.OnCreatingPotionText(other.transform.position, "Max Magic Point + " + maxMpIncreaseAmount 
            + "!");
        Destroy(gameObject);
    }
}
