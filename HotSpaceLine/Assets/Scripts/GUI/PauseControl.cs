using UnityEngine;
using System.Collections;

public class PauseControl : MonoBehaviour {
    public GameObject pauseMenu;
    private WeaponController stopShoot;
    private bool isPause = false;

    private void Start () {
        stopShoot = GameObject.FindObjectOfType<WeaponController>();
	}

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            isPause = !isPause;
            if (isPause) {
                stopShoot.enabled = false;
                Time.timeScale = 0;
            } else {
                stopShoot.enabled = true;
                Time.timeScale = 1;
            }
        }
    }

    void OnGUI() {
        if (isPause) {
            pauseMenu.SetActive(true);
        } else {
            pauseMenu.SetActive(false);
        }
    }

    public void Continue() {
        isPause = false;
        Time.timeScale = 1;
    }

    public void Restart() {
        Application.LoadLevel(Application.loadedLevel);
        isPause = false;
        Time.timeScale = 1;
    }

    public void MainMenu() {
        Application.LoadLevel(0);
        isPause = false;
        Time.timeScale = 1;
    }

    public void ExitGame() {
        Application.Quit();
    }

}

 