//using System;

//[Serializable]
//public class DamageEffect : IEffect<IDamagable>
//{
//    public int damageAmount = 10;

//    public void Apply(IDamagable target)
//    {
//        target.TakeDamage(damageAmount);
//    }

//    public void Cancel()
//    {
//        // no-op
//    }

//    public event Action<IEffect<IDamagable>> OnCompleted;
//}