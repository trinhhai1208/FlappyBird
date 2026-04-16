using System.Collections.Generic;
using UnityEngine;

public class PipePool : MonoBehaviour
{
    private static PipePool _instance;
    public static PipePool instance
    {
        get
        {
            if (_instance == null)
            {
                // Tự động tìm Pool trong Scene nếu chưa gán
                _instance = FindObjectOfType<PipePool>();
                
                // Nếu vẫn không thấy, tự tạo một Object mới (Lazy Initialization)
                if (_instance == null)
                {
                    GameObject go = new GameObject("PipePool");
                    _instance = go.AddComponent<PipePool>();
                }
            }
            return _instance;
        }
    }

    private GameObject prefab;
    private List<GameObject> pool = new List<GameObject>();

    public void Initialize(GameObject pipePrefab, int initialSize = 5)
    {
        this.prefab = pipePrefab;
        for (int i = 0; i < initialSize; i++)
        {
            CreateNewPipe();
        }
    }

    private GameObject CreateNewPipe()
    {
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }

    public GameObject GetFromPool()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // Tự mở rộng nếu hết
        GameObject newObj = CreateNewPipe();
        newObj.SetActive(true);
        return newObj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
