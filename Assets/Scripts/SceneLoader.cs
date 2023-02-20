using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private string sceneName;

        public void Load()
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}