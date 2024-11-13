using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUIManager : MonoBehaviour
{
    // you will need to drag in a reference to a parent object to turn off/on when pausing/unpausing
    [SerializeField] private GameObject pauseUI;
    private bool paused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        paused = false;
        // add more here
    }

    public void Pause()
    {
        paused = true;
        // add more here
    }

    public void Quit()
    {
        Application.Quit();
    }
}
