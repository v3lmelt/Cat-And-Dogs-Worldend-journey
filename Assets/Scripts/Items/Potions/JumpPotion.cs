using UnityEngine;
public class JumpPotion : MonoBehaviour
{
    private Collider2D _collider2D;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!Util.ComparePlayerTag(other.gameObject)) return;
        TextManager.Instance.OnCreatingPotionText(other.transform.position, "Jump Potion!");
        PotionEvent.OnGettingJumpPotion.Invoke();
    }
}