using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public Button myButton;       // 버튼 컴포넌트
    public Sprite stopSprite;
    public Sprite playSprite;
    public PlayerController playerController;

    private bool isPaused = false;

    void Start()
    {
        // myButton과 buttonImage가 null인지 확인
        if (myButton == null)
        {
            Debug.LogError("myButton is not assigned!");
            return;
        }

        // playerController 초기화 확인
        if (playerController == null)
        {
            Debug.LogError("PlayerController is not assigned!");
            return;
        }

        // 버튼 클릭 이벤트에 메서드 등록
        myButton.onClick.AddListener(TogglePause);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Debug.Log("pause");
            PauseGame();
        }
        else
        {
            Debug.Log("restart");
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        if (playerController == null)
        {
            Debug.LogError("PlayerController not found! Cannot pause the game.");
            return;
        }

        myButton.GetComponent<Image>().sprite = playSprite;
        playerController.GamePause();
        Time.timeScale = 0f; // 게임 일시정지
    }

    public void ResumeGame()
    {
        if (playerController == null)
        {
            Debug.LogError("PlayerController not found! Cannot resume the game.");
            return;
        }

        myButton.GetComponent<Image>().sprite = stopSprite;
        playerController.GameStart();
        Time.timeScale = 1f; // 게임 재개
    }
}
