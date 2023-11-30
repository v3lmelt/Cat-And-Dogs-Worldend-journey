using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    // 定义事件
    public UnityEvent<int, Vector2> damageableHit;  // 受到伤害时触发的事件，包括伤害值和击退向量
    public UnityEvent damageableDeath;  // 死亡时触发的事件
    public UnityEvent<int, int> healthChanged;  // 生命值改变时触发的事件，包括当前生命值和最大生命值
    public UnityEvent<int, int> mpChanged;

    // 引用动画组件
    Animator animator;

    // 最大生命值属性
    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
            healthChanged?.Invoke(_health, MaxHealth);
        }
    }

    // 当前生命值属性
    [SerializeField]
    private int _health = 100;
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private int _mp = 100;
    public int MP
    {
        get { return _mp; }
        set
        {
            _mp = value;
            mpChanged?.Invoke(_mp, MaxMP);
            if (_mp <= 0)
            {
                HasMp = false;
            }
        }
    }

    [SerializeField]
    private int _maxMP = 100;
    public int MaxMP
    {
        get { return _maxMP; }
        set
        {
            _maxMP = value; 
            mpChanged?.Invoke(_mp, MaxMP);
        }
    }

    //魔法消耗值
    public int MagicCost = 20;


    [SerializeField]
    private bool _hasMp = true;

    public bool HasMp
    {
        get
        {
            return _hasMp;
        }
        set
        {
            _hasMp = value;
            animator.SetBool(AnimationStrings.hasMp, value);
            Debug.Log("HasMp set " + value);
        }
    }

    // 是否存活属性
    [SerializeField]
    private bool _isAlive = true;

    // 是否无敌属性
    [SerializeField]
    private bool isInvincible = false;

    private float timeSinceHit = 0;

    // 无敌时间
    public float invincibilityTime = 0.25f;

    // 是否存活属性的公共访问器
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set " + value);

            if (value == false)
            {
                damageableDeath.Invoke();
            }
        }
    }

    // 锁定速度属性
    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    // 在脚本唤醒时获取动画组件的引用
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // 在每一帧更新时检查是否无敌状态的持续时间已经过去
    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }

    public bool Fir()
    {
        if (MP >= MagicCost)
        {
            MP -= MagicCost;

            return true;
        }
        return false;
    }

    public void RestoreMP(int restoreMp)
    {
        if (MP < MaxMP)
        {
            int maxRestore = Mathf.Max(MaxMP - MP, 0);
            int actualRestore = Mathf.Min(restoreMp, maxRestore);
            MP += actualRestore;
        }
        if (MP >= MagicCost)
        {
            HasMp = true;
        }
    }


    // 受到伤害的方法，返回是否成功受到伤害
    public bool Hit(int damage, Vector2 knockback, Enums.DamageType damageType)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            // 使用新定义的方法触发事件
            CharacterEvents.TriggerCharacterDamaged(gameObject, damage, damageType);

            return true;
        }
        return false;
    }

    // 治疗的方法，返回是否成功治疗
    public bool Heal(int healthRestore)
    {
        if (IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(healthRestore, maxHeal);
            Health += actualHeal;

            CharacterEvents.characterHealed(gameObject, actualHeal);
            return true;
        }
        return false;
    }
}
