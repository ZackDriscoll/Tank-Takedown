using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : Singleton<SaveManager>
{
    public float musicVolume = 1.0f;
    public float fxVolume = 1.0f;

    public void Start()
    {
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("fxVolume", fxVolume);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        }

        if (PlayerPrefs.HasKey("fxVolume"))
        {
            fxVolume = PlayerPrefs.GetFloat("fxVolume");
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
