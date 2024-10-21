using System;
using System.Collections.Generic;
using UnityEngine;

public enum KeyNum {
    One,
    Two,
    Three
}

[Serializable]
public class Bob {
    public string a;
    public string b;
}

public class Alice : MonoBehaviour {
    public PropertySet<KeyNum, Bob> dict;
    private void Start() {

    }
}
