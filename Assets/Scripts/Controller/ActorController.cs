﻿using UnityEngine;
using UnityEngine.Events;

public class ActorController : IActorController {
    public void SetAttackListener(UnityAction callback) {
        throw new System.NotImplementedException();
    }

    public void SetDashListener(UnityAction callback) {
        throw new System.NotImplementedException();
    }

    public void SetMovementListener(UnityAction<Vector2> callback) {
        throw new System.NotImplementedException();
    }
}
