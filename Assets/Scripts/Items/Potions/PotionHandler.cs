using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PotionHandler : MonoBehaviour
{
    private Coroutine _jumpBuffCoroutine;
    private Coroutine _speedBuffCoroutine;
    
    
    [FormerlySerializedAs("_playerController")] [SerializeField]
    private PlayerController playerController;
    
    private float _jumpBuffTimer;
    private float _speedBuffTimer;

    public float jumpBuffDuration = 10f;
    public float speedBuffDuration = 10f;

    public float jumpImpulseBuffAmount = 5f;
    public float speedBuffAmount = 5f;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        // PotionEvent.OnGettingJumpPotion += OnApplyJumpBuff;
        // PotionEvent.OnGettingSpeedPotion += OnApplySpeedBuff;
    }

    public void OnApplyJumpBuff()
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
        playerController.jumpImpulse += jumpImpulseBuffAmount;
        while (_jumpBuffTimer < jumpBuffDuration)
        {
            _jumpBuffTimer += Time.deltaTime;
            yield return null;
        }
        playerController.jumpImpulse -= jumpImpulseBuffAmount;
        _jumpBuffCoroutine = null;
        _jumpBuffTimer = 0f;
    }

    public void OnApplySpeedBuff()
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
        playerController.walkSpeed += speedBuffAmount;
        while (_speedBuffTimer < speedBuffDuration)
        {
            _speedBuffTimer += Time.deltaTime;
            yield return null;
        }
        playerController.walkSpeed -= speedBuffAmount;
        _speedBuffCoroutine = null;
        _speedBuffTimer = 0f;
    }
}