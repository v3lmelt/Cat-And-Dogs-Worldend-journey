using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

// 使用陷阱脚本需要Collider2D提供检测支持
[RequireComponent(typeof(Collider2D))]
public class SpikeTrap : MonoBehaviour
{
    [Tooltip("陷阱的伤害")]
    public int trapDamage = 10;
    private Collider2D _collider2D;
    
    
    
    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        if(_collider2D == null) Debug.LogError("Have you forgot to add collider2D on spike trap?");
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        // 只有玩家能够与陷阱进行交互
        if (!Util.ComparePlayerTag(other.gameObject)) return;
        var damageable = other.gameObject.GetComponent<Damageable>();
        
        damageable.Hit(trapDamage, new Vector2(0, 5), DamageType.Melee);
    }
}
