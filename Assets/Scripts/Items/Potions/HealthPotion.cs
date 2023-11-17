using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    private Collider2D _collider2D;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 判断接触者是否是玩家
        if (!Util.ComparePlayerTag(other.gameObject)) return;
        
        var damageable = other.GetComponent<Damageable>();
        var healthRecoveryAmount = damageable.MaxHealth - damageable.Health;
        // 都满血了回个啥呢
        // if (healthRecoveryAmount == 0) return;
        // 触发治疗事件
        damageable.Health += healthRecoveryAmount;

        if (other.gameObject.CompareTag("Player_cat"))
        {
            // 是猫猫的话再回个蓝
            var manaRecoveryAmount = damageable.MaxMP - damageable.MP;
            // if (manaRecoveryAmount == 0) return;
        
            damageable.MP += manaRecoveryAmount;
        }
        Debug.Log("About to destroy!");
        Destroy(this.gameObject);

    }
}
