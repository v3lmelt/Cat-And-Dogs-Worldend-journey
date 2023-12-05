using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    Transform C_tra;
    Transform Mouse;
    public Transform player1Transform;
    public Transform player2Transform;
    public float speed = 5.0f;
    public GameObject FoamPrefab;
    public GameObject WaterPrefab;

    private bool canMove = true;
    public float MaxHp;
    public float Hp;
    public bool isDead = false;
    private Animator animator;

    Vector2 Initial;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        C_tra = GetComponent<Transform>();
        Initial = C_tra.localScale;
        Mouse = transform.Find("Mouse");
    }

    private void Update()
    {
        CheckHp();
        // 计算怪物到两个玩家的距离
        float distanceToPlayer1 = Vector2.Distance(transform.position, player1Transform.position);
        float distanceToPlayer2 = Vector2.Distance(transform.position, player2Transform.position);

        // 选择距离更短的玩家进行追逐
        Transform targetPlayer = (distanceToPlayer1 < distanceToPlayer2) ? player1Transform : player2Transform;

        // 在 Update 方法中实现怪物追逐玩家的逻辑
        if (canMove)
        {
            ChasePlayer(targetPlayer);
        }
    }

    private void ChasePlayer(Transform targetPlayer)
    {
        // 计算朝向目标玩家的方向
        Vector2 direction = (targetPlayer.position - transform.position).normalized;

        // 设置怪物的速度
        GetComponent<Rigidbody2D>().velocity = direction * speed;

        // 通过修改怪物的朝向
        if (direction.x < 0)
        {
            // 玩家在怪物的右侧，保持原始朝向
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x > 0)
        {
            // 玩家在怪物的左侧，翻转朝向
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    public void CreateFoam()
    {
        if (C_tra.localScale.x == Initial.x)
        {
            for (int i = -5; i < 2; i++)
            {
                GameObject firebullet = Instantiate(FoamPrefab, null);
                Vector3 dir = Quaternion.Euler(0, i * 15, 0) * -transform.right;
                firebullet.transform.position = Mouse.position + dir * 1.0f;
                firebullet.transform.rotation = Quaternion.Euler(0, 0, i * 15);
            }
            for (int i = 0; i < 2; i++)
            {
                float randomAngle = Random.Range(-5f, 2f);
                GameObject firebullet = Instantiate(FoamPrefab, null);
                Vector3 dir = Quaternion.Euler(0, randomAngle * 15, 0) * transform.right;
                firebullet.transform.position = Mouse.position + dir * 1.0f;
                firebullet.transform.rotation = Quaternion.Euler(0, 0, randomAngle * 15);
            }
        }
        else if (C_tra.localScale.x == -Initial.x)
        {
            for (int i = -1; i < 5; i++)
            {
                GameObject firebullet = Instantiate(FoamPrefab, null);
                Vector3 dir = Quaternion.Euler(0, i * 15, 0) * transform.right;
                firebullet.transform.position = Mouse.position + dir * 1.0f;
                firebullet.transform.rotation = Quaternion.Euler(0, 0, i * 15);
            }
            for (int i = 0; i < 2; i++)
            {
                float randomAngle = Random.Range(-5f, 2f);
                GameObject firebullet = Instantiate(FoamPrefab, null);
                Vector3 dir = Quaternion.Euler(0, randomAngle * 15, 0) * transform.right;
                firebullet.transform.position = Mouse.position + dir * 1.0f;
                firebullet.transform.rotation = Quaternion.Euler(0, 0, randomAngle * 15);
            }
        }
    }
    public void CreateWater()
    {
        canMove = false;
        // 将 Boss 的速度置零
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        for (int i = 0; i < 5; i++)
        {
            int r = Random.Range(-13, 17);
            GameObject foam = Instantiate(WaterPrefab, null);
            foam.transform.position = new Vector3(156, r, 0);
        }
    }
    public void CanMove()
    {
        canMove=true;
    }
    public void Death()
    {
        Destroy(gameObject);
    }
    public void CheckHp()
    {
        if (Hp <= 0)
        {
            isDead = true;
            animator.Play("Death");
        }
        else if (Hp>0 &&Hp <= 30)
        {
            animator.Play("Attack_Water");
        }
    }
    public void BeHit(float Damge)
    {
        Hp -= Damge;
        //isHit = true;
    }
}
