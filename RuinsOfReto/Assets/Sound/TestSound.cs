using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace masterFeature
{
    public class TestSound : MonoBehaviour
    {

        public void Start_MenuScene()
        {
            MusicEngine.Instance.Play_MusicSituation(MusicEngine.Music_Situation.Start_MenuScene);
        }

        public void End_MenuScene()
        {
            MusicEngine.Instance.Play_MusicSituation(MusicEngine.Music_Situation.End_MenuScene);
        }

        public void Start_LevelScene()
        {
            MusicEngine.Instance.Play_MusicSituation(MusicEngine.Music_Situation.Start_LevelScene);
        }

        public void ActionZoneTrigger()
        {
            MusicEngine.Instance.Play_MusicSituation(MusicEngine.Music_Situation.ActionZoneTrigger);
        }

        public void CalmZoneTrigger()
        {
            MusicEngine.Instance.Play_MusicSituation(MusicEngine.Music_Situation.CombatZoneTrigger);
        }

        public void LevelFinished()
        {
            MusicEngine.Instance.Play_MusicSituation(MusicEngine.Music_Situation.LevelFinished);
        }

        public void Reset()
        {
            SceneManager.LoadScene("TestSound");
        }

    }
}
