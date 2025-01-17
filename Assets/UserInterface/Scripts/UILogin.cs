using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace sampleUserInterface
{
    public class UILogin : MonoBehaviour
    {
        [SerializeField] private GameObject panelLogin;

        [Space] [Header("Hover")] [SerializeField]
        private GameObject hoverButton;

        [SerializeField] private GameObject hoverInput;

        [Space] [Header("InputField")] [SerializeField]
        private TMP_InputField txtAccount;

        [SerializeField] private TMP_InputField txtPassword;

        public void ClickLogin()
        {
            hoverButton.SetActive(false);
            hoverInput.SetActive(true);
        }

        public void ClickBack()
        {
            hoverButton.SetActive(true);
            hoverInput.SetActive(false);
        }

        public void ClickSignUp()
        {
            if (txtAccount.text == "Admin" && txtPassword.text == "Admin")
            {
                ShowDisplay(false);
                UIController.Instance.UILobby.ShowDisplay(true);
            }
        }

        public void ShowDisplay(bool enable)
        {
            panelLogin.SetActive(enable);
        }
    }
}
