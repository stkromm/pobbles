﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {
    public Toggle effectsToggle;
    public Toggle musicToggle;
    public Slider effectsSlider;
    public Slider musicSlider;
    private Sound soundObject;

	// Use this for initialization
	void Start () {
        //load saved sound settings
        this.soundObject = Object.FindObjectOfType<Sound>();
        effectsToggle.isOn = soundObject.GetEffectsBool();
        musicToggle.isOn = soundObject.GetMusicBool();
        effectsSlider.value = soundObject.GetEffectsValue();
        musicSlider.value = soundObject.GetMusicValue();

        //listener for toggle state changes
        effectsToggle.onValueChanged.AddListener(delegate
        {
            EffectsToggleValueChanged(effectsToggle.isOn);
        });
        musicToggle.onValueChanged.AddListener(delegate
        {
            MusicToggleValueChanged(musicToggle.isOn);
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
    }

    //handle toggle change
    void EffectsToggleValueChanged(bool value)
    {
        soundObject.SetEffectsBool(value);
    }

    void MusicToggleValueChanged(bool value)
    {
        soundObject.SetMusicBool(value);
    }

    //handle slider change
    void EffectsSliderValueChanged(int value)
    {
        soundObject.SetEffectsValue(value);
    }

    void MusicSliderValueChanged(int value)
    {
        soundObject.SetMusicValue(value);
    }
}