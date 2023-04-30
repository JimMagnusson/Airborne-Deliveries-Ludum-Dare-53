using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Mailbox[] mailboxes;

    [SerializeField] private float showWinScreenTime = 3;

    private LevelLoader levelLoader;
    private UIManager uIManager;

    private TimeTracker timeTracker;

    private void Start() {
        levelLoader = FindObjectOfType<LevelLoader>();
        uIManager = FindObjectOfType<UIManager>();
        timeTracker = GetComponent<TimeTracker>();

        if(levelLoader.GetCurrentSceneIndex() == 0) {
            PlayerPrefs.SetFloat("TotalTime", 0.0f);
        }
        timeTracker.totalTimeTimer = PlayerPrefs.GetFloat("TotalTime", 0.0f);
        if(levelLoader.GetCurrentSceneIndex() == levelLoader.GetSceneCount()-1) {
            uIManager.SetFinalTime(timeTracker.totalTimeTimer);
            // check if new time is best time
            float bestTime = PlayerPrefs.GetFloat("BestTime", 10000f);
            if(timeTracker.totalTimeTimer < bestTime) {
                PlayerPrefs.SetFloat("BestTime", timeTracker.totalTimeTimer);
                uIManager.SetBestTime(timeTracker.totalTimeTimer, true);
            }
            else {
                uIManager.SetBestTime(bestTime, false);
            }
        }
    }

    public void WinCheck(){
        foreach(Mailbox mailbox in mailboxes) {
            if(mailbox.open) {
                return;
            }
        }
        Win();
        
    }

    private void Win() {
        int currIndex = levelLoader.GetCurrentSceneIndex();
        if(currIndex != 0 && currIndex != levelLoader.GetSceneCount()-1) { // Dont show for main menu
            uIManager.ShowWinLevelScreen();
            // Save time to playerprefs
            PlayerPrefs.SetFloat("TotalTime", timeTracker.totalTimeTimer);
        }
        timeTracker.StopTimer();
        StartCoroutine(WaitAndChangeScene());
    }

    private IEnumerator WaitAndChangeScene() {
        yield return new WaitForSeconds(showWinScreenTime);
        if(levelLoader.GetCurrentSceneIndex() == levelLoader.GetSceneCount()-1) {
            levelLoader.LoadSceneWithBuildIndex(0);
        }
        else {
            levelLoader.LoadNextScene();
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {
            // Save round time to player prefs before restarting
            PlayerPrefs.SetFloat("TotalTime", timeTracker.totalTimeTimer);
            levelLoader.ReloadScene();
        }
    }
}
