using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackPotion : MonoBehaviour
{
    public int damageIncreaseAmount = 5;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 只有玩家才能发生碰撞
        if (!TagUtil.ComparePlayerTag(other.gameObject)) return;
        PlayerStatUtil.IncreaseAttackDamage(damageIncreaseAmount);
        TextManager.Instance.OnCreatingPotionText(other.transform.position, "Damage + " + damageIncreaseAmount
        + "MagicDamage" + damageIncreaseAmount*2 + "!");
        
        Destroy(gameObject);
    }
}
