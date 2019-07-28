using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("AchievementCollection")]
public class AchievementContainer
{
    [XmlArray("Achievements")]
    [XmlArrayItem("Achievement")]
    public List<Achievement> Achievements = new List<Achievement>();

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(AchievementContainer));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static AchievementContainer Load(string path)
    {
#if UNITY_ANDROID
        var file = new WWW(path);
        while (!file.isDone) { }
        return LoadFromText(file.text);
#else

        var serializer = new XmlSerializer(typeof(AchievementContainer));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as AchievementContainer;
        }
#endif

    }

    //Loads the xml directly from the given string. Useful in combination with www.text.
    public static AchievementContainer LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(AchievementContainer));
        return serializer.Deserialize(new StringReader(text)) as AchievementContainer;
    }
}

