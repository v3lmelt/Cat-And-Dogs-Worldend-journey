using System.Collections;
using TMPro;
using UnityEngine;

public class TextFadeoutEffect : MonoBehaviour
{
    public float timeToFade = 1.0f;
    public float moveRange = 200f;

    private float _timer;
    private TMP_Text _tmpText;

    private Color _color;
    private RectTransform _rectTransform;
    private void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();
        _rectTransform = GetComponent<RectTransform>();
        
        _color = _tmpText.color;
    }

    private void OnEnable()
    {
        StartCoroutine(FadeoutEffect());
    }

    private IEnumerator FadeoutEffect()
    {
        var startingColor = _color;
        var startingPos = _rectTransform.position;
        while (_timer < timeToFade)
        {
            _timer += Time.deltaTime;
            // 透明度的变换是线性插值
            _tmpText.color = new Color(startingColor.r, startingColor.g, startingColor.b,
                Mathf.Lerp(startingColor.a, 0, _timer / timeToFade));
            // 文本位置的移动也是线性插值
            _rectTransform.position = new Vector3(startingPos.x, Mathf.Lerp(
                startingPos.y, startingPos.y + moveRange, _timer / timeToFade), startingPos.z);
            yield return null;
        }
        Destroy(gameObject);
    }
}
