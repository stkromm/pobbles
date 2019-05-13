using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsDetails : MonoBehaviour
{
    public Image achievementIcon;
    public Text achievementHeading;
    public Text achievementDescription;
    public Slider achievementProgressSlider;

    private void Start()
    {
        InitAchievement("baseline_chat_white_48dp", "test name", "beschreibung");
    }

    //called upon initializing
    public void InitAchievement(string iconName, string heading, string description)
    {
        //load icon
        Debug.Log("AchievementIcons/"+iconName);
        achievementIcon.sprite = Resources.Load<Sprite>("AchievementIcons/" + iconName);
        //change heading
        achievementHeading.text = heading;
        //change description
        achievementDescription.text = description;
        //update progress
        LoadAchievementProgress(heading);
    }
    
    private void LoadAchievementProgress(string key)
    {
        //load progress from local db or server
        float progress = 0.5f;

        //adjust the progress bar
        achievementProgressSlider.value = progress;
    }
    


    }
