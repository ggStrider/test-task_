using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class ChangeScene : MonoBehaviour
    {
        public void _ImmediateSwitchSceneBySceneName(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void _ImmediateSwitchSceneByBuildIndex(int buildIndex)
        {
            SceneManager.LoadScene(buildIndex);
        }

        public void _ImmediateSwitchToNextBuildIndex()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
