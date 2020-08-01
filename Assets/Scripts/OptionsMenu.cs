using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    //Access to the AudioMixer component
    public AudioMixer audioMixer;

    //Variable to check if the number of players will be one or two
    public bool isTwoPlayerGame;

    //Checks the number of players
    public void IsTwoPlayer()
    {
        GameManager.Instance.twoPlayerGame = !GameManager.Instance.twoPlayerGame;
    }

    //Sets the Level of the Day based on player input
    public void LevelOfTheDay(Toggle levelOfTheDay)
    {
        if (levelOfTheDay.isOn)
        {
            GameManager.Instance.mapGenerator.SetMapType(MapGenerator.MapType.MapOfTheDay);
            GameManager.Instance.mapGenerator.SetSeed(GameManager.Instance.mapGenerator.DateToInt(DateTime.Now.Date)); 
        }
        else
        {
            GameManager.Instance.mapGenerator.SetMapType(MapGenerator.MapType.Random);
            GameManager.Instance.mapGenerator.SetSeed(GameManager.Instance.mapGenerator.DateToInt(DateTime.Now));
        }
    }

    //Sets the map seed based on player input
    public void SeedInput(InputField seedInput)
    {
        if (seedInput == null)
        {
            GameManager.Instance.mapGenerator.SetMapType(MapGenerator.MapType.Random);
            GameManager.Instance.mapGenerator.SetSeed(GameManager.Instance.mapGenerator.DateToInt(DateTime.Now));
        }
        else
        {
            GameManager.Instance.mapGenerator.SetMapType(MapGenerator.MapType.Seeded);
            GameManager.Instance.mapGenerator.SetSeed(int.Parse(seedInput.text));
        }
    }

    //Adjusts the music volume
    public void MusicSlider(Slider sliderValue)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue.value) * 20);
    }

    //Adjusts the SFX volume
    public void SFXSlider(Slider sliderValue)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue.value) * 20);
    }
}
