using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void PlayButton()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
    }
    public void QuitButton()
    {
        Application.Quit();
    }

}
