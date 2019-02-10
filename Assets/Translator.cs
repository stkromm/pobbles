using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Translator : MonoBehaviour
{
    private Settings settingsObject;
    private string currentLang;
    Text[] texts;

    // Start is called before the first frame update
    void Awake()
    {
        settingsObject = GameObject.FindObjectOfType<Settings>();
        Translate(settingsObject.GetLanguage());
    }
    
    private void Translate(string language)
    {
        currentLang = language;
        Debug.Log("the langauge is : " + language);
        texts = gameObject.GetComponentsInChildren<Text>();
        foreach (Text text in texts){
            //only change the text, if there is a value stored in the hashtable
            Debug.Log("current key: " + text.name);
            Debug.Log("passed value: " + settingsObject.GetStringFromHashtable(text.name));
            if (settingsObject.GetStringFromHashtable(text.name)!="")
            {
                text.text = settingsObject.GetStringFromHashtable(text.name);
            }
            
        }
        
    }
    

    private void Update()
    {
        //check for language changes and update if language has changed
        if (currentLang != settingsObject.GetLanguage())
        {
            Translate(settingsObject.GetLanguage());
        }
    }
}
