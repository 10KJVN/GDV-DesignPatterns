using System;

public interface IEffect
{
    void Apply(IDamagable target);
    //void Cancel();
    //event Action<IEffect<TTarget>> OnCompleted;
}