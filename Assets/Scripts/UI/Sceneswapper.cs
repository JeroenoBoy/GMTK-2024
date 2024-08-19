using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneswapper : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }
    public void SwapScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
