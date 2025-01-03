using System;

public abstract class IWeaponControlHelper : CoreComponent {
    public abstract bool NoCommand { get; protected set; }
    public abstract bool StopAttack { get; protected set; }
    public abstract bool MeleeAttack { get; protected set; }
    public abstract bool RangedAttack { get; protected set; }
    /// <summary>
    /// Attach with Input System
    /// </summary>
    public abstract void Init(Action onHaveTriggerdCommand);
    /// <summary>
    /// Attach with commander
    /// </summary>
    public abstract void Init(Action onHaveTriggerdCommand, WeaponCommander commander);
    /// <summary>
    /// Un attach with commander (definitely only use it when you Attached with commander)
    /// </summary>
    public abstract void UnbindCommander(Action onHaveTriggerdCommand, WeaponCommander commander);
}


