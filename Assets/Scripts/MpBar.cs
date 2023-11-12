using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MpBar : MonoBehaviour
{
    public Slider mpSlider;

    public TMP_Text mpBarText;

    Damageable playerDamageable;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("No player found in the scene. Make sure it has tag 'Player'");
        }
        playerDamageable = player.GetComponent<Damageable>();
    }
    // Start is called before the first frame update
    void Start()
    {
        mpSlider.value = CalculateMpSliderPercentage(playerDamageable.MP, playerDamageable.MaxMP);
        mpBarText.text = "MP " + playerDamageable.MP + "/" + playerDamageable.MaxMP;
    }

    private void OnEnable()
    {
        playerDamageable.mpChanged.AddListener(OnPlayerMPChanged);
    }

    private void OnDisable()
    {
        playerDamageable.mpChanged.RemoveListener(OnPlayerMPChanged);
    }

    private float CalculateMpSliderPercentage(float currentMp, float maxMp)
    {
        return currentMp / maxMp;
    }

    private void OnPlayerMPChanged(int newMP, int maxMP)
    {
        mpSlider.value = CalculateMpSliderPercentage(newMP, maxMP);
        mpBarText.text = "MP" + newMP + "/" + maxMP;
    }


}
