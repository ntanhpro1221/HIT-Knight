using System.Collections.Generic;
using UnityEngine;


namespace sampleUserInterface
{
    public abstract class UISelectBase<T> : MonoBehaviour
    {
        [SerializeField] private GameObject panelSelect;
        [SerializeField] private InfoItem itemPrefab;
        [SerializeField] private Transform contentTransform;

        protected List<T> items;


        public void Initialize(List<T> data)
        {
            items = data;
            PopulateScrollView();
        }

        private void PopulateScrollView()
        {
            foreach (T item in items)
            {
                InfoItem newItem = Instantiate(itemPrefab, contentTransform);
                newItem.name = GetName(item);
                newItem.img.sprite = GetSprite(item);
                newItem.btn.onClick.AddListener(() => OnItemSelected(item));
            }
        }

        public void ShowDisplay(bool enable)
        {
            panelSelect.SetActive(enable);
        }

        public void ClickBack()
        {
            ShowDisplay(false);
            UIController.Instance.UILobby.ShowDisplay(true);
        }
        protected abstract string GetName(T item);
        protected abstract Sprite GetSprite(T item);
        protected abstract void OnItemSelected(T item);
    }
}