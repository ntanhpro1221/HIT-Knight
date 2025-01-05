using UnityEngine;

namespace sampleUserInterface
{
    public class UILobby : MonoBehaviour
    {
        [SerializeField] private GameObject panelLobby;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void ShowSelectHero()
        {
            ShowDisplay(false);
            UIController.Instance.UISelectHero.ShowDisplay(true);
        }

        public void ShowSelectRuneHero()
        {

        }

        public void ShowSelectWeapon()
        {
            ShowDisplay(false);
            UIController.Instance.UISelectWeapon.ShowDisplay(true);
        }

        public void ShowSelectRuneWeapon()
        {

        }

        public void ShowSelectMap()
        {
            ShowDisplay(false);
            UIController.Instance.UISelectMap.ShowDisplay(true);
        }

        public void ShowDisplay(bool enable)
        {
            panelLobby.SetActive(enable);
        }

        public void UpdateData(Sprite item)
        {
            _spriteRenderer.sprite = item;
        }
    }
}
