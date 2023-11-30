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
        if (!Util.ComparePlayerTag(other.gameObject)) return;
        var damageable = other.gameObject.GetComponent<Damageable>();
        if (damageable == null)
        {
            Debug.LogError("Hey wtf damageable can't be found");
            return;
        }

        damageable.MaxMP += maxMpIncreaseAmount;
        damageable.MP += maxMpIncreaseAmount;
        TextManager.Instance.OnCreatingPotionText(other.transform.position, "Max Magic Point + " + maxMpIncreaseAmount 
            + "!");
        Destroy(gameObject);
    }
}
