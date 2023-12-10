using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Damageable), typeof(Rigidbody2D), typeof(TouchingDirection))]
[RequireComponent(typeof(Animator))]
public class MobileEnemy : MonoBehaviour
{
    public float walkAcceleration = 3f;
    public float maxSpeed = 3f;
    public float walkStopRate = 0.05f;
    
    // 组件
    private Damageable _damageable;
    private Rigidbody2D _rigidbody2D;
    private TouchingDirection _touchingDirections;
    private Animator _animator;
    
    // 怪物走路方向的设定
    public enum WalkDirection { Right, Left };
    
    public WalkDirection CurrentWalkDirection { get ; set; }
    private Vector2 _walkDirectionVector = Vector2.right;

    public bool HasTarget { get; set; }
    public bool CanMove { get; set; }
    
    // 初始化组件
    private void Awake()
    {
        _damageable = GetComponent<Damageable>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _touchingDirections = GetComponentInChildren<TouchingDirection>();
        _animator = GetComponent<Animator>();
    }
    
    // 保证怪物不会摔下悬崖
    
}