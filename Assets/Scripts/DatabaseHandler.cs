using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Linq;


public class DatabaseHandler : MonoBehaviour
{
    public Text feedbackText;
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    // Initialize the Firebase database:
    protected virtual void InitializeFirebase()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        app.SetEditorDatabaseUrl("https://pobbles-dev.firebaseio.com/");
        if (app.Options.DatabaseUrl != null)
            app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);
    }

    public void WriteNewFeedback()
    {
        string feedback = feedbackText.text;
        if(feedback.Length == 0){
            return;
        }
        Debug.Log("FeedbackText:"+feedback);
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        string key = reference.Child("feedback").Push().Key;
        reference.Child("feedback").Child(key).SetValueAsync(feedback);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

// CUSTOM CLASSES

// Feedback
public class Feedback
{
    public string feedbackText;

    public Feedback()
    {
    }

    public Feedback(string feedbackText)
    {
        this.feedbackText = feedbackText;
    }
}
