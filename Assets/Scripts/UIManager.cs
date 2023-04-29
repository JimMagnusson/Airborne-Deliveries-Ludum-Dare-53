using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Image WinLevelImage;
    [SerializeField] private Image WonGameImage;
    [SerializeField] private Image RetryImage;
    [SerializeField] private Image PauseImage;
    [SerializeField] private Image StartGameImage;


    public void ShowWinLevelScreen()
    {
        WinLevelImage.gameObject.SetActive(true);
    }

    public void ShowWonGameImage()
    {
        WonGameImage.gameObject.SetActive(true);
    }

    public void ShowRetryImage()
    {
        RetryImage.gameObject.SetActive(true);
    }

    public void ToggleStartGameImage(bool boolean)
    {
        StartGameImage.gameObject.SetActive(boolean);
    }

    public void TogglePauseImage(bool boolean)
    {
        PauseImage.gameObject.SetActive(boolean);
    }

    public bool WinLevelScreenIsActive()
    {
        return WinLevelImage.gameObject.activeSelf;
    }

    public bool RetryImageScreenIsActive()
    {
        return RetryImage.gameObject.activeSelf;
    }

    public bool PauseImageScreenIsActive()
    {
        return PauseImage.gameObject.activeSelf;
    }

    public bool StartGameImageIsActive()
    {
        return StartGameImage.gameObject.activeSelf;
    }

}