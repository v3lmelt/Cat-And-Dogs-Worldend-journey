using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaManager : MonoBehaviour
{
    public GameObject Cat;
    public GameObject Dog;
    private RaycastHit2D hit;
    void Start()
    {
        Cat = GameManager.Instance.cat;
        Dog = GameManager.Instance.dog;
    }

    // Update is called once per frame
    void Update()
    {
        //猫脚底下发射射线 如果检测到狗 则猫的速度等于狗的速度
         hit = Physics2D.Raycast(Cat.transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Player"));
        Debug.DrawRay(Cat.transform.position, Vector2.down * 1f, Color.red);
        if (hit.collider.gameObject.name == "Dog" )
        {
            Cat.GetComponent<Rigidbody2D>().velocity = Dog.GetComponent<Rigidbody2D>().velocity;
        }
        //如果猫踩到了海就会死亡
        hit = Physics2D.Raycast(Cat.transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Sea"));
        Debug.DrawLine(Cat.transform.position, Cat.transform.position + Vector3.down * 0.1f, Color.red);
        if (hit.collider != null)
        {
            Cat.GetComponent<Damageable>().IsAlive = false;
        }
    }
}
