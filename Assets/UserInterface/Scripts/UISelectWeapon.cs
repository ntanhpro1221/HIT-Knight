using System.Collections.Generic;
using UnityEngine;

namespace sampleUserInterface
{
    public class UISelectWeapon : UISelectBase<Sprite>
    {
        [SerializeField] private List<Sprite> weapomDatas = new List<Sprite>();

        private void Start()
        {
            Initialize(weapomDatas);
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
            Debug.Log($"Weapon Selected: {item.name}");
        }
    }
}