using UnityEngine;

public class HealthBarSubmarine : HealthBar
{
    public string playerObjectName;
    private void Awake()
    {
        playerDamageable = GameObject.Find(playerObjectName).GetComponent<Damageable>();
        if (playerDamageable == null)
        {
            Debug.LogError("Have you forgot to set playerObjectName?");
        }
    }
}