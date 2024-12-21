using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Allow object to register to be called when have corresponding control request
/// </summary>
public abstract class IController:MonoBehaviour
{
    private Dictionary<ControlType, UnityAction<object>> _controlListener = new Dictionary<ControlType, UnityAction<object>>();

    public void RegisterControlTarget(ControlType controlType, UnityAction<object> callback)
    {
        if (!_controlListener.ContainsKey(controlType))
        {
            _controlListener[controlType] = new UnityAction<object>(callback);
            Debug.Log("Registering control listener");
        }
        else if (_controlListener[controlType] != null)
        {
            _controlListener[controlType] += callback;
        }
    }

    public void RemoveControlTarget(ControlType controlType, UnityAction<object> callback) {
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
    
    /// <summary>
    /// </summary>
    /// <param name="param">see ControlType's comment for the data type of the parameter</param>
    protected void PostControlCommand(ControlType controlType, object param = null)
    {
        if (!_controlListener.ContainsKey(controlType))
        {
            Debug.Log("Not containing control listener");
            return;
        }
        _controlListener[controlType].Invoke(param);
        Debug.Log("Posting control listener");
    }
}
