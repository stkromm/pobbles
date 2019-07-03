using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Unity.Editor;
using System.Threading.Tasks;
using System;

public class PobblesFirebaseApp : MonoBehaviour { 
    private bool isInitialized = false;

    void Start()
    {
        PobblesFirebaseApp[] lights = (PobblesFirebaseApp[])GameObject.FindObjectsOfType(typeof(PobblesFirebaseApp));
        if (lights.Length > 1)
        {
            Destroy(this);
        }
    }
}
