using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public int maxCount;
    public int enemyCount;
    public float spawnTime;
    public float curTime;
    public bool[] isSpawn;
    public Transform[] spawnPoints;
    public GameObject enemy;
    public GameObject player;

    public static EnemySpawn _instance;

    void Awake()
    {
        // �ʱ� ����
        _instance = this;
        enemyCount = 0;
        isSpawn = new bool[spawnPoints.Length];

        // ���� ���� �ʱ�ȭ
        for (int i = 0; i < isSpawn.Length; i++)
            isSpawn[i] = false;
    }

    void Update()
    {
        // ���� �ð��� �Ǹ� ���� �Լ� ȣ��
        if (curTime >= spawnTime && enemyCount < maxCount)
        {
            int x = Random.Range(0, spawnPoints.Length);
            if (!isSpawn[x])
                SpawnEnemy(x);
        }
        curTime += Time.deltaTime;
    }

    void SpawnEnemy(int x)
    {
        curTime = 0;
        enemyCount++;

        //player : enemy�� �߰��ϸ� �Ѿƿ� �÷��̾� ��ü
        EnemyMove em = enemy.GetComponent<EnemyMove>();
        em.player = player;
        em.index = x;

        // ������ ��ġ�� ���� ����                
        Instantiate(enemy, spawnPoints[x]);
        isSpawn[x] = true;
    }
}