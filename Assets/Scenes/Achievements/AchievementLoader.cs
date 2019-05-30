using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;

public class AchievementLoader : MonoBehaviour
{
    ArrayList achievements;
    private Settings settingsObject;
    private Hashtable Strings;

    public ArrayList GetAchievements()
    {
        //initialize the settings object to get the translation running
        settingsObject = Object.FindObjectOfType<Settings>();

        //get the path to the Achievements.xml file and Load it
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, "Achievements.xml");
        var achievementCollection = AchievementContainer.Load(System.IO.Path.Combine(Application.streamingAssetsPath, "Achievements.xml"));
        
        achievements = new ArrayList();
        foreach (Achievement achievement in achievementCollection.Achievements)
        {
            achievements.Add(new Achievement(settingsObject.GetStringFromHashtable(achievement.achievementHeading + "Heading"), settingsObject.GetStringFromHashtable(achievement.achievementHeading + "Description")));
        }
        return achievements;

    }
    
}
