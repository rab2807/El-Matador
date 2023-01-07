using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static void GoTo(string name)
    {
        if (name.Equals("main"))
            SceneManager.LoadScene("MainMenu");
        else if (name.Equals("gameplay"))
            SceneManager.LoadScene("GamePlay");
        else if (name.Equals("help"))
            SceneManager.LoadScene("HelpMenu");
        else if (name.Equals("gameover"))
            Instantiate(Resources.Load("GameOverPrefab"));
        else if (name.Equals("pause"))
            Instantiate(Resources.Load("PausePrefab"));
        else if(name.Equals("quit"))
            Application.Quit();
    }
}
