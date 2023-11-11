using System.Collections;
using UnityEngine;

public class PotionHandler : MonoBehaviour
{
    private Coroutine _jumpBuffCoroutine;
    private Coroutine _speedBuffCoroutine;

    private PlayerController _playerController;
    
    private float _jumpBuffTimer;
    private float _speedBuffTimer;

    public float jumpBuffDuration = 10f;
    public float speedBuffDuration = 10f;

    public float jumpImpulseBuffAmount = 5f;
    public float speedBuffAmount = 5f;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        PotionEvent.OnGettingJumpPotion += OnApplyJumpBuff;
        PotionEvent.OnGettingSpeedPotion += OnApplySpeedBuff;
    }

    private void OnApplyJumpBuff()
    {
        if (_jumpBuffCoroutine != null)
        {
            // 刷新jump
            _jumpBuffTimer = 0f;
        }
        else
        {
            _jumpBuffCoroutine = StartCoroutine(ApplyJumpBuff());
        }

        
    }

    private IEnumerator ApplyJumpBuff()
    {
        _playerController.jumpImpulse += jumpImpulseBuffAmount;
        while (_jumpBuffTimer < jumpBuffDuration)
        {
            _jumpBuffTimer += Time.deltaTime;
            yield return null;
        }
        _playerController.jumpImpulse -= jumpImpulseBuffAmount;
        _jumpBuffCoroutine = null;
        _jumpBuffTimer = 0f;
    }

    private void OnApplySpeedBuff()
    {
        if (_speedBuffCoroutine != null)
        {
            // 刷新jump
            _speedBuffTimer = 0f;
        }
        else
        {
            _speedBuffCoroutine = StartCoroutine(ApplySpeedBuff());
        }
    }

    private IEnumerator ApplySpeedBuff()
    {
        _playerController.walkSpeed += speedBuffAmount;
        while (_speedBuffTimer < speedBuffDuration)
        {
            _speedBuffTimer += Time.deltaTime;
            yield return null;
        }
        _playerController.walkSpeed -= speedBuffAmount;
        _speedBuffCoroutine = null;
        _speedBuffTimer = 0f;
    }
}