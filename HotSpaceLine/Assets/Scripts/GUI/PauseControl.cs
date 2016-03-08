using UnityEngine;
using System.Collections;



public class PauseControl : MonoBehaviour {
    public GameObject pauseMenu;
    bool isPause = false;
    public Rect PauseRect = new Rect(20, 20, 120, 50);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
            if (isPause)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
	}
    void OnGUI()
    {
        if (isPause)
            pauseMenu.SetActive(true);
        else
            pauseMenu.SetActive(false);

    }
    public void Continue()
    {
        isPause = false;
        Time.timeScale = 1;
    }
    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
        isPause = false;
        Time.timeScale = 1;
    }
    public void MainMenu()
    {
        Application.LoadLevel(0);
        isPause = false;
        Time.timeScale = 1;
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}

 