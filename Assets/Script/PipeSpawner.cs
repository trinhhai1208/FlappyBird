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
        while (true)
        {
            SpawnPipe();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnPipe()
    {
        if (Time.timeScale == 0f) return;

        // Kiểm tra xem pool đã được tạo chưa để tránh lỗi NullReferenceException
        if (SimpleObjectPool.instance == null)
        {
            Debug.LogError("Error: Không tìm thấy SimpleObjectPool trong Scene. Đảm bảo bạn đã gán nó vào một Object.");
            return;
        }

        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(10f, randomY, 0f);
        
        // Lấy từ pool
        GameObject pipe = SimpleObjectPool.instance.GetFromPool();
        
        // Cực kỳ quan trọng: Thiết lập lại vị trí và kích hoạt ống
        pipe.transform.position = spawnPos;
        pipe.SetActive(true);

        // Reset tốc độ cho ống
        PipeMove pipeMove = pipe.GetComponent<PipeMove>();
        if (pipeMove != null)
        {
            pipeMove.speed = GameManager.instance.GetCurrentSpeed();
        }
    }
}
