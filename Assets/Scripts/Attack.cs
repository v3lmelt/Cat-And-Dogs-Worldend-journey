using Enums;
using System.Collections;
using System.Collections.Generic;
using Boss1;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //每次攻击恢复魔力值
    public int restoreMp = 10;

    // 攻击伤害值
    public int attackDamage = 10;

    // 击退向量
    public Vector2 knockback = Vector2.zero;

    // 当触发器与其他Collider2D碰撞时调用的方法
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 获取与触发器碰撞的对象的Damageable组件
        Damageable damageable = collision.GetComponent<Damageable>();

        // 如果碰撞的对象具有Damageable组件
        if (damageable != null)
        {
            // 根据攻击方向设置实际的击退向量
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            // 触发Damageable对象的Hit方法，传递攻击伤害和击退向量
            bool getHit = damageable.Hit(attackDamage, deliveredKnockback, DamageType.Melee);

            // 如果成功击中
            if (getHit)
            {
                // 在控制台输出击中信息
                Debug.Log(collision.name + " hit for " + attackDamage);

                Damageable attackerDamageable = transform.parent.GetComponent<Damageable>();

                if (attackerDamageable != null)
                {
                    attackerDamageable.RestoreMP(restoreMp);
                }
            }
        }

        ObstacleDamage obstacleDamage = collision.GetComponent<ObstacleDamage>();

        if (obstacleDamage != null)
        {
            Vector2 deliveredKnockback = new Vector2(0,0);
            obstacleDamage.Hit(attackDamage, deliveredKnockback, DamageType.Melee);
        }

        if (collision.gameObject.CompareTag("Boss1"))
        {
            collision.GetComponent<Villain>().BeHit(attackDamage);
        }
        if (collision.gameObject.CompareTag("Boss2"))
        {
            collision.GetComponent<Tentacle>().BeHit(attackDamage);
        }
    }
}
