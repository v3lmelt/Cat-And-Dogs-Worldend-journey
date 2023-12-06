using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public int damage = 50;

    private Transform target;

    void Start()
    {
        // 在启动时查找 Boss 对象
        target = GameObject.FindGameObjectWithTag("Boss2").transform;

        if (target == null)
        {
            Debug.LogError("Boss not found!");
        }
    }

    void Update()
    {
        if (target != null)
        {
            Seek();
        }
        else
        {
            // 如果没有目标，销毁导弹
            Destroy(gameObject);
        }
    }

    void Seek()
    {
        // 计算导弹方向
        Vector2 direction = (target.position - transform.position).normalized;

        // 计算旋转角度
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 旋转导弹
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotationSpeed * Time.deltaTime);

        // 移动导弹
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    // 设置导弹的初始朝向
    public void SetInitialDirection(float direction)
    {
        transform.localScale = new Vector3(direction, 1f, 1f);
    }

    // 导弹与其他物体碰撞时触发
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss2"))
        {
            collision.GetComponent<Tentacle>().BeHit(damage);
            Destroy(gameObject); // 碰撞后销毁导弹
        }
    }
}
