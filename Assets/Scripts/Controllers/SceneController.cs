using System;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace BridgeLearningTest {

    public class SceneController : MonoBehaviour
    {
        public static event Action OnSceneLoaded;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += SceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= SceneLoaded;
        }


        private void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            int sceneId = scene.buildIndex;
            GameController.Instance.SceneLoaded(sceneId);
            OnSceneLoaded?.Invoke();
        }

        public void LoadScene(int index)
        {
            SceneManager.LoadScene(index);

        }
    }
}

