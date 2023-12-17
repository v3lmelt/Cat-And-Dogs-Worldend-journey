using System;
using Cainos.LucidEditor;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tentacle : MonoBehaviour
{
    public float speed = 5.0f;
    public GameObject foamPrefab;
    public GameObject waterPrefab;

    private const float MaxHp = 3750;
    public float hp;
    public bool isDead;

    [Header("玩家追逐计时器")] 
    public float chasePlayerTime = 2.5f;
    [SerializeField]
    private float chasePlayerTimeElapsed;

    [Header("Boss静止计时器")] 
    public float idleTime = 3.0f;
    [SerializeField] 
    private float idleTimeElapsed;
    
    private Transform _cTra;
    private Transform _mouse;

    [SerializeField] 
    private bool canMove;
    private Animator _animator;

    private Vector2 _initial;
    private Rigidbody2D _rigidbody2D;

    private BossHealthBar _bossHealthBar;
    private Transform _submarineTransform;
    
    [Tooltip("激光射出的源点")]
    public GameObject laserShotOrigin;
    
    [Tooltip("激光射出的Y范围")]
    public float laserMinY;
    public float laserMaxY;

    [Tooltip("生成激光的数量")] 
    public int laserCountMax = 9;
    public int laserCountMin = 5; 
    
    [Header("触碰伤害")]
    public int touchDamage = 5;

    public string sceneChangeAfterDeath = "StartMenu";

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _cTra = GetComponent<Transform>();
        _initial = _cTra.localScale;
        _mouse = transform.Find("Mouse");
        _bossHealthBar = GameObject.Find("BossHealthBar").GetComponent<BossHealthBar>();
        _submarineTransform = GameObject.Find("submarine").transform;
        
        
        // 进行Boss血条的初始化操作
        if (_bossHealthBar == null)
        {
            Debug.Log("Can't find bossHealthBar, did you forget to place it?");
        }
        else
        {
            _bossHealthBar.maxHp = MaxHp;
            _bossHealthBar.currentHp = hp;
        }
        
    }

    private void Update()
    {
        CheckHp();
        // 对于水下关卡，此时玩家只有一个Submarine, 因此只考虑追逐Submarine
        if (canMove && chasePlayerTimeElapsed < chasePlayerTime)
        {
            ChasePlayer(_submarineTransform);
        }
        // 追逐玩家完毕后转而静止
        else if (idleTimeElapsed < idleTime)
        {
            _rigidbody2D.velocity = Vector2.zero;
            canMove = false;
            idleTimeElapsed += Time.deltaTime;
        }
        // 静止时间计时器
        else if (idleTimeElapsed >= idleTime)
        {
            idleTimeElapsed = 0;
            chasePlayerTimeElapsed = 0;
            canMove = true;
        }
        
        
        
    }

    private void ChasePlayer(Transform targetPlayer)
    {
        chasePlayerTimeElapsed += Time.deltaTime;
        // 计算朝向目标玩家的方向
        Vector2 direction = (targetPlayer.position - transform.position).normalized;

        // 设置怪物的速度
        _rigidbody2D.velocity = direction * speed;

        switch (direction.x)
        {
            // 通过修改怪物的朝向
            case < 0:
            {
                var currentLocalScale = transform.localScale;
                // 玩家在怪物的右侧，保持原始朝向
                transform.localScale =
                    new Vector3(Mathf.Abs(currentLocalScale.x), currentLocalScale.y, currentLocalScale.z);
                break;
            }
            case > 0:
                // 玩家在怪物的左侧，翻转朝向
                var localScale = transform.localScale;
                localScale = new Vector3(-Mathf.Abs(localScale.x), localScale.y, localScale.z);
                transform.localScale = localScale;
                break;
        }
    }

    public void CreateFoam()
    {
        if (Math.Abs(_cTra.localScale.x - _initial.x) < 0.005f)
        {
            for (var i = -5; i < 2; i++)
            {
                var fireBullet = Instantiate(foamPrefab, null);
                var dir = Quaternion.Euler(0, i * 15, 0) * -transform.right;
                fireBullet.transform.position = _mouse.position + dir * 1.0f;
                fireBullet.transform.rotation = Quaternion.Euler(0, 0, i * 15);
            }

            for (var i = 0; i < 2; i++)
            {
                var randomAngle = Random.Range(-5f, 2f);
                var fireBullet = Instantiate(foamPrefab, null);
                var dir = Quaternion.Euler(0, randomAngle * 15, 0) * transform.right;
                fireBullet.transform.position = _mouse.position + dir * 1.0f;
                fireBullet.transform.rotation = Quaternion.Euler(0, 0, randomAngle * 15);
            }
        }
        else if (Math.Abs(_cTra.localScale.x - (-_initial.x)) < 0.005f)
        {
            for (var i = -1; i < 5; i++)
            {
                var fireBullet = Instantiate(foamPrefab, null);
                var dir = Quaternion.Euler(0, i * 15, 0) * transform.right;
                fireBullet.transform.position = _mouse.position + dir * 1.0f;
                fireBullet.transform.rotation = Quaternion.Euler(0, 0, i * 15);
            }

            for (var i = 0; i < 2; i++)
            {
                var randomAngle = Random.Range(-5f, 2f);
                var fireBullet = Instantiate(foamPrefab, null);
                var dir = Quaternion.Euler(0, randomAngle * 15, 0) * transform.right;
                fireBullet.transform.position = _mouse.position + dir * 1.0f;
                fireBullet.transform.rotation = Quaternion.Euler(0, 0, randomAngle * 15);
            }
        }
    }

    public void CreateWater()
    {
        canMove = false;
        // 将 Boss 的速度置零
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        for (var i = 0; i < Random.Range(laserCountMin, laserCountMax); i++)
        {
            // 使用激光射出的Y范围
            var randomY = Random.Range(laserMinY, laserMaxY);
            var laserPos = new Vector3(laserShotOrigin.transform.position.x, randomY,
                0);
            var foam = Instantiate(waterPrefab, laserPos,
                Quaternion.identity);
        }
    }

    public void CanMove()
    {
        canMove = true;
    }

    public void Death()
    {
        LoadSceneManager.Instance.LoadScene(sceneChangeAfterDeath);
        Destroy(gameObject);
    }

    private void CheckHp()
    {
        switch (hp)
        {
            case <= 0:
                isDead = true;
                _animator.Play("Death");
                break;
            case > 0 and <= MaxHp * 0.8f:
                _animator.Play("Attack_Water");
                break;
        }
    }

    public void BeHit(float damage)
    {
        hp -= damage;
        _bossHealthBar.ChangeHealth(-damage);
        CharacterEvents.TriggerCharacterDamaged(gameObject, (int)damage, DamageType.Melee);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var damageable = other.GetComponent<Damageable>();
            damageable.Hit(touchDamage, Vector2.zero, DamageType.Melee);
        }
    }
}