using UnityEngine;
using UnityEngine.SceneManagement;

// Ensure the script executes in the Unity namespace
namespace masterFeature
{
    public class LevelPortal : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("WAHAHAHGA");
            // Check if the colliding object is the player
            if (other.gameObject.CompareTag("Player") && other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Debug.Log("Player has entered the portal");

                // Optionally, perform any checks or setup before transitioning
                TriggerLevelTransition();
            }
        }

        private void TriggerLevelTransition()
        {
            // Assuming SceneTransition is a static class that manages scene transitions
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            // Check if the next scene index exceeds your available scenes count
            // This example simply wraps around to the first scene
            if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }

            // Play level finished music if the MusicEngine is available
            if (MusicEngine.Instance != null)
            {
                MusicEngine.Instance.Play_MusicSituation(MusicEngine.Music_Situation.LevelFinished);
            }

            // Transition to the next scene
            SceneTransition.TransitionToNextScene((SceneTransition.SceneName)nextSceneIndex);
        }
    }
}
