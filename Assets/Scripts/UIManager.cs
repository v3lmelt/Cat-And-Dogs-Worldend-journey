using System;
using TMPro;
using UnityEngine;
using Enums;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;

    public Canvas gameCanvas;

    public Vector3 damageTextSpawnDelta = new Vector3(1.5f, 0, 0);

    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed;
    }

    public void CharacterTookDamage(GameObject character, int damageReceived, DamageType damageType)
    {
        if (Camera.main == null) return;
        var spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position + damageTextSpawnDelta);

        var tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tmpText.text = damageReceived.ToString();

        // ��ȡ HeathText ���
        HeathText heathText = tmpText.GetComponent<HeathText>();

        switch (damageType)
        {
            case DamageType.Melee:
                heathText.SetStartColor(Color.red);
                break;
            case DamageType.Ranged:
                heathText.SetStartColor(Color.blue);
                break;
            // �����˺����͵Ĵ���
            default:
                throw new ArgumentOutOfRangeException(nameof(damageType), damageType, null);
        }
    }

    public void CharacterHealed(GameObject character, int healthRestored)
    {
        if (Camera.main == null) return;
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tmpText.text = healthRestored.ToString();
    }
}
