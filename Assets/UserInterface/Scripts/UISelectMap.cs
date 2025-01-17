using UnityEngine;

namespace sampleUserInterface
{
    public class UISelectMap : MonoBehaviour
    {
        [SerializeField] private GameObject panelSelectMap;

        public void ShowDisplay(bool enable)
        {
            panelSelectMap.SetActive(enable);
        }

        public void ClickBack()
        {
            ShowDisplay(false);
            UIController.Instance.UILobby.ShowDisplay(true);
        }
    }
}
