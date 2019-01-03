using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {

    //setting variables
    private bool effectsBool;
    private bool musicBool;
    private int effectsValue;
    private int musicValue;

    //Sound effect object and the different clips
    public AudioSource globalEffectObject;
    public AudioClip BubblePop1;
    public AudioClip BubblePop2;
    public AudioClip BubblePop3;

    //Music object and a clip
    public AudioSource globalMusicObject;
    public AudioClip arcadeGameMusic;
    public AudioClip menuMusic;

    //string to check which song was played before stopping
    private string musicState;

    private void Awake()
    {
        //load stored sound settings
        //check if key exists
        if (PlayerPrefs.HasKey("effectsBool"))
        {
            // if so, load the stored value
            this.effectsBool = IntToBool(PlayerPrefs.GetInt("effectsBool"));
        }
        else
        {
            //else set to default (true)
            this.effectsBool = true;
        }

        if (PlayerPrefs.HasKey("musicBool"))
        {
            this.musicBool = IntToBool(PlayerPrefs.GetInt("musicBool"));
        }
        else
        {
            this.musicBool = true;
        }

        if (PlayerPrefs.HasKey("effectsValue"))
        {
            this.effectsValue = PlayerPrefs.GetInt("effectsValue");
        }
        else
        {
            this.effectsValue = 100;
        }

        if (PlayerPrefs.HasKey("musicValue"))
        {
            this.musicValue = PlayerPrefs.GetInt("musicValue");
        }
        else
        {
            this.musicValue = 100;
        }
        
    }

    //play the menu soundtrack
    private void Start()
    {
        PlayMenuGameMusic();
    }

    //set effects
    public void SetEffectsBool(bool value)
    {
        this.effectsBool = value;
        //write effects bool as int into the playerPrefs
        PlayerPrefs.SetInt("effectsBool", effectsBool ? 1 : 0);
        UpdateSoundSettings();
    }

    public void SetEffectsValue(int value)
    {
        this.effectsValue = value;
        //write effects value as int into the playerPrefs
        PlayerPrefs.SetInt("effectsValue", effectsValue);
        UpdateSoundSettings();
    }

    //get effects
    public bool GetEffectsBool()
    {
        return this.effectsBool;
    }
    
    public int GetEffectsValue()
    {
        return this.effectsValue;
    }

    //set music
    public void SetMusicBool(bool value)
    {
        this.musicBool = value;
        //write music bool as int into the playerPrefs
        PlayerPrefs.SetInt("musicBool", musicBool ? 1 : 0);
        UpdateSoundSettings();
    }

    public void SetMusicValue(int value)
    {
        this.musicValue = value;
        //write music value as int into the playerPrefs
        PlayerPrefs.SetInt("musicValue", musicValue);
        UpdateSoundSettings();
    }

    //get music
    public bool GetMusicBool()
    {
        return this.musicBool;
    }

    public int GetMusicValue()
    {
        return this.musicValue;
    }

    bool IntToBool(int value)
    {
        if (value == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //play pop sound
    public void PlayPopSound()
    {
        
        //check if effects are enabled
        if (GetEffectsBool())
        {
            //play sound
            float rnd = Random.Range(0.0f, 1.0f);
            if (rnd <= 0.33f)
            {
                globalEffectObject.PlayOneShot(BubblePop1);
            } else if (rnd >= 0.66f)
            {
                globalEffectObject.PlayOneShot(BubblePop3);
            } else
            {
                globalEffectObject.PlayOneShot(BubblePop2);
            }
        }
    }

    //play arcade game music
    public void PlayArcadeGameMusic()
    {
        //start playing, if music enabled
        if (GetMusicBool())
        {
            if (globalMusicObject.isPlaying && !(musicState=="arcade"))
            {
                globalMusicObject.Stop();
                globalMusicObject.PlayOneShot(arcadeGameMusic);
            }
            else if (!globalMusicObject.isPlaying)
            {
                globalMusicObject.PlayOneShot(arcadeGameMusic);
            }
        }
        musicState = "arcade";
    }

    //play menu music
    public void PlayMenuGameMusic()
    {
        
        
        if (GetMusicBool())
        { 
            //if another music was played before, stop it and play the menu music. prevents a new start upon slider usage
            if (globalMusicObject.isPlaying && !(musicState == "menu"))
            {
                globalMusicObject.Stop();
                globalMusicObject.PlayOneShot(menuMusic);
            }
            //if there was no other music playing before, just start the menu music 
            else if (!globalMusicObject.isPlaying)
            {
                globalMusicObject.PlayOneShot(menuMusic);
            }
        }
        musicState = "menu";
    }

    void UpdateSoundSettings()
    {
        globalMusicObject.volume = 0.01f * GetMusicValue();
        globalEffectObject.volume = 0.01f * GetEffectsValue();

        //check if music is enabled/disabled
        if (!GetMusicBool())
        {
            globalMusicObject.Stop();
        }//check if menu music was played before
        else if (musicState == "menu")
        {
            PlayMenuGameMusic();
        }//check if arcade music was played before
        else if (musicState == "arcade")
        {
            PlayArcadeGameMusic();
        }
    }
}
