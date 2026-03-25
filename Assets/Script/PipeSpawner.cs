using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnInterval = 1.5f;
    public float minY = -2f;
    public float maxY = 2f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        // Chờ 1.5 giây trước pipe đầu tiên
        //yield return new WaitForSeconds(1.5f);

        while (true)
        {
            SpawnPipe();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    void SpawnPipe()
    {
        if (Time.timeScale == 0f) return;

        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(10f, randomY, 0f);
        Instantiate(pipePrefab, spawnPos, Quaternion.identity);
    }
}
