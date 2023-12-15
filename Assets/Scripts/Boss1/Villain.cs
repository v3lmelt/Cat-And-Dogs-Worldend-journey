using System;
using System.Collections;
using Cainos.LucidEditor;
using Enums;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Boss1
{
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
        private Transform _cTra;
        private Transform _muzzle;
        private Rigidbody2D _cRig;

        private Animator _cAni;
        //public AudioSource HitAudio;
        public GameObject fireBullet;

        private Vector2 _initial;

        private BossState _state;
        
        [Header("基本属性")]
        public float maxHp;
        public float hp;

        public float speed;

        public float idleTime;

        public int fireBulletAttackTime;
        [Header("Boss状态")]
        public bool isHit;
        public bool isDead;
        
        [Header("怪物生成")]
        [Tooltip("在Boss空闲时生成的怪物数量")]
        public int enemySpawnMax = 8;
        public int enemySpawnMin = 4;

        public float spawnEnemyTimerElapsed = 0f;
        [Tooltip("生成敌人的计时器")]
        public float spawnEnemyTimer = 15f;

        [Tooltip("结束后去的场景")] 
        public string sceneToLoadAfterDeath;
        
        private BossHealthBar _healthBar;
        private void Awake()
        {
            _cTra = GetComponent<Transform>();
            _cRig = GetComponent<Rigidbody2D>();
            _cAni = GetComponent<Animator>();
            
            GameObject.Find("Dog");
            GameObject.Find("Cat");
            
            _muzzle = transform.Find("Muzzle");

            _initial = _cTra.localScale;

            _state = BossState.Idle;
            
            _healthBar = GameObject.Find("BossHealthBar").GetComponent<BossHealthBar>();
            // boss血量初始化工作
            if (_healthBar == null)
            {
                Debug.LogError("Can't find healthbar");
                return;
            }
            
            
            _healthBar.maxHp = maxHp;
            _healthBar.currentHp = hp;
        }

        // Update is called once per frame
        void Update()
        {
            CheckHp();
            switch (_state)
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
                    IdleProcess();
                    break;
                }
                case BossState.BeHit:
                {
                    BeHitProcess();
                    break;
                }
                case BossState.Death:
                {
                    _cAni.Play("Death");
                    break;
                }
            }
        }

        public void FireBulletAttack()
        {
            _cAni.Play("Attack");
            if (fireBulletAttackTime <= 0 && !isDead)
            {
                _state = BossState.Idle;
            }
            else if (isDead)
            {
                _state = BossState.Death;
            }
        }
        public void FireBulletCreate()
        {
            if (Math.Abs(_cTra.localScale.x - _initial.x) < 0.005f)
            {
                for (int i = -5; i < 2; i++)
                {
                    GameObject firebullet = Instantiate(fireBullet, null);
                    Vector3 dir = Quaternion.Euler(0, i * 15, 0) * -transform.right;
                    firebullet.transform.position = _muzzle.position + dir * 1.0f;
                    firebullet.transform.rotation = Quaternion.Euler(0, 0, i * 15);
                }
            }
            else if (Math.Abs(_cTra.localScale.x - (-_initial.x)) < 0.005f)
            {
                for (int i = -1; i < 5; i++)
                {
                    GameObject firebullet = Instantiate(fireBullet, null);
                    Vector3 dir = Quaternion.Euler(0, i * 15, 0) * transform.right;
                    firebullet.transform.position = _muzzle.position + dir * 1.0f;
                    firebullet.transform.rotation = Quaternion.Euler(0, 0, i * 15);
                }
            }
            fireBulletAttackTime -= 1;
        }
        public void DashSkill()
        {
            if (!isDead)
            {
                Dash();
                idleTime = 5f;
            }
            else if (isDead)
            {
                _state = BossState.Death;
            }
        }
        public void Dash()//冲撞 
        {
            if (Math.Abs(_cTra.localScale.x - _initial.x) < 0.005f)
            {
                _cAni.Play("Walk");
                _cRig.velocity = new Vector2(-speed, _cRig.velocity.y);
            }
            else if (Math.Abs(_cTra.localScale.x - (-_initial.x)) < 0.005f)
            {
                _cAni.Play("Walk");
                _cRig.velocity = new Vector2(speed, _cRig.velocity.y);
            }
        }
        public void IdleProcess()
        {
            _cAni.Play("Idle");
            idleTime -= Time.deltaTime;
            spawnEnemyTimerElapsed += Time.deltaTime;
            
            if (spawnEnemyTimerElapsed >= spawnEnemyTimer)
            {
                StartCoroutine(BossEnemySpawner.Instance.SpawnEnemy(Random.Range(
                    enemySpawnMin, enemySpawnMax)));
                spawnEnemyTimerElapsed = 0f;
            }
            if (hp <= maxHp / 2 && idleTime > 0)
            {
                fireBulletAttackTime = 5;
            }
            else if (hp > maxHp / 2 && idleTime > 0)
            {
                fireBulletAttackTime = 3;
            }
            if (idleTime <= 0 && !isHit && !isDead)
            {
                _state = BossState.Dash;
            }
            else if (isHit && !isDead)
            {
                _state = BossState.BeHit;
            }
            else if (isDead)
            {
                _state = BossState.Death;
            }
        }
        public void BeHitProcess()
        {
            _cAni.Play("BeHit");
            idleTime -= Time.deltaTime;
            if (!isHit && !isDead)
            {
                _state = BossState.Idle;
            }
            else if (isDead)
            {
                _state = BossState.Death;
            }
            
            
        }
        public void BeHit(float damage)
        {
            hp -= damage;
            isHit = true;
            
            _healthBar.ChangeHealth(-damage);
            CharacterEvents.TriggerCharacterDamaged(gameObject, (int)damage, DamageType.Melee);
        }
        public void BeHitOver()
        {
            isHit = false;
        }
        public void CheckHp()
        {
            if (hp <= 0)
            {
                isDead = true;
            }
        }
        public void Death()
        {
            StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            yield return new WaitForSeconds(1f);
            LoadSceneManager.Instance.LoadScene(sceneToLoadAfterDeath);
            
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {

            if (collision.collider.CompareTag("AirWall"))
            {
                var localScale = _cTra.localScale;
                localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
            
                _cTra.localScale = localScale;
                _state = BossState.FireBullet;
            }
        }
    }
}