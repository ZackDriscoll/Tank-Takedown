using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance
    {
        get { return instance; }
    }

    public static bool IsInitialized
    {
        get { return instance != null; }
    }

    protected virtual void Awake()
    {
        if (IsInitialized)
        {
            Debug.LogError("Attempting to instantiate a second instance of a Game Manager.");
            Destroy(this.gameObject);
        }
        else
        {
            instance = (T) this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
