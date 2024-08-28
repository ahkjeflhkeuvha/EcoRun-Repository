using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewSpawner : MonoBehaviour
{
    public enum ObjectType { Rock, Grass, Net, Attack, Gun, E, C, O, R, U, N } // 오브젝트 타입을 구분하기 위한 enum

    [System.Serializable]
    public class ObjectInfo
    {
        public ObjectType type;   // 오브젝트 타입 (Rock, Grass, Net, Attack)
        public Vector3 position;  // 오브젝트 위치 (x, y, z 좌표)
    }

    public GameObject rockPrefab;
    public GameObject grassPrefab;
    public GameObject netPrefab;
    public GameObject attackPrefab;
    public GameObject gunPrefab;

    public GameObject EPrefab;
    public GameObject CPrefab;
    public GameObject OPrefab;
    public GameObject RPrefab;
    public GameObject UPrefab;
    public GameObject NPrefab;

    // 생성 간격을 2초로 고정
    public float timeBetSpawn = 2f;

    private float lastSpawnTime;
    private int currentIndex = 0; // 리스트에서 현재 생성할 오브젝트의 인덱스

    // 오브젝트 정보를 저장하는 리스트
    public List<ObjectInfo> objectsToSpawn;

    void Start()
    {
        this.lastSpawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            // 기록된 마지막 배치 시점을 현재 시점으로 갱신
            this.lastSpawnTime = Time.time;

            // 리스트에서 현재 인덱스의 오브젝트 정보를 기반으로 새로운 오브젝트 생성
            if (objectsToSpawn != null && objectsToSpawn.Count > 0)
            {
                SpawnObject(objectsToSpawn[currentIndex]);

                // 다음 인덱스로 이동, 리스트의 끝에 도달하면 다시 처음으로
                currentIndex = (currentIndex + 1) % objectsToSpawn.Count;
            }
        }
    }

    public void SpawnObject(ObjectInfo info)
    {
        GameObject prefabToSpawn = null;

        // 오브젝트 타입에 따라 프리팹 선택
        switch (info.type)
        {
            case ObjectType.Rock:
                prefabToSpawn = rockPrefab;
                break;
            case ObjectType.Grass:
                prefabToSpawn = grassPrefab;
                break;
            case ObjectType.Net:
                prefabToSpawn = netPrefab;
                break;
            case ObjectType.Attack:
                prefabToSpawn = attackPrefab;
                break;
            case ObjectType.Gun:
                prefabToSpawn = gunPrefab;
                break;
            case ObjectType.E:
                prefabToSpawn = EPrefab;
                break;
            case ObjectType.C:
                prefabToSpawn = CPrefab;
                break;
            case ObjectType.O:
                prefabToSpawn = OPrefab;
                break;
            case ObjectType.R:
                prefabToSpawn = RPrefab;
                break;
            case ObjectType.U:
                prefabToSpawn = UPrefab;
                break;
            case ObjectType.N:
                prefabToSpawn = NPrefab;
                break;
        }

        // 프리팹이 선택되었으면 해당 위치에 오브젝트 생성
        if (prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn, info.position, Quaternion.identity);
        }
    }
}