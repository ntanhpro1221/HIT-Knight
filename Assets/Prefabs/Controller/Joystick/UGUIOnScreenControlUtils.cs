using UnityEngine;

#if UNITY_EDITOR
#endif
////TODO: custom icon for EnhanceOnScreenStick component

namespace EnhancedOnCreenStick {
    public static class UGUIOnScreenControlUtils
    {
        public static RectTransform GetCanvasRectTransform(Transform transform)
        {
            var parentTransform = transform.parent;
            return parentTransform != null ? transform.parent.GetComponentInParent<RectTransform>() : null;
        }
    }
}