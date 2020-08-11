using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ScoreData : IComparable<ScoreData>
{
    public int score;
    public string name;

    public int CompareTo(ScoreData other)
    {
        if (other == null)
        {
            return 1;
        }

        if (this.score > other.score)
        {
            return 1;
        }

        if (this.score < other.score)
        {
            return -1;
        }

        return 0;
    }

    public void SaveScore()
    {
        SaveManager.Instance.Save(this);
    }

    public void Load(ScoreData scoreData)
    {
        score = SaveManager.Instance.Load(scoreData);
    }
}
