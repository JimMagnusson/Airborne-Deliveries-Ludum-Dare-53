using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Image WinLevelImage;
    [SerializeField] private TextMeshProUGUI totalTimeTimerTMP;

    [SerializeField] private TextMeshProUGUI endTimeUITMP;

    [SerializeField] private TextMeshProUGUI bestTimeUITMP;

    public void ShowWinLevelScreen()
    {
        WinLevelImage.gameObject.SetActive(true);
    }

    public void SetFinalTime(float time) {
        if(endTimeUITMP) {
            endTimeUITMP.SetText("Your time: {0:0}:{1:0}:{2:0}", (int) time/60, time % 60, (time*100)%100);
        }
    }

    public void SetBestTime(float time, bool record) {
        if(bestTimeUITMP) {
            if(record) {
                bestTimeUITMP.SetText("NEW BEST TIME: {0:0}:{1:0}:{2:0}", (int) time/60, time % 60, (time*100)%100);
            }
            else{
                bestTimeUITMP.SetText("Your best time: {0:0}:{1:0}:{2:0}", (int) time/60, time % 60, (time*100)%100);
            }
            
        }
    }

    public void SetTotalTime(float time) {
        if(totalTimeTimerTMP != null) {
            
            totalTimeTimerTMP.SetText("{0:0}:{1:0}:{2:0}", (int) time/60, time % 60, (time*100)%100);
        }
    }
}