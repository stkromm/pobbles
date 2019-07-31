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
#if UNITY_ANDROID
        Debug.Log("Setting up PlayGamePlatform");
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .RequestServerAuthCode(false)
            .RequestIdToken()
            .Build();
        Debug.Log("Config: " + config.ToString());
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Debug.Log("PlayGamePlatform Activated");
#endif
        Social.localUser.Authenticate(callback);
        Debug.Log("Leaving Login");
    }
}
