using UnityEngine;

[ExecuteInEditMode]
public class Coord3DSimilator : MonoBehaviour {
    private void LateUpdate() {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.y);
    }
}
