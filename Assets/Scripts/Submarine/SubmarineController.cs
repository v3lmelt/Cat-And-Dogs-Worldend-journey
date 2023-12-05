using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class SubmarineController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float attackCooldown = 1f;
    public GameObject missilePrefab;
    public Transform launchPoint;  // 发射点

    private float attackTimer = 0f;
    private Rigidbody2D rb;

    private bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 不再处理输入，由外部控制器处理后调用对应的方法
        attackTimer -= Time.deltaTime;
    }

    public void OnMove(Vector2 direction)
    {
        MoveSubmarine(direction);
    }

    public void OnAttack()
    {
        if (attackTimer <= 0f)
        {
            Attack();
            attackTimer = attackCooldown;
        }
    }

    public void setBoolCanMove()
    {
        canMove = true;
    }
    public void setBoolCanNotMove()
    {
        canMove = false;
    }

    void Attack()
    {
        if (missilePrefab != null && launchPoint != null)
        {
            // 获取潜水艇的当前朝向
            float submarineDirection = transform.localScale.x;

            // 实例化导弹并设置初始朝向
            GameObject missileObj = Instantiate(missilePrefab, launchPoint.position, Quaternion.identity);
            Missile missile = missileObj.GetComponent<Missile>();
            missile.SetInitialDirection(submarineDirection);
        }
        else
        {
            Debug.LogWarning("MissilePrefab or LaunchPoint not set!");
        }
    }

    void MoveSubmarine(Vector2 direction)
    {
        if (canMove)
        {
            rb.velocity = direction * moveSpeed;
            // 如果角色在向右移动，保持朝右；如果在向左移动，进行水平翻转
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
