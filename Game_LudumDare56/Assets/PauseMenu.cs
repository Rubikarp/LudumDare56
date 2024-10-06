using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    bool isPaused = false;

    // Start is called before the first frame update
    [SerializeField] GameObject pausePanel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }

            else 
            {
                Resume();
            }
        }
    }

    void Pause()
    {
        pausePanel.SetActive(true);
        isPaused = true;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        isPaused = false;
    }

    public void openPanel(GameObject panelToOpen)
    {
        pausePanel.SetActive(false);
        panelToOpen.SetActive(true);
    }

    public void Quitter()
    {
        Application.Quit();
    }

    public void Retour(GameObject currentPanel)
    {
        currentPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
}
