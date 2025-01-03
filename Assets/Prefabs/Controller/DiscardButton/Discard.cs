using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Discard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public bool StoredHoverStatus { get; private set; }

    public void AddInput(InputAction input) {
        input.started += obj => {
            m_Image.enabled = true;
            m_Image.color = m_ColorNormal;
            StoredHoverStatus = false;
        };
        input.canceled += obj => {
            m_Image.enabled = false;
        };
    }

    private Image m_Image;
    [SerializeField] private Color m_ColorNormal = new(1, 1, 1, 0.5f);
    [SerializeField] private Color m_ColorHover = new(1, 1, 1, 1);

    private void Awake() {
        m_Image = GetComponent<Image>();
        m_Image.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (m_Image.enabled) {
            StoredHoverStatus = true;
            m_Image.color = m_ColorHover;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (m_Image.enabled) {
            StoredHoverStatus = false;
            m_Image.color = m_ColorNormal;
        }
    }
}
