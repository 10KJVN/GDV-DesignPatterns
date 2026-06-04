public struct CombatContext
{
    public object Caster { get; }
    public object Target { get; }
    
    public CombatContext (object caster, object target)
    {
        Caster = caster;
        Target = target;
    }
}
