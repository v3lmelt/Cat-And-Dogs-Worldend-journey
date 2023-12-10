public interface IDamageable
{
    public void OnDamageHit();
    public void OnDeath();
    public void OnHealthChanged();
    public void OnMpChanged();

    public void HealthChanged(int changeAmt);
    public void MpChanged(int changeAmt);

    public int Health { get; set; }
    public int Mp { get; set; }

    public int MaxHealth { get; set; }
    public int MaxMp { get; set; }
}