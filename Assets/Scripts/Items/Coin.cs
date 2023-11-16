using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private CircleCollider2D _circleCollider2D;
    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 只有玩家能拾取硬币
        if (!Util.ComparePlayerTag(other.gameObject)) return;
        Destroy(gameObject);
    }
}
