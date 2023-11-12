using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    // �����¼�
    public UnityEvent<int, Vector2> damageableHit;  // �ܵ��˺�ʱ�������¼��������˺�ֵ�ͻ�������
    public UnityEvent damageableDeath;  // ����ʱ�������¼�
    public UnityEvent<int, int> healthChanged;  // ����ֵ�ı�ʱ�������¼���������ǰ����ֵ���������ֵ
    public UnityEvent<int, int> mpChanged;

    // ���ö������
    Animator animator;

    // �������ֵ����
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

    // ��ǰ����ֵ����
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
        set { _maxMP = value; }
    }

    //ħ������ֵ
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

    // �Ƿ�������
    [SerializeField]
    private bool _isAlive = true;

    // �Ƿ��޵�����
    [SerializeField]
    private bool isInvincible = false;

    private float timeSinceHit = 0;

    // �޵�ʱ��
    public float invincibilityTime = 0.25f;

    // �Ƿ������ԵĹ���������
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

    // �����ٶ�����
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

    // �ڽű�����ʱ��ȡ�������������
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // ��ÿһ֡����ʱ����Ƿ��޵�״̬�ĳ���ʱ���Ѿ���ȥ
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


    // �ܵ��˺��ķ����������Ƿ�ɹ��ܵ��˺�
    public bool Hit(int damage, Vector2 knockback, Enums.DamageType damageType)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            // ʹ���¶���ķ��������¼�
            CharacterEvents.TriggerCharacterDamaged(gameObject, damage, damageType);

            return true;
        }
        return false;
    }

    // ���Ƶķ����������Ƿ�ɹ�����
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
