using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject panel;
public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void Сlose()
    {
        Cursor.lockState = CursorLockMode.Locked;
        panel.SetActive(false);
    }
    public void play()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
