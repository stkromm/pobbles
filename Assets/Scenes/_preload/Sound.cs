using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO SKR 19-05-30 This design quickly will become unmaintainable as it will increase linear with new features.
// This Manager should (atleast for Music) accept Music and play it on demand. Then every Scene can use some simple 'PlayMusic' API to change the 
// background music on demand.
public class Sound : MonoBehaviour
{
    //setting variables
    private bool effectsEnabled;
    private bool musicEnabled;
    private int effectsVolume;
    private int musicVolume;

    public AudioSource globalEffectObject;
    public AudioClip bubblePop1;
    public AudioClip bubblePop2;
    public AudioClip negativeBubblePop;
    public AudioClip magneticBubblePop;
    public AudioClip freezeBubblePop;

    public AudioSource globalMusicObject;
    public AudioClip arcadeGameMusic;
    public AudioClip menuMusic;

    //string to check which song was played before stopping
    private string lastPlayedMusic;

    private void Awake()
    {
        //load stored sound settings
        if (PlayerPrefs.HasKey("effectsBool"))
        {
            // if so, load the stored value
            this.effectsEnabled = IntToBool(PlayerPrefs.GetInt("effectsBool"));
        }
        else
        {
            //else set to default (true)
            this.effectsEnabled = true;
        }

        if (PlayerPrefs.HasKey("musicBool"))
        {
            this.musicEnabled = IntToBool(PlayerPrefs.GetInt("musicBool"));
        }
        else
        {
            this.musicEnabled = true;
        }

        if (PlayerPrefs.HasKey("effectsValue"))
        {
            this.effectsVolume = PlayerPrefs.GetInt("effectsValue");
        }
        else
        {
            this.effectsVolume = 100;
        }

        if (PlayerPrefs.HasKey("musicValue"))
        {

            this.musicVolume = PlayerPrefs.GetInt("musicValue");
            Debug.Log("preset music value set to: " + this.musicVolume);
        }
        else
        {
            this.musicVolume = 50;
        }
        UpdateSoundSettings();
    }

    private void Start()
    {
        PlayMenuGameMusic();
    }

    public void SetEffectsEnabled(bool value)
    {
        this.effectsEnabled = value;
        //write effects bool as int into the playerPrefs
        PlayerPrefs.SetInt("effectsBool", effectsEnabled ? 1 : 0);
        UpdateSoundSettings();
    }

    public void SetEffectsVolume(int value)
    {
        this.effectsVolume = value;
        //write effects value as int into the playerPrefs
        PlayerPrefs.SetInt("effectsValue", effectsVolume);
        UpdateSoundSettings();
    }

    //get effects
    public bool GetEffectsBool()
    {
        return this.effectsEnabled;
    }

    public int GetEffectsValue()
    {
        return effectsVolume;
    }

    //set music
    public void SetMusicEnabled(bool enabled)
    {
        musicEnabled = enabled;
        //write music bool as int into the playerPrefs
        PlayerPrefs.SetInt("musicBool", musicEnabled ? 1 : 0);
        UpdateSoundSettings();
    }

    public void SetMusicVolume(int value)
    {
        musicVolume = value;
        //write music value as int into the playerPrefs
        PlayerPrefs.SetInt("musicValue", musicVolume);
        UpdateSoundSettings();
    }
    
    public bool IsMusicEnabled() => musicEnabled;

    public int GetMusicValue()
    {
        return musicVolume;
    }

    bool IntToBool(int value) => value != 0;

    public void PlayPopSound()
    {
        if (GetEffectsBool())
        {
            globalEffectObject.PlayOneShot(bubblePop1);

        }
    }

    public void PlayNegativePopSound()
    {
        if (GetEffectsBool())
        {

            globalEffectObject.PlayOneShot(negativeBubblePop);

        }
    }

    public void PlayMagneticPopSound()
    {
        if (GetEffectsBool())
        {

            globalEffectObject.PlayOneShot(magneticBubblePop);

        }
    }

    public void PlayFreezePopSound()
    {
        if (GetEffectsBool())
        {

            globalEffectObject.PlayOneShot(freezeBubblePop);

        }
    }

    public void PlayArcadeGameMusic()
    {
        if (IsMusicEnabled())
        {
            if (globalMusicObject.isPlaying && !(lastPlayedMusic == "arcade"))
            {
                globalMusicObject.Stop();
                ChooseTitle();
            }
            else if (!globalMusicObject.isPlaying)
            {
                ChooseTitle();
            }
        }
        lastPlayedMusic = "arcade";
    }

    private void ChooseTitle()
    {
        globalMusicObject.PlayOneShot(arcadeGameMusic);
    }

    //play menu music
    public void PlayMenuGameMusic()
    {
        if (IsMusicEnabled())
        {
            //if another music was played before, stop it and play the menu music. prevents a new start upon slider usage
            if (globalMusicObject.isPlaying && !(lastPlayedMusic == "menu"))
            {
                globalMusicObject.Stop();
                globalMusicObject.clip = menuMusic;
                globalMusicObject.Play();
            }
            //if there was no other music playing before, just start the menu music 
            else if (!globalMusicObject.isPlaying)
            {
                globalMusicObject.clip = menuMusic;
                globalMusicObject.Play();
            }
        }
        lastPlayedMusic = "menu";
    }

    void UpdateSoundSettings()
    {
        globalMusicObject.volume = 0.01f * GetMusicValue();
        globalEffectObject.volume = 0.01f * GetEffectsValue();
        
        if (!IsMusicEnabled())
        {
            globalMusicObject.Stop();
        }//check if menu music was played before
        else if (lastPlayedMusic == "menu")
        {
            PlayMenuGameMusic();
        }//check if arcade music was played before
        else if (lastPlayedMusic == "arcade")
        {
            PlayArcadeGameMusic();
        }
    }
}
