using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Authentication : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Login();
    }

    void Login() {
        Debug.Log("Try to login with GameCenter or Play");
        if (Application.platform == RuntimePlatform.Android) {
            // Android Phone
            Social.localUser.Authenticate(ProcessAuthentication);
        } else if (Application.platform == RuntimePlatform.IPhonePlayer) {
            // iOS Phone
            Social.localUser.Authenticate(ProcessAuthentication);
        }
    }

    void ProcessAuthentication(bool success) {
        if (success){
            string username = Social.localUser.userName;
            Debug.Log("Success! Logged in "+username+" with ID: "+Social.localUser.id);
        }else{
            Debug.Log("Failed to log in");
        }
    }
}
