using System;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Tentacle : MonoBehaviour
{
    private Transform _cTra;
    private Transform _mouse;
    public Transform player1Transform;
    public Transform player2Transform;
    public float speed = 5.0f;
    [FormerlySerializedAs("FoamPrefab")] public GameObject foamPrefab;
    [FormerlySerializedAs("WaterPrefab")] public GameObject waterPrefab;

    private bool _canMove = true;
    [FormerlySerializedAs("MaxHp")] public float maxHp;
    [FormerlySerializedAs("Hp")] public float hp;
    public bool isDead;
    private Animator _animator;

    private Vector2 _initial;
    private Rigidbody2D _rigidbody2D;

    private BossHealthBar _bossHealthBar;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _cTra = GetComponent<Transform>();
        _initial = _cTra.localScale;
        _mouse = transform.Find("Mouse");
        _bossHealthBar = GameObject.Find("HealthBar").GetComponent<BossHealthBar>();
        
        
        // 进行Boss血条的初始化操作
        if (_bossHealthBar == null)
        {
            Debug.Log("Can't find bossHealthBar, did you forget to place it?");
        }
        else
        {
            _bossHealthBar.maxHp = maxHp;
            _bossHealthBar.currentHp = hp;
        }
        
    }

    private void Update()
    {
        CheckHp();
        // 计算怪物到两个玩家的距离
        var position = transform.position;
        var distanceToPlayer1 = Vector2.Distance(position, player1Transform.position);
        var distanceToPlayer2 = Vector2.Distance(position, player2Transform.position);

        // 选择距离更短的玩家进行追逐
        var targetPlayer = (distanceToPlayer1 < distanceToPlayer2) ? player1Transform : player2Transform;

        // 在 Update 方法中实现怪物追逐玩家的逻辑
        if (_canMove)
        {
            ChasePlayer(targetPlayer);
        }
    }

    private void ChasePlayer(Transform targetPlayer)
    {
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
                var firebullet = Instantiate(foamPrefab, null);
                var dir = Quaternion.Euler(0, i * 15, 0) * -transform.right;
                firebullet.transform.position = _mouse.position + dir * 1.0f;
                firebullet.transform.rotation = Quaternion.Euler(0, 0, i * 15);
            }

            for (var i = 0; i < 2; i++)
            {
                var randomAngle = Random.Range(-5f, 2f);
                var firebullet = Instantiate(foamPrefab, null);
                var dir = Quaternion.Euler(0, randomAngle * 15, 0) * transform.right;
                firebullet.transform.position = _mouse.position + dir * 1.0f;
                firebullet.transform.rotation = Quaternion.Euler(0, 0, randomAngle * 15);
            }
        }
        else if (Math.Abs(_cTra.localScale.x - (-_initial.x)) < 0.005f)
        {
            for (var i = -1; i < 5; i++)
            {
                var firebullet = Instantiate(foamPrefab, null);
                var dir = Quaternion.Euler(0, i * 15, 0) * transform.right;
                firebullet.transform.position = _mouse.position + dir * 1.0f;
                firebullet.transform.rotation = Quaternion.Euler(0, 0, i * 15);
            }

            for (var i = 0; i < 2; i++)
            {
                var randomAngle = Random.Range(-5f, 2f);
                var firebullet = Instantiate(foamPrefab, null);
                var dir = Quaternion.Euler(0, randomAngle * 15, 0) * transform.right;
                firebullet.transform.position = _mouse.position + dir * 1.0f;
                firebullet.transform.rotation = Quaternion.Euler(0, 0, randomAngle * 15);
            }
        }
    }

    public void CreateWater()
    {
        _canMove = false;
        // 将 Boss 的速度置零
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        for (var i = 0; i < 5; i++)
        {
            var r = Random.Range(-13, 17);
            var foam = Instantiate(waterPrefab, null);
            foam.transform.position = new Vector3(156, r, 0);
        }
    }

    public void CanMove()
    {
        _canMove = true;
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void CheckHp()
    {
        switch (hp)
        {
            case <= 0:
                isDead = true;
                _animator.Play("Death");
                break;
            case > 0 and <= 30:
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
}