using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] string sceneName;
    public void SwitchScene(string sceneName){
        SceneManager.LoadScene(sceneName); 
    }

    public void NextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }
}
