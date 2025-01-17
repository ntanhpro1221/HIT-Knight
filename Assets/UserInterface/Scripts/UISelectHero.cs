using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace sampleUserInterface
{
    public class UISelectHero : UISelectBase<Sprite>
    {
        [SerializeField] private List<Sprite> heroDatas = new List<Sprite>();
        [SerializeField] private Image imgSelectHero;
        //private datahero .....

        private void Start()
        {
            Initialize(heroDatas);
        }

        protected override string GetName(Sprite item)
        {
            return item.name;
        }

        protected override Sprite GetSprite(Sprite item)
        {
            return item;
        }

        protected override void OnItemSelected(Sprite item)
        {
            Debug.Log($"Hero Selected: {item.name}");
            imgSelectHero.sprite = item;
        }

        public void ClickChoice()
        {
            // update data hero qua lobby
             UIController.Instance.UILobby.UpdateData(imgSelectHero.sprite);
            UIController.Instance.UILobby.ShowDisplay(true);
            ShowDisplay(false);
            
            
        }
    }
}
