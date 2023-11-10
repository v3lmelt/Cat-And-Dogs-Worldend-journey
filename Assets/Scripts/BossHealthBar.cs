using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Image hpImg;
    public Image hpEffectImg;

    public float maxHp = 100f;
    public float currentHp = 100f;
    public float buffTime = 0.5f;

    private Coroutine _updateCoroutine;

    private void Start()
    {
        currentHp = maxHp;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        hpImg.fillAmount = currentHp / maxHp;
        if (_updateCoroutine != null)
        {
            StopCoroutine(_updateCoroutine);
        }

        _updateCoroutine = StartCoroutine(UpdateHealthEffect());
    }

    public void SetHealth(float health)
    {
        currentHp = Mathf.Clamp(health, 0f, maxHp);
        UpdateHealthBar();
    }

    public void ChangeHealth(float amount)
    {
        SetHealth(currentHp + amount);
    }
    
    private IEnumerator UpdateHealthEffect()
    {
        var effectLength = hpEffectImg.fillAmount - hpImg.fillAmount;
        var elapsedTime = 0f;

        while (elapsedTime < buffTime && effectLength != 0)
        {
            elapsedTime += Time.deltaTime;
            hpEffectImg.fillAmount =
                Mathf.Lerp(hpImg.fillAmount + effectLength, hpImg.fillAmount, elapsedTime / buffTime);
            yield return null;
        }
        hpEffectImg.fillAmount = hpImg.fillAmount;
    }
}
