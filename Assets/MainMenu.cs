using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    public void ToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(2);
    }

}
