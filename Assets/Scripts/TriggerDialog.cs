using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// 需要Collider2D检测玩家是否在范围内
[RequireComponent(typeof(BoxCollider2D))]
public class TriggerDialog : MonoBehaviour
{
    [Tooltip("提示信息创建的位移")]
    public Vector3 hintCreateDelta;
    [Tooltip("对话框信息创建的位移")]
    public Vector3 dialogCreatePosDelta;
    
    [Tooltip("要加载的对话")]
    public TextAsset dialogText;
    [Tooltip("对话加载的速度")]
    public float textSpeed = 0.1f;
    
    private Collider2D _collider2D;
    private bool _playerInZone;
    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!Util.ComparePlayerTag(other.gameObject)) return;
        _playerInZone = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!Util.ComparePlayerTag(other.gameObject)) return;
        _playerInZone = false;
        
        // 强制取消对话
        DialogSystemNew.Instance.ResetDialogSystem();
    }

    private void Update()
    {
        if (!_playerInZone) return;
        if (Input.GetKeyDown(KeyCode.R) && DialogSystemNew.Instance.HasFinishedDialog)
        {
            DialogSystemNew.Instance.InitDialogSystem(dialogText, transform.position + dialogCreatePosDelta, textSpeed);
        }
    }
}
