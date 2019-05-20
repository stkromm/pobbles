using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementListController : MonoBehaviour
{
    ArrayList achievements;
    public GameObject content;
    public GameObject achievementListItem;
    AchievementLoader achievementLoader = new AchievementLoader();

    void Start()
    {

        //Get the data to be displayed
        achievements = achievementLoader.GetAchievements();

        //Iterate through the data
        foreach (Achievement achievement in achievements)
        {
            //create new achievement object
            GameObject newAchievement = Instantiate(achievementListItem) as GameObject;

            //set the data
            AchievementController controller = newAchievement.GetComponent<AchievementController>();
            controller.achievementIcon.sprite = achievement.achievementIcon;
            controller.achievementHeading.text = achievement.achievementHeading;
            controller.achievementDescription.text = achievement.achievementDescription;
            controller.achievementProgressSlider.value = achievement.achievementProgressSlider;

            //set the position 
            newAchievement.transform.parent = content.transform;
            newAchievement.transform.localScale = Vector3.one;

            //rename the heading and descirption text field to make it available for the translator
            controller.achievementHeading.name = achievement.achievementHeading + "Heading";
            controller.achievementDescription.name = achievement.achievementHeading + "Description";

        }

        AchievementWriter writer = new AchievementWriter();
        writer.WriteAchievementProgress("trophy", 0.2f);
    }
}
