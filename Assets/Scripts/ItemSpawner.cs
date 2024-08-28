using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewSpawner : MonoBehaviour
{
    public enum ObjectType { Rock, Grass, Net, Attack, Gun, E, C, O, R, U, N } // ������Ʈ Ÿ���� �����ϱ� ���� enum

    [System.Serializable]
    public class ObjectInfo
    {
        public ObjectType type;   // ������Ʈ Ÿ�� (Rock, Grass, Net, Attack)
        public Vector3 position;  // ������Ʈ ��ġ (x, y, z ��ǥ)
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

    // ���� ������ 2�ʷ� ����
    public float timeBetSpawn = 2f;

    private float lastSpawnTime;
    private int currentIndex = 0; // ����Ʈ���� ���� ������ ������Ʈ�� �ε���

    // ������Ʈ ������ �����ϴ� ����Ʈ
    public List<ObjectInfo> objectsToSpawn;

    void Start()
    {
        this.lastSpawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            // ��ϵ� ������ ��ġ ������ ���� �������� ����
            this.lastSpawnTime = Time.time;

            // ����Ʈ���� ���� �ε����� ������Ʈ ������ ������� ���ο� ������Ʈ ����
            if (objectsToSpawn != null && objectsToSpawn.Count > 0)
            {
                SpawnObject(objectsToSpawn[currentIndex]);

                // ���� �ε����� �̵�, ����Ʈ�� ���� �����ϸ� �ٽ� ó������
                currentIndex = (currentIndex + 1) % objectsToSpawn.Count;
            }
        }
    }

    public void SpawnObject(ObjectInfo info)
    {
        GameObject prefabToSpawn = null;

        // ������Ʈ Ÿ�Կ� ���� ������ ����
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

        // �������� ���õǾ����� �ش� ��ġ�� ������Ʈ ����
        if (prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn, info.position, Quaternion.identity);
        }
    }
}