using UnityEngine;

public abstract class Navigator2D : Navigator {
    private bool isLeft = false;
    public override Vector2 CurDir {
        get => Root.right; 
        set {
            if (value == Vector2.zero) return;

            if (value.x != 0) isLeft = value.x < 0;

            float angle = Vector2.SignedAngle(Vector2.right, value);
            Root.eulerAngles = isLeft 
                ? new Vector3(0, 180, 180 - angle)
                : new Vector3(0, 0, angle);
        }
    }
}
