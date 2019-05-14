using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementListController : MonoBehaviour
{
    ArrayList achievements;
    public GameObject content;
    public GameObject achievementListItem;

    void Start()
    {

        //Get the data to be displayed
        achievements = new ArrayList(){
             new Achievement(Resources.Load<Sprite>("AchievementIcons/testtest"), "Test", "Test beschr.",0.3f ),
             new Achievement(Resources.Load<Sprite>("AchievementIcons/testtest"), "Test", "Test beschr.",0.5f ),
             new Achievement(Resources.Load<Sprite>("AchievementIcons/testtest"), "Test", "Test beschr.",0.7f ),
             new Achievement(Resources.Load<Sprite>("AchievementIcons/testtest"), "Test", "Test beschr.",0.7f ),
             new Achievement(Resources.Load<Sprite>("AchievementIcons/testtest"), "Test", "Test beschr.",0.7f ),
             new Achievement(Resources.Load<Sprite>("AchievementIcons/testtest"), "Test", "Test beschr.",0.7f ),
             new Achievement(Resources.Load<Sprite>("AchievementIcons/testtest"), "Test", "Test beschr.",0.7f ),
             new Achievement(Resources.Load<Sprite>("AchievementIcons/testtest"), "Test", "Test beschr.",0.7f ),
             new Achievement(Resources.Load<Sprite>("AchievementIcons/testtest"), "Test", "Test beschr.",0.7f ),
             new Achievement(Resources.Load<Sprite>("AchievementIcons/testtest"), "Test", "Test beschr.",0.9f )
        };

        // 2. Iterate through the data, 
        //	  instantiate prefab, 
        //	  set the data, 
        //	  add it to panel
        foreach (Achievement achievement in achievements)
        {
            GameObject newAchievement = Instantiate(achievementListItem) as GameObject;
            AchievementController controller = newAchievement.GetComponent<AchievementController>();
            controller.achievementIcon.sprite = achievement.achievementIcon;
            controller.achievementHeading.text = achievement.achievementHeading;
            controller.achievementDescription.text = achievement.achievementDescription;
            controller.achievementProgressSlider.value = achievement.achievementProgressSlider;

            newAchievement.transform.parent = content.transform;
            newAchievement.transform.localScale = Vector3.one;
        }
    }
}
