using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif

public class SocialSignin
{
    public static bool IsAuthenticated()
    {
        return Social.localUser.authenticated;
    }

    public static void TrySignIn(System.Action<bool> callback)
    {
        Debug.Log("Entering Login");
        Social.localUser.Authenticate(callback);
        Debug.Log("Leaving Login");
    }
}
