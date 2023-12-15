using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnergyRecovertRatePotion : MonoBehaviour
{
    public int IncreaseAmount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!TagUtil.ComparePlayerTag(other.gameObject)) return;

        PlayerStatUtil.IncreaseRestoreMp(IncreaseAmount);
        TextManager.Instance.OnCreatingPotionText(other.transform.position, "RestoreMp + " + IncreaseAmount
            + "!");

        Destroy(gameObject);
    }
}

