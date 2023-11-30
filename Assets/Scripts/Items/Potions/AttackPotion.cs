using System;
using UnityEditor.UI;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackPotion : MonoBehaviour
{
    public int damageIncreaseAmount = 5;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 只有玩家才能发生碰撞
        if (!Util.ComparePlayerTag(other.gameObject)) return;
        var attack = other.gameObject.GetComponentInChildren<Attack>();
        if (attack == null)
        {
            Debug.LogError("Attack component can't be found in children!");
            return;
        }
        // 
        attack.attackDamage += damageIncreaseAmount;
        TextManager.Instance.OnCreatingPotionText(other.transform.position, "Damage + " + damageIncreaseAmount
        + "!");
        
        Destroy(gameObject);
    }
}
