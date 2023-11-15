using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleDamage : MonoBehaviour
{
    // 定义事件
    public UnityEvent<int, Vector2> damageableHit;  // 受到伤害时触发的事件，包括伤害值和击退向量
    public UnityEvent<int, int> healthChanged;  // 生命值改变时触发的事件，包括当前生命值和最大生命值

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

    // 是否存活属性
    [SerializeField]
    private bool _isAlive = true;

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
        }
    }

    // 在脚本唤醒时获取动画组件的引用
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // 受到伤害的方法，返回是否成功受到伤害
    public bool Hit(int damage, Vector2 knockback, Enums.DamageType damageType)
    {
        if (IsAlive)
        {
            Health -= damage;
            damageableHit?.Invoke(damage, knockback);
            // 使用新定义的方法触发事件
            CharacterEvents.TriggerCharacterDamaged(gameObject, damage, damageType);

            return true;
        }
        return false;
    }

}
