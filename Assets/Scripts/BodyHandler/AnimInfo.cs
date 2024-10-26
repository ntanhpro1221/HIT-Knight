﻿using System;

/// <summary>
/// Infomation of animation clip.
/// It help to avoid the use hard-coded strings such as animation name, variable name to communicate with animator
/// </summary>
[Serializable]
public class AnimInfo {
    /// <summary>
    /// Name of animation.
    /// </summary>
    public string name;
    /// <summary>
    /// Name of variable that controls the play speed of animation.
    /// </summary>
    public string speedVar;
}