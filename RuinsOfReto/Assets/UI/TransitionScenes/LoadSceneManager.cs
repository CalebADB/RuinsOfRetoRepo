using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace masterFeature
{
    public class LoadSceneManager : MonoBehaviour
    {
        public float loadingTime = 5;
        [SerializeField]
        private GameObject pnlLvl1;
        [SerializeField]
        private GameObject pnlLvl2;
        [SerializeField]
        private GameObject pnlLvl3;
        [SerializeField]
        private GameObject pnlLvl4;
        [SerializeField]
        private GameObject pnlMenu;

        // Start is called before the first frame update
        void Start()
        {
            InitializePanel();

            StartCoroutine(LoadLevelAfterDelay());
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator LoadLevelAfterDelay()
        {
            yield return new WaitForSeconds(loadingTime);

            SceneManager.LoadScene(SceneTransition.DestinationScene);
        }

        private void InitializePanel()
        {
            var sceneName = (SceneTransition.SceneName)SceneTransition.DestinationScene;
            switch (sceneName)
            {
                case SceneTransition.SceneName.MainMenu:
                    pnlMenu.SetActive(true);
                    break;
                case SceneTransition.SceneName.Level1:
                    pnlLvl1.SetActive(true);
                    break;
                case SceneTransition.SceneName.Level2:
                    pnlLvl2.SetActive(true);
                    break;
                case SceneTransition.SceneName.Level3:
                    pnlLvl3.SetActive(true);
                    break;
                case SceneTransition.SceneName.Level4:
                    pnlLvl4.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }
}
