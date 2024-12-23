using UnityEngine;

public static class Util {
    public static string GetHierarchyPath(GameObject obj) {
        try {
            string path = obj.name;
            while (obj.transform.parent != null) {
                obj = obj.transform.parent.gameObject;
                path = obj.name + "/" + path;
            }
            path = obj.scene.name + ": " + path;
            return path;
        } catch {
            return null;
        }
    }
    public static void LogWarning_WithAddress(GameObject objAddress, string mes) {
        Debug.LogWarning(GetHierarchyPath(objAddress) + " --> " + mes);
    }
}