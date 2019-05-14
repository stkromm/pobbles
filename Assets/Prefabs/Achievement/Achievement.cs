using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement
{
    public Sprite achievementIcon;
    public string achievementHeading;
    public string achievementDescription;
    public float achievementProgressSlider;

    public Achievement(Sprite icon, string heading, string description, float progress)
    {
        achievementIcon = icon;
        achievementHeading = heading;
        achievementDescription = description;
        achievementProgressSlider = progress;
    }
}
