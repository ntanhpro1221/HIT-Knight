using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(OnSiteNavigator2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class DirectionVisualizer : MonoBehaviour {
    [SerializeField] private Color m_ColorNormal = Color.green;
    [SerializeField] private Color m_ColorDiscard = Color.red;
    private OnSiteNavigator2D m_Nav;
    private SpriteRenderer m_SR;
    private Discard m_Discard;

    private void Awake() {
        m_Nav = GetComponent<OnSiteNavigator2D>();
        m_SR = GetComponent<SpriteRenderer>();
        m_SR.enabled = false;
    }

    private void Update() {
        m_SR.color = m_Discard.StoredHoverStatus ? m_ColorDiscard : m_ColorNormal;
    }

    public Vector2 CurDir => m_Nav.CurDir;

    public void Init(Transform root, Discard discard) {
        transform.SetParent(root);
        transform.localPosition = new Vector3(0, 0, 0.1f);
        m_Discard = discard;
    }
    
    public void AddInput(InputAction input) {
        input.started += obj => m_SR.enabled = true;
        input.performed += obj => m_Nav.CurDir = obj.ReadValue<Vector2>();
        input.canceled += obj => m_SR.enabled = false;
    }
}
