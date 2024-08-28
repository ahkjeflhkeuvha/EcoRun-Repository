using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public Button myButton;       // ��ư ������Ʈ
    public Sprite stopSprite;
    public Sprite playSprite;
    public PlayerController playerController;

    private bool isPaused = false;

    void Start()
    {
        // myButton�� buttonImage�� null���� Ȯ��
        if (myButton == null)
        {
            Debug.LogError("myButton is not assigned!");
            return;
        }

        // playerController �ʱ�ȭ Ȯ��
        if (playerController == null)
        {
            Debug.LogError("PlayerController is not assigned!");
            return;
        }

        // ��ư Ŭ�� �̺�Ʈ�� �޼��� ���
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
        Time.timeScale = 0f; // ���� �Ͻ�����
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
        Time.timeScale = 1f; // ���� �簳
    }
}
