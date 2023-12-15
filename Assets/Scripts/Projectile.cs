using System.Collections;
using System.Collections.Generic;
using Boss1;
using UnityEngine;
using Enums;
public class Projectile : MonoBehaviour
{

    public int damage;
    public Vector2 moveSpeed = new Vector2(3f, 0);
    public Vector2 knockback = new Vector2(0, 0);

    Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            bool getHit = damageable.Hit(damage, deliveredKnockback, Enums.DamageType.Ranged);
            if (getHit)
                Debug.Log(collision.name + " hit for " + damage);
                Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Boss1"))
        {
            collision.GetComponent<Villain>().BeHit(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Boss2"))
        {
            collision.GetComponent<Tentacle>().BeHit(damage);
            Destroy(gameObject);
        }
    }
}
