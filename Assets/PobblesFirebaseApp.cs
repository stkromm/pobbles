using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class PobblesFirebaseApp : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        FirebaseApp.LogLevel = LogLevel.Verbose;
        FirebaseDatabase.DefaultInstance.LogLevel = LogLevel.Verbose;
        // NOTE: You'll need to replace this url with your Firebase App's database
        // path in order for the database connection to work correctly in editor.
        app.SetEditorDatabaseUrl("https://pobbles-dev.firebaseio.com");
        if (app.Options.DatabaseUrl != null)
            app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        reference.Child("Test").SetValueAsync("Hello World!");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
