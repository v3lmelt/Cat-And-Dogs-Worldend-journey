using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;

    public TMP_Text healthBarText;

    protected Damageable playerDamageable;

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
        healthSlider.value = CalculateSLiderPercentage(playerDamageable.Health,playerDamageable.MaxHealth);
        healthBarText.text = "HP" + playerDamageable.Health + "/" + playerDamageable.MaxHealth;
    }

    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnplayerHealthChanged);
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnplayerHealthChanged);
    }

    private float CalculateSLiderPercentage(float currentHealth,float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    private void OnplayerHealthChanged(int newHealth,int maxHealth)
    {
        healthSlider.value = CalculateSLiderPercentage(newHealth, maxHealth);
        healthBarText.text = "HP" + newHealth + "/" + maxHealth;
    }


}
