using System;
using UnityEngine;

/// <summary>
/// Infomation of animation clip.
/// It help to avoid the use hard-coded strings such as animation name, variable name to communicate with animator
/// </summary>
[Serializable]
public class AnimInfo {
    /// <summary>
    /// Name of animation.
    /// </summary>
    [field: SerializeField]
    public string Name { get; private set; }
    /// <summary>
    /// Hash of Name using Animator.StringToHash()
    /// </summary>
    public int HashName { get; }
    /// <summary>
    /// Name of variable that controls the play speed of animation.
    /// </summary>
    public string SpeedVarName => Name + "_Speed";
    public AnimInfo(string name) {
        Name = name;
        HashName = Animator.StringToHash(name);
    }
}
