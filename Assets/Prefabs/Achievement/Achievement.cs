using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;

public class Achievement
{
    
    //read from XML file
    [XmlAttribute("heading")]
    public string achievementHeading;

    //added later based on the read key
    public string achievementDescription;
    public float achievementProgressSlider;
    public Sprite achievementIcon;


    public Achievement(){}

    public Achievement(string key, string heading, string description)
    {
        //available on init from xml
        achievementHeading = heading;
        achievementDescription = description;
        Debug.Log("achievement heading: " + heading);
        achievementIcon = Resources.Load<Sprite>("AchievementIcons/" + key);
        achievementProgressSlider = LoadAchievementProgress(key);
    }

    public float LoadAchievementProgress(string name)
    {
        //check if there is a progress saved for the acievement
        if (PlayerPrefs.HasKey(name))
        {
            return PlayerPrefs.GetFloat(name);
        }
        //if nothing was saved for the achievement yet
        else
        {
            return 0f;
        }


    }
}
