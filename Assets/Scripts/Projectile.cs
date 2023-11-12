using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // 抛射物的伤害值
    public int damage = 10;

    // 抛射物的移动速度
    public Vector2 moveSpeed = new Vector2(3f, 0);

    // 抛射物的击退向量
    public Vector2 knockback = new Vector2(0, 0);

    // 刚体组件
    Rigidbody2D rb;

    // 在脚本唤醒时执行的方法
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // 在脚本启用时执行的方法
    void Start()
    {
        // 设置刚体的速度，使抛射物沿着指定方向移动
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    // 当抛射物与其他Collider2D碰撞时调用的方法
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 获取与抛射物碰撞的对象的Damageable组件
        Damageable damageable = collision.GetComponent<Damageable>();

        // 如果碰撞的对象具有Damageable组件
        if (damageable != null)
        {
            // 根据抛射物方向设置实际的击退向量
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            // 触发Damageable对象的Hit方法，传递抛射物伤害和击退向量
            bool getHit = damageable.Hit(damage, deliveredKnockback, Enums.DamageType.Ranged);

            // 如果成功击中
            if (getHit)
            {
                // 在控制台输出击中信息
                Debug.Log(collision.name + " hit for " + damage);

                // 销毁抛射物对象
                Destroy(gameObject);
            }
        }
    }
}
