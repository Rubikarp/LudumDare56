using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField, Scene]
    private int sceneToLoad = 1;

    [SerializeField] 
    private GameObject creditPanel;

    public void Jouer()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public void ToogleCredit()
    {
        if (creditPanel == null)
        {
            Debug.LogError("Credit panel not set");
            return;
        }
        creditPanel.SetActive(!creditPanel.activeSelf);
    }

    public void Quitter()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
}
