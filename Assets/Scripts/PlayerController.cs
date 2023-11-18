using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(Damageable))]

public class PlayerController : MonoBehaviour
{
    // 移动速度
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float airWalkSpeed = 3f;
    public float jumpImpulse = 10f;

    //冲刺
    private bool canDash = true;
    private bool isDashing = false;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField]
    private TrailRenderer tr;

    // 输入变量
    Vector2 moveInput;

    // 组件引用
    TouchingDirection touchingDirections;
    Damageable damageable;


    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        /*float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;*/

        // 设置冲刺速度
        float dashSpeed = dashingPower;
        if (!IsFacingRight)
        {
            dashSpeed = -dashingPower;
        }

        // 应用冲刺速度
        rb.velocity = new Vector2(dashSpeed, rb.velocity.y);

        // 播放冲刺效果
        tr.emitting = true;

        // 冲刺持续时间
        yield return new WaitForSeconds(dashingTime);

        // 关闭冲刺效果
        tr.emitting = false;
        
        //rb.gravityScale = originalGravity;
        isDashing = false;

        // 冷却时间
        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
    }


    // 属性
    public float CurrentMoveSpeed
    {
        get
        {
            if (IsMoving && !touchingDirections.IsOnWall)
            {
                if (touchingDirections.IsGrounded)
                {
                    if (IsRunning)
                        return runSpeed;
                    else
                        return walkSpeed;
                }
                else
                {
                    return airWalkSpeed;
                }
            }
            else
                return 0;
        }
    }


    // 是否移动属性
    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    // 是否奔跑属性
    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    // 是否面朝右边属性
    [SerializeField]
    public bool _isFacingRight = true;

    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    // 是否可以移动属性
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    // 对刚体2D的物理使用
    Rigidbody2D rb;

    // 对象动画控制器
    Animator animator;

    // 在脚本唤醒时执行的方法
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirection>();
        damageable = GetComponent<Damageable>();
    }

    // 这个地方是对对象的速度进行设置
    private void FixedUpdate()
    {
        if (!damageable.LockVelocity)
        {
            if (!isDashing)
            {
                rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
            }
        }

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    // 是否存活属性
    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    // 处理移动输入的方法
    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    public void onMove(Vector2 input)
    {
        moveInput = input;
        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }
    

    public void onDash(InputAction.CallbackContext context)
    {
        if (context.started && canDash)
        {
            StartCoroutine(Dash());
            animator.SetTrigger(AnimationStrings.dash);
        }
    }

    public void onDash()
    {
        if (canDash)
        {
            StartCoroutine(Dash());
            animator.SetTrigger(AnimationStrings.dash);
        }
    }

    // 设置朝向的方法
    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    // 处理奔跑输入的方法
    public void onRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    // 处理跳跃输入的方法
    public void onJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }
    
    public void onJump()
    {
        if (touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    // 处理攻击输入的方法
    public void onAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void onAttack()
    {
        animator.SetTrigger(AnimationStrings.attackTrigger);
    }

    // 处理远程攻击输入的方法
    public void onRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
        }
    }

    public void onRangedAttack()
    {
        animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
    }

    // 处理被攻击的方法
    public void onHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
