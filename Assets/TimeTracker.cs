using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTracker : MonoBehaviour
{
    private UIManager uIManager;
    public float totalTimeTimer = 0;

    private LevelLoader levelLoader;

    private bool counting = true;

    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        levelLoader = FindObjectOfType<LevelLoader>();

        // Need to load from playerprefs?
    }

    public void StopTimer() {
        counting = false;
    }
    void Update()
    {
        // Dont count time if in welcome menu or end menu.
        int index = levelLoader.GetCurrentSceneIndex();
        if(counting && !((index == levelLoader.GetSceneCount() - 1)|| index == 0)) {
            totalTimeTimer += Time.deltaTime;
        }
        uIManager.SetTotalTime(totalTimeTimer);
    }
}
