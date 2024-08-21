using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{
    public float scrollSpeed = 2f;    // 배경 이동 속도
    public float backgroundWidth;     // 배경 이미지의 폭

    private Transform[] backgrounds;  // 배경 이미지의 트랜스폼
    private Vector3 startPosition;    // 시작 위치

    void Start()
    {
        // 배경 이미지의 트랜스폼을 초기화
        backgrounds = new Transform[2];
        for (int i = 0; i < 2; i++)
        {
            backgrounds[i] = transform.GetChild(i);
        }

        // 배경 이미지의 시작 위치를 초기화
        startPosition = backgrounds[0].position;
        backgroundWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // 배경 이미지를 왼쪽으로 이동
        for (int i = 0; i < 2; i++)
        {
            backgrounds[i].Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        }

        // 첫 번째 배경 이미지가 왼쪽 끝에 도달했는지 확인
        if (backgrounds[0].position.x <= -backgroundWidth)
        {
            // 첫 번째 배경을 오른쪽으로 이동하여 다시 시작
            backgrounds[0].position = new Vector3(backgrounds[1].position.x + backgroundWidth, startPosition.y, startPosition.z);

            // 두 개의 배경을 스왑
            Transform temp = backgrounds[0];
            backgrounds[0] = backgrounds[1];
            backgrounds[1] = temp;
        }
    }
}
