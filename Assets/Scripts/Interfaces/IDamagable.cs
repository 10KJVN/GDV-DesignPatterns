public interface IDamagable
{
    void TakeDamage(int damage);
    void ApplyEffect(IEffect<IDamagable> effect);
}