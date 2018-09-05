using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocalizedText : MonoBehaviour {

    public string key;

    // Use this for initialization
    void Start () 
    {
        if (!LocalizationManager.instance) {
            Debug.LogError("Localization Manager not loaded!");
            return;
        }
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI> ();
        text.text = LocalizationManager.instance.GetLocalizedValue (key);
    }

}