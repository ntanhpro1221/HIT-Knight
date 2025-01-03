using System;
using UnityEngine;

public abstract class IActorControlHelper : CoreComponent {
    public abstract bool NoCommand { get; protected set; }
    public abstract Vector2 MoveByDir { get; protected set; }
    public abstract Vector2? MoveByPos { get; protected set; }
    public abstract bool Attack { get; protected set; }
    public abstract Vector2 Dash { get; protected set; } // Trigger command
}