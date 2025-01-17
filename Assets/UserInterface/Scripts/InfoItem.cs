using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoItem : MonoBehaviour
{
    public Image img;
    public TextMeshProUGUI txtInfo;
    public Button btn;

    public void Reset()
    {
        img = transform.GetChild(0).GetComponent<Image>();
        txtInfo = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        btn = transform.GetChild(2).GetComponent<Button>();
    }
}
