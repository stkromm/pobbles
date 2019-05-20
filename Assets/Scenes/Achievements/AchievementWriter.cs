using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;

public class AchievementWriter : MonoBehaviour
{
    public void WriteAchievementProgress(string name, float progress)
    {
        PlayerPrefs.SetFloat(name, progress);
    }
    
}
