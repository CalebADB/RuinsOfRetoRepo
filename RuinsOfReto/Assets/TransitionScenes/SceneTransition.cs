using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace masterFeature
{
    public class SceneTransition : MonoBehaviour
    {
        public static int DestinationScene
        {
            get
            {
                return PlayerPrefs.GetInt("fhgfkk", 0);
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
                return PlayerPrefs.GetInt("lvfdfas", 1);
            }
            set
            {
                PlayerPrefs.SetInt("lvfdfas", value);
            }
        }

        public static void TransitionToNextScene(SceneName destinationScene)
        {
            switch (destinationScene)
            {
                case SceneName.MainMenu:
                    DestinationScene = 1;
                    break;
                case SceneName.Level1:
                    DestinationScene = 2;
                    break;
                case SceneName.Level2:
                    DestinationScene = 3;
                    break;
                case SceneName.Level3:
                    DestinationScene = 4;
                    break;
                case SceneName.Level4:
                    DestinationScene = 5;
                    break;
                default:
                    break;
            }

            //LoadingScene
            SceneManager.LoadScene((int)SceneName.Loading);

        }

        public enum SceneName
        {
            MainMenu = 0,
            Loading = 1,
            Level1 = 2,
            Level2 = 3,
            Level3 = 4,
            Level4 = 5
        }

    }
}
