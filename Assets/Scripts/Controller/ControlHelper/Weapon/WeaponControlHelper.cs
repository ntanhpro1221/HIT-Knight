using System;

public class WeaponControlHelper : IWeaponControlHelper {
    private Action onHaveTriggeredCommand;

    private void StopAttack_Callback(object param) {
        StopAttack = true;
        onHaveTriggeredCommand.Invoke();
        StopAttack = false;
    }

    private void MeleeAttack_Callback(object param) {
        MeleeAttack = true;
        onHaveTriggeredCommand.Invoke();
        MeleeAttack = false;
    }

    private void RangedAttack_Callback(object param) {
        RangedAttack = true;
        onHaveTriggeredCommand.Invoke();
        RangedAttack = false;
    }
    
    public override bool NoCommand {
        get =>
            StopAttack == default &&
            MeleeAttack == default &&
            RangedAttack == default;
        protected set { }
    }

    public override bool StopAttack { get; protected set; }

    public override bool MeleeAttack { get; protected set; }

    public override bool RangedAttack { get; protected set; }

    public override void Init(Action onHaveTriggerdCommand) { }

    public override void Init(Action onHaveTriggeredCommand, WeaponCommander commander) {
        this.onHaveTriggeredCommand = onHaveTriggeredCommand;
        commander.AddListener(WeaponCommand.StopAttack, StopAttack_Callback);
        commander.AddListener(WeaponCommand.MeleeAttack, MeleeAttack_Callback);
        commander.AddListener(WeaponCommand.RangedAttack, RangedAttack_Callback);
    }

    public override void UnbindCommander(Action onHaveTriggerdCommand, WeaponCommander commander) {
        commander.RemoveListener(WeaponCommand.StopAttack, StopAttack_Callback);
        commander.RemoveListener(WeaponCommand.MeleeAttack, MeleeAttack_Callback);
        commander.RemoveListener(WeaponCommand.RangedAttack, RangedAttack_Callback);
    }
}

