using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum BossState
{
    FireBullet,
    Dash,
    Idle,
    BeHit,
    Death,
}

public class Villain : MonoBehaviour
{
    Transform C_tra;
    Transform Muzzle;
    Rigidbody2D C_rig;
    Animator C_ani;
    //public AudioSource HitAudio;
    public GameObject FireBullet;
    public GameObject Player;//获取到玩家的位置
    public GameObject Player2;//获取到玩家的位置

    Vector2 Initial;

    BossState state;

    public float MaxHp;
    public float Hp;

    public int MoveDamge;

    public float Speed;

    public float IdleTime;
    public float time;

    public int FireBulletAttackTime;

    public bool isHit;
    public bool isDead;

    private void Awake()
    {
        C_tra = GetComponent<Transform>();
        C_rig = GetComponent<Rigidbody2D>();
        C_ani = GetComponent<Animator>();
        Player = GameObject.Find("Player");
        Player2 = GameObject.Find("Player_cat");
        Muzzle = transform.Find("Muzzle");

        Initial = C_tra.localScale;

        state = BossState.Idle;

        MaxHp = 2000;
        Hp = MaxHp;

        MoveDamge = 20;

        Speed = 12;

        isDead = false;

        IdleTime = 5f;
        time = 1f;

        FireBulletAttackTime = 3;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHp();
        switch (state)
        {
            case BossState.FireBullet:
                {
                    FireBulletAttack();
                    break;
                }
            case BossState.Dash:
                {
                    DashSkill();
                    break;
                }
            case BossState.Idle:
                {
                    IdleProccess();
                    break;
                }
            case BossState.BeHit:
                {
                    BeHitProccess();
                    break;
                }
            case BossState.Death:
                {
                    C_ani.Play("Death");
                    break;
                }
        }
    }

    public void FireBulletAttack()
    {
        C_ani.Play("Attack");
        if (FireBulletAttackTime <= 0 && !isDead)
        {
            state = BossState.Idle;
        }
        else if (isDead)
        {
            state = BossState.Death;
        }
    }
    public void FireBulletCreate()
    {
        if (C_tra.localScale.x == Initial.x)
        {
            for (int i = -5; i < 2; i++)
            {
                GameObject firebullet = Instantiate(FireBullet, null);
                Vector3 dir = Quaternion.Euler(0, i * 15, 0) * -transform.right;
                firebullet.transform.position = Muzzle.position + dir * 1.0f;
                firebullet.transform.rotation = Quaternion.Euler(0, 0, i * 15);
            }
        }
        else if (C_tra.localScale.x == -Initial.x)
        {
            for (int i = -1; i < 5; i++)
            {
                GameObject firebullet = Instantiate(FireBullet, null);
                Vector3 dir = Quaternion.Euler(0, i * 15, 0) * transform.right;
                firebullet.transform.position = Muzzle.position + dir * 1.0f;
                firebullet.transform.rotation = Quaternion.Euler(0, 0, i * 15);
            }
        }
        FireBulletAttackTime -= 1;
    }
    public void DashSkill()
    {
        if (!isDead)
        {
            Dash();
            IdleTime = 5f;
        }
        else if (isDead)
        {
            state = BossState.Death;
        }
    }
    public void Dash()//冲撞 
    {
        if (C_tra.localScale.x == Initial.x)
        {
            C_ani.Play("Walk");
            C_rig.velocity = new Vector2(-Speed, C_rig.velocity.y);
        }
        else if (C_tra.localScale.x == -Initial.x)
        {
            C_ani.Play("Walk");
            C_rig.velocity = new Vector2(Speed, C_rig.velocity.y);
        }
    }
    public void IdleProccess()
    {
        C_ani.Play("Idle");
        IdleTime -= Time.deltaTime;
        if (Hp <= MaxHp / 2 && IdleTime > 0)
        {
            FireBulletAttackTime = 5;
        }
        else if (Hp > MaxHp / 2 && IdleTime > 0)
        {
            FireBulletAttackTime = 3;
        }
        if (IdleTime <= 0 && !isHit && !isDead)
        {
            state = BossState.Dash;
        }
        else if (isHit && !isDead)
        {
            state = BossState.BeHit;
        }
        else if (isDead)
        {
            state = BossState.Death;
        }
    }
    public void BeHitProccess()
    {
        C_ani.Play("BeHit");
        IdleTime -= Time.deltaTime;
        if (!isHit && !isDead)
        {
            state = BossState.Idle;
        }
        else if (isDead)
        {
            state = BossState.Death;
        }
    }
    public void BeHit(float Damge)
    {
        Hp -= Damge;
        isHit = true;
    }
    public void BeHitOver()
    {
        isHit = false;
    }
    public void CheckHp()
    {
        if (Hp <= 0)
        {
            isDead = true;
        }
    }
    public void Death()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("AirWall"))
        {
            C_tra.localScale = new Vector3(-C_tra.localScale.x, C_tra.localScale.y, C_tra.localScale.z);
            state = BossState.FireBullet;
        }
    }
}
