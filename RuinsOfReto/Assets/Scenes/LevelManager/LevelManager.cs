using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class LevelManager : MonoBehaviour
    {
        public int levelNo;

        public float xActionTrigger;
        public float xWinTrigger;


        // Start is called before the first frame update
        void Start()
        {
            if (MusicEngine.Instance != null)
            {
                MusicEngine.Instance.Play_MusicSituation(MusicEngine.Music_Situation.Start_LevelScene);
            }
        }
    }
}
