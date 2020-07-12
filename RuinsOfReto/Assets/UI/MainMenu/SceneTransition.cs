using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public static int CurrentScene
    {
        get
        {
            return PlayerPrefs.GetInt("fhgfkk", 1);
        }
        set
        {
            PlayerPrefs.SetInt("fhgfkk", value);
        }
    }
    public static int UnlockedLevels
    {
        get
        {
            return PlayerPrefs.GetInt("lvfdfas", 0);
        }
        set
        {
            PlayerPrefs.SetInt("lvfdfas", value);
        }
    }

    public static void TransitionToNextScene(SceneName destinationScene)
    {

    }

    public enum SceneName
    {
        MainMenu,
        Level1,
        Level2,
        Level3,
        Level4
    }

}
