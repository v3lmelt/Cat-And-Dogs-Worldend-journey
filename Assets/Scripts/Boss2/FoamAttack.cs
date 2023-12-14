using Enums;
using UnityEngine;

public class FoamAttack : MonoBehaviour
{
    GameObject Boss;
    Animator animator;
    Collider2D collider2d;
    SubmarineController controller;
    Vector3 dir;

    private bool canMove = true;

    public float Speed;

    public int Damge;

    public float LifeTime;

    // Start is called before the first frame update
    void Start()
    {
        Boss = GameObject.Find("Tentacle");
        animator = GetComponent<Animator>();
        collider2d = GetComponent<Collider2D>();

        dir = transform.localScale;

        Speed = 5;

        Damge = 8;

        LifeTime = 7f;
        controller = GameObject.Find("submarine").GetComponent<SubmarineController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
            Move();
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0 || Boss.GetComponent<Tentacle>().isDead)
        {
            controller.setBoolCanMove();
            Destroy(gameObject);
        }
    }

    public void Move()
    {
        if (Boss.transform.localScale.x > 0)
        {
            var transform1 = transform;
            transform1.localScale = new Vector3(dir.x, dir.y, dir.z);
            transform1.position += -transform1.right * (Speed * Time.deltaTime);
        }
        else if (Boss.transform.localScale.x < 0)
        {
            var transform1 = transform;
            transform1.localScale = new Vector3(-dir.x, dir.y, dir.z);
            transform1.position += transform1.right * (Speed * Time.deltaTime);
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
        if (collision.CompareTag("Submarine"))
        {
            Damageable damageable = collision.GetComponent<Damageable>();
            animator.Play("Hit");
            if (damageable != null)
            {
                if (transform.position.x < collision.transform.position.x)
                {
                    //collision.GetComponent<PlayerController>().onHit(damage, Vector2.right);
                    damageable.Hit(Damge, Vector2.right, DamageType.Melee);
                }
                else if (transform.position.x >= collision.transform.position.x)
                {
                    //collision.GetComponent<PlayerController>().onHit(damage, Vector2.left);
                    damageable.Hit(Damge, Vector2.left, DamageType.Melee);
                }
                controller.setBoolCanNotMove();
            }
            canMove = false;
        }
        else if (collision.CompareTag("Ground"))
        {
            animator.Play("Hit");
        }
    }
}
