using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public KeyCode attack;
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode rangedAttack;
    public KeyCode jump;

    public Vector2 inputVector;

    public bool hasDash;
    public KeyCode dashKey;

    private PlayerController _playerController;
    private PlayerController _dogPlayerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _dogPlayerController = GameObject.Find("Dog").GetComponent<PlayerController>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // 更新二维向量
        if(Input.GetKey(moveLeft)) inputVector = Vector2.left;
        else if (Input.GetKey(moveRight)) inputVector = Vector2.right;
        else if (Input.GetKeyUp(moveLeft) || Input.GetKeyUp(moveRight)) inputVector = Vector2.zero;
        
        // 处理移动
        _playerController.onMove(inputVector);
        
        // 有没有冲刺?
        if (Input.GetKeyDown(dashKey) && hasDash) _playerController.onDash();
        
        // 有没有跳跃
        if(Input.GetKeyDown(jump)) _playerController.onJump();
        
        // 有没有攻击
        if(Input.GetKeyDown(attack)) _playerController.onAttack();
        
        // 有没有远程攻击，根据一荣的神奇阐述这里必须猫狗一起触发rangedAttack, fxxk
        if (Input.GetKeyDown(rangedAttack))
        {
            _playerController.onRangedAttack();
            _dogPlayerController.onRangedAttack();
        }
    }
}
