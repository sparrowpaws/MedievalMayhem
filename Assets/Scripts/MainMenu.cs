using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    //function to change the scene
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene("GameMainScene");
    }

    //function to exit the game
    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application has quit"); //wont show anything in unity editor without this line of code
    }
}
