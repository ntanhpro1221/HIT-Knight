using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Allow object to register to be called when have corresponding control request
/// </summary>
public abstract class IController {
    public void RegisterControlTarget(ControlType controlType, UnityAction callback) {
        
    }
    public void RemoveControlTarget(ControlType controlType, UnityAction callback) {
        
    }
    protected void PostControlCommand(ControlType controlType) {
        
    }
}
