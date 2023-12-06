using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireBullet : MonoBehaviour
{
    GameObject Boss;
    Animator animator;
    Collider2D collider2d;

    Vector3 dir;

    public float Speed;

    public int Damge;

    public float LifeTime;

    // Start is called before the first frame update
    void Start()
    {
        Boss = GameObject.Find("Dr. Villain");
        animator = GetComponent<Animator>();
        collider2d = GetComponent<Collider2D>();

        dir = transform.localScale;

        Speed = 5;

        Damge = 8;

        LifeTime = 7f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0 || Boss.GetComponent<Villain>().isDead)
        {
            Destroy(gameObject);
        }
    }

    public void Move()
    {
        if (Boss.transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(dir.x, dir.y, dir.z);
            transform.position += Speed * -transform.right * Time.deltaTime;
        }
        else if (Boss.transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-dir.x, dir.y, dir.z);
            transform.position += Speed * transform.right * Time.deltaTime;
        }
    }
    public void CloseCollider()
    {
        collider2d.enabled = false;
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")|| collision.CompareTag("Player_cat"))
        {
            Damageable damageable = collision.GetComponent<Damageable>();
            animator.Play("Hit");
            if (damageable != null)
            {
                if (transform.position.x < collision.transform.position.x)
                {
                    //collision.GetComponent<PlayerController>().onHit(Damge, Vector2.right);
                    damageable.Hit(Damge, Vector2.right, DamageType.Melee);
                }
                else if (transform.position.x >= collision.transform.position.x)
                {
                    //collision.GetComponent<PlayerController>().onHit(Damge, Vector2.left);
                    damageable.Hit(Damge, Vector2.left, DamageType.Melee);
                }
            }
        }
        else if (collision.CompareTag("Ground"))
        {
            animator.Play("Hit");
        }
    }
}
