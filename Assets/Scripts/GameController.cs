using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Mailbox[] mailboxes;

    [SerializeField] private float showWinScreenTime = 3;

    private LevelLoader levelLoader;
    private UIManager uIManager;

    private void Start() {
        levelLoader = FindObjectOfType<LevelLoader>();
        uIManager = FindObjectOfType<UIManager>();
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
        }
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
            levelLoader.ReloadScene();
        }
    }
}
