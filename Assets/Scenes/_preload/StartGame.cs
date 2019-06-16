using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (Settings)), RequireComponent(typeof (DDOL)), RequireComponent(typeof (PobblesFirebaseApp))]
public class StartGame : MonoBehaviour {
    Settings settingsObject;
    PobblesFirebaseApp firebaseApp;
    const string FIREBASE_URL = "https://pobbles-dev.firebaseio.com";
    bool init = false;
    
    void Start ()
    {
        settingsObject = GetComponent<Settings>();
        firebaseApp = GetComponent<PobblesFirebaseApp>();
        Init();
    }

    async void Init()
    {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            FirebaseApp.LogLevel = LogLevel.Verbose;
            FirebaseDatabase.DefaultInstance.LogLevel = LogLevel.Verbose;
            app.SetEditorDatabaseUrl(FIREBASE_URL);
            if (app.Options.DatabaseUrl != null)
                app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);
            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
            if (auth.CurrentUser == null)
            {
                try
                {
                    var user = await auth.SignInAnonymouslyAsync();

                    Debug.LogFormat("User signed in successfully: {0} ({1})",
                    user.DisplayName, user.UserId);
                    init = true;
                }
                catch (Exception e)
                {
                    init = true;

                }
        }
        init = true;
    }

    void Update()
    {
        if (init)
        {
            SceneManager.LoadScene("MainMenu");
        }        
    }

    public void PlayGame()
    {
        if (settingsObject.GetIntroBool())
        {
            SceneManager.LoadScene("Introduction");
        }
        else
        {
            SceneManager.LoadScene("GamemodeArcade");
        }
    }
}
