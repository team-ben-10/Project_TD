using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawnRatios
    {
        public GameObject enemyPrefab;
        public float spawnPercent;
    }

    public float SpawnTime = 4;
    public List<EnemySpawnRatios> spawnRatios = new List<EnemySpawnRatios>();

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    int round = 0;

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(SpawnTime);
        if (GameManager.instance.Running)
        {
            GameObject enemyObject = null;
            while (enemyObject == null)
            {
                int index = UnityEngine.Random.Range(0, spawnRatios.Count);
                int percent = UnityEngine.Random.Range(0, 101);
                if (spawnRatios[index].spawnPercent >= percent)
                {
                    enemyObject = spawnRatios[index].enemyPrefab;
                }
            }
            Instantiate(enemyObject, transform.position, Quaternion.identity);
            if(round%2==0)
                SpawnTime -= 0.01f;
            round++;
        }
        StartCoroutine(Spawn());
    }

    public void SpawnBlocker(int v)
    {
        Debug.Log(v + " Blocker was recieved!");
        Instantiate(spawnRatios[v-1].enemyPrefab, transform.position, Quaternion.identity);
    }
}
