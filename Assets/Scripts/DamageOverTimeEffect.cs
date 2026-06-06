using System;
using ImprovedTimers;

[Serializable]
public class DamageOverTimeEffect : IEffect<IDamagable>
{
    public float duration = 5f;
    public float tickInterval = 1.0f;
    public int damagePerTick;

    private IntervalTimer _timer;
    private IDamagable _currentTarget;
    
    public void Apply(IDamagable target)
    {
        _currentTarget = target;
        _timer = new IntervalTimer(duration, tickInterval);
        _timer.OnInterval = OnInterval;
        _timer.OnTimerStop = OnStop;
        _timer.Start();
    }
    
    void OnInterval() => _currentTarget?.TakeDamage(damagePerTick);
    void OnStop() => Cleanup();

    public void Cancel()
    {
        _timer?.Stop();
        Cleanup();
    }

    public event Action<IEffect<IDamagable>> OnCompleted;

    // TODO: Improve by caching timers.
    private void Cleanup()
    {
        _timer = null; 
        _currentTarget = null;
    }
}