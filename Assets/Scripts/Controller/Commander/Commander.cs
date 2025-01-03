using System;
using UnityEngine.Events;

/// <summary>
/// Allow object to register to be called when have corresponding control request
/// </summary>
public class Commander<TCommand> : CoreComponent where TCommand : Enum {
    private PropertySet<TCommand, UnityEvent<object>> OnPostCommand { get; } = new();

    public void AddListener(TCommand command, UnityAction<object> callback)
        => OnPostCommand[command].AddListener(callback);
    
    public void RemoveListener(TCommand command, UnityAction<object> callback)
        => OnPostCommand[command].RemoveListener(callback);

    /// <summary>
    /// </summary>
    /// <param name="param">see ControlType's comment for the data type of the parameter</param>
    public void PostCommand(TCommand command, object param = null)
        => OnPostCommand[command].Invoke(param);
}
