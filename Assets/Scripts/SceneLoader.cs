using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        public void Load()
        {
            SceneManager.LoadSceneAsync(_sceneName);
        }
    }
}