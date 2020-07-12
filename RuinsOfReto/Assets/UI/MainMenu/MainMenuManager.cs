using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform pnlMain;
    [SerializeField]
    private RectTransform pnlLvlSelect;
    [SerializeField]
    private RectTransform pnlCredits;
    [SerializeField]
    private GameObject btnLvl2;
    [SerializeField]
    private GameObject btnLvl3;
    [SerializeField]
    private GameObject btnLvl4;




    // Start is called before the first frame update
    void Start()
    {
        MusicEngine.Instance.Play_MusicSituation(MusicEngine.Music_Situation.Start_MenuScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick_BtnNewGame()
    {
        MusicEngine.Instance.PlaySFX(MusicEngine.SFXType.buttonClick);
        SceneTransition.UnlockedLevels = 1;
        MusicEngine.Instance.Play_MusicSituation(MusicEngine.Music_Situation.End_MenuScene);
        SceneTransition.TransitionToNextScene(SceneTransition.SceneName.Level1);
    }
    public void OnClick_BtnLevelSelect()
    {
        MusicEngine.Instance.PlaySFX(MusicEngine.SFXType.buttonClick);

        pnlMain.gameObject.SetActive(false);
        pnlLvlSelect.gameObject.SetActive(true);
    }
    public void OnClick_BtnCredits()
    {
        MusicEngine.Instance.PlaySFX(MusicEngine.SFXType.buttonClick);

        pnlMain.gameObject.SetActive(false);
        pnlCredits.gameObject.SetActive(true);
    }
    public void OnClick_BtnQuit()
    {
        MusicEngine.Instance.PlaySFX(MusicEngine.SFXType.buttonClick);

        Application.Quit();
    }
    public void OnClick_BtnLevel1()
    {
        MusicEngine.Instance.PlaySFX(MusicEngine.SFXType.buttonClick);

        SceneTransition.TransitionToNextScene(SceneTransition.SceneName.Level1);
    }
    public void OnClick_BtnLevel2()
    {
        MusicEngine.Instance.PlaySFX(MusicEngine.SFXType.buttonClick);

        SceneTransition.TransitionToNextScene(SceneTransition.SceneName.Level2);
    }
    public void OnClick_BtnLevel3()
    {
        MusicEngine.Instance.PlaySFX(MusicEngine.SFXType.buttonClick);

        SceneTransition.TransitionToNextScene(SceneTransition.SceneName.Level3);
    }
    public void OnClick_BtnLevel4()
    {
        MusicEngine.Instance.PlaySFX(MusicEngine.SFXType.buttonClick);

        SceneTransition.TransitionToNextScene(SceneTransition.SceneName.Level4);
    }
    public void OnClick_BtnBackLevelSelect()
    {
        MusicEngine.Instance.PlaySFX(MusicEngine.SFXType.buttonClick);
      
        pnlLvlSelect.gameObject.SetActive(false);
        pnlMain.gameObject.SetActive(true);
    }
    public void OnClick_BtnBackCredit()
    {
        MusicEngine.Instance.PlaySFX(MusicEngine.SFXType.buttonClick);
        
        pnlCredits.gameObject.SetActive(false);
        pnlMain.gameObject.SetActive(true);
    }








}
