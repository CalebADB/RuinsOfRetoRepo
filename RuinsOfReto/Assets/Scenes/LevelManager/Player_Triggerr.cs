using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace masterFeature
{
    public class Player_Trigger : MonoBehaviour
    {
        private bool isActionMusicTriggered = false;
        private bool isCombatMusicTriggered = false;
        private bool isWinTriggered = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "TriggerActionMusic" &&
                isActionMusicTriggered == false)
            {
                Debug.Log("Music Action Triggered");
                isActionMusicTriggered = true;
                if (MusicEngine.Instance != null)
                {
                    MusicEngine.Instance.Play_MusicSituation(MusicEngine.Music_Situation.ActionZoneTrigger);
                }
            }

            if (collision.gameObject.tag == "TriggerCombatMusic" &&
               isCombatMusicTriggered == false)
            {
                Debug.Log("Music Combat Triggered");
                isCombatMusicTriggered = true;
                if (MusicEngine.Instance != null)
                {
                    MusicEngine.Instance.Play_MusicSituation(MusicEngine.Music_Situation.CombatZoneTrigger);
                }
            }

            if (collision.gameObject.tag == "TriggerWin" &&
               isWinTriggered == false)
            {
                Debug.Log("Win Triggered");
                isWinTriggered = true;

                SceneTransition.UnlockedLevels++;
                if (SceneTransition.UnlockedLevels >= 3)
                {
                    SceneTransition.UnlockedLevels = 3;
                }

                int currentScene = SceneManager.GetActiveScene().buildIndex;
                int nextScene = 0;
                if (currentScene >= 3)
                {
                    nextScene = 0;
                }
                else
                {
                    nextScene = currentScene + 1;
                }
                if (MusicEngine.Instance != null)
                {
                    MusicEngine.Instance.Play_MusicSituation(MusicEngine.Music_Situation.LevelFinished);
                }
                SceneTransition.TransitionToNextScene((SceneTransition.SceneName)(nextScene));
            }


        }
    }
}