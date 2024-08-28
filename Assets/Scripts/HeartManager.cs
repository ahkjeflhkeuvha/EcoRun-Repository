using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeartManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public GameObject[] heartObjects;
    public Sprite emptyHeart;

    void Start()
    {
        heartObjects = GameObject.FindGameObjectsWithTag("Heart");
    }

    public void DestroyHeart(int index)
    {
        Debug.Log("destroy");
        SpriteRenderer sr = heartObjects[index].GetComponent<SpriteRenderer>();
        if (index >= 0 && index < heartObjects.Length)
        {
            sr.sprite = emptyHeart;
        }
        else
        {
            /*SceneManager.LoadScene("GameOver");*/
            Debug.Log("gameover");
        }
    }
}