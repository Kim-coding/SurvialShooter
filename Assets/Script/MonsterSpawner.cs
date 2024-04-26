using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Monster[] monsterPrefab; 
    public GameObject[] spawnPoints; 
    private List<GameObject>[] monsterPools;

    void Start()
    {
        monsterPools = new List<GameObject>[monsterPrefab.Length];
        for (int i = 0; i < monsterPrefab.Length; i++)
        {
            monsterPools[i] = new List<GameObject>();
            for (int j = 0; j < 5; j++)
            {
                GameObject obj = Instantiate(monsterPrefab[i].gameObject);
                obj.SetActive(false);
                monsterPools[i].Add(obj);
            }
        }
        StartCoroutine(SpawnRoutine(0, 3));
        StartCoroutine(SpawnRoutine(1, 6));
        StartCoroutine(SpawnRoutine(2, 3));
    }

    private IEnumerator SpawnRoutine(int index, int interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Spawn(index);
        }
    }

    private void Spawn(int spawnIndex)
    {
        GameObject monster = GetMonster(spawnIndex);
        if (monster != null)
        {
            monster.transform.position = spawnPoints[spawnIndex].transform.position;
            monster.SetActive(true);
        }
    }

    private GameObject GetMonster(int index)
    {
        foreach (GameObject obj in monsterPools[index])
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null; 
    }
}
