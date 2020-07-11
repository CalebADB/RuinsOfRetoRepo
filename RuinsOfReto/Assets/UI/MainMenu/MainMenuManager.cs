using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MusicEngine.Instance.Play_MusicSituation(MusicEngine.Music_Situation.Start_MenuScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
