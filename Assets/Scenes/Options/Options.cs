using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {
    public Toggle effectsToggle;
    public Toggle musicToggle;
    public Toggle introToggle;
    public Slider effectsSlider;
    public Slider musicSlider;
    public Dropdown languageDropdown;
    private Sound soundObject;
    private Settings settingsObject;

	// Use this for initialization
	void Start () {
        //load saved sound settings
        this.soundObject = Object.FindObjectOfType<Sound>();
        this.settingsObject = Object.FindObjectOfType<Settings>();
        effectsToggle.isOn = soundObject.GetEffectsBool();
        musicToggle.isOn = soundObject.IsMusicEnabled();
        introToggle.isOn = settingsObject.GetIntroBool();
        effectsSlider.value = soundObject.GetEffectsValue();
        musicSlider.value = soundObject.GetMusicValue();

        int counter = 0;
        foreach (Dropdown.OptionData option in languageDropdown.options)
        {
            if (option.text == settingsObject.GetLanguage())
            {
                Debug.Log("counter in if: " + counter);
                languageDropdown.value = counter;
            }
            counter = counter + 1;
            Debug.Log("counter: " + counter);
        }

        //listener for toggle state changes
        effectsToggle.onValueChanged.AddListener(delegate
        {
            EffectsToggleValueChanged(effectsToggle.isOn);
        });
        musicToggle.onValueChanged.AddListener(delegate
        {
            MusicToggleValueChanged(musicToggle.isOn);
        });
        introToggle.onValueChanged.AddListener(delegate
        {
            settingsObject.SetIntroBool(introToggle.isOn);
        });

        //listener for slider state changes
        effectsSlider.onValueChanged.AddListener(delegate
        {
            //Round as Typecast since slider only allows whole number based on unity settings
            EffectsSliderValueChanged(Mathf.RoundToInt(effectsSlider.value));
        });

        musicSlider.onValueChanged.AddListener(delegate
        {
            //Round as Typecast since slider only allows whole number based on unity settings
            MusicSliderValueChanged(Mathf.RoundToInt(musicSlider.value));
        });

        //listener for dropdown change
        languageDropdown.onValueChanged.AddListener(delegate
        {
            settingsObject.SetLanguage(languageDropdown.options[languageDropdown.value].text);
        });
    }

    //handle toggle change
    void EffectsToggleValueChanged(bool value)
    {
        soundObject.SetEffectsEnabled(value);
    }

    void MusicToggleValueChanged(bool value)
    {
        soundObject.SetMusicEnabled(value);
    }

    //handle slider change
    void EffectsSliderValueChanged(int value)
    {
        soundObject.SetEffectsVolume(value);
    }

    void MusicSliderValueChanged(int value)
    {
        soundObject.SetMusicVolume(value);
    }
}
