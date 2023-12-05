using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SubmarineControls
{
    public KeyCode moveUp = KeyCode.U;
    public KeyCode moveDown = KeyCode.J;
    public KeyCode moveLeft = KeyCode.H;
    public KeyCode moveRight = KeyCode.K;
    public KeyCode attack = KeyCode.Space;

    [HideInInspector]
    public Vector2 inputVector;
}
public class SubmarineInput : MonoBehaviour
{
    public SubmarineControls controls;
    private SubmarineController _submarineController;

    private void Awake()
    {
        _submarineController = GetComponent<SubmarineController>();
    }

    void Update()
    {
        // 更新二维向量
        float horizontalInput = (Input.GetKey(controls.moveLeft) ? -1f : 0f) + (Input.GetKey(controls.moveRight) ? 1f : 0f);
        float verticalInput = (Input.GetKey(controls.moveDown) ? -1f : 0f) + (Input.GetKey(controls.moveUp) ? 1f : 0f);
        controls.inputVector = new Vector2(horizontalInput, verticalInput);

        // 处理移动
        _submarineController.OnMove(controls.inputVector);

        // 有没有攻击
        if (Input.GetKeyDown(controls.attack)) _submarineController.OnAttack();
    }
}
