using UnityEngine;

public class Bob : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
        print(GetType().Name + " Collision " + Time.time);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        print(GetType().Name + " trigger " + Time.time);
    }
}

