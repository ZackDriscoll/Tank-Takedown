using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : Singleton<SaveManager>
{
    public float musicVolume = 1.0f;
    public float fxVolume = 1.0f;

    public string playerName;
    public int score;

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

    public void Save(ScoreData scoreData)
    {
        playerName = scoreData.name;
        score = scoreData.score;
        PlayerPrefs.SetInt("High Score", score);
        PlayerPrefs.SetString("High Score Name", playerName);
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

        if (PlayerPrefs.HasKey("High Score"))
        {
            score = PlayerPrefs.GetInt("High Score");
        }
    }

    public int Load(ScoreData scoreData)
    {
        if (PlayerPrefs.HasKey("High Score"))
        {
            score = PlayerPrefs.GetInt("High Score");

            return score;
        }

        return 0;
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
