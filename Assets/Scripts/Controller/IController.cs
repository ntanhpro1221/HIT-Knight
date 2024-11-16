using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Allow object to register to be called when have corresponding control request
/// </summary>
public abstract class IController
{
    private Dictionary<ControlType, UnityAction> _controlListener = new Dictionary<ControlType, UnityAction>();

    public void RegisterControlTarget(ControlType controlType, UnityAction callback)
    {
        if (!_controlListener.ContainsKey(controlType))
        {
            _controlListener[controlType] = new UnityAction(callback);
            Debug.Log("Registering control listener");
        }
        else if (_controlListener[controlType] != null)
        {
            _controlListener[controlType] += callback;
        }
    }

    public void RemoveControlTarget(ControlType controlType, UnityAction callback) {
        if (!_controlListener.ContainsKey(controlType)) {
            Debug.Log("Not containing control listener");
            return;
        }

        if (_controlListener[controlType] != null) {
            _controlListener[controlType] -= callback;
            Debug.Log("Removing control listener");
        } else if (_controlListener[controlType] == null) {
            _controlListener.Remove(controlType);
            Debug.Log("Removing control Type");
        }
    }
        
    protected void PostControlCommand(ControlType controlType)
    {
        if (!_controlListener.ContainsKey(controlType))
        {
            Debug.Log("Not containing control listener");
            return;
        }
        _controlListener[controlType].Invoke();
        Debug.Log("Posting control listener");
    }
}
