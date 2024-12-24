using System;
using System.Collections.Generic;
using UnityEngine;

public class Alice : MonoBehaviour {
    Rigidbody2D rb;
    public float speed = 5;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            rb.velocity = Vector2.left * speed;
            print(rb.velocity);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            rb.velocity = Vector2.right * speed;
            print(rb.velocity);
        }
    }
}
