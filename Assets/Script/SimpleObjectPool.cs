using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectPool : MonoBehaviour
{
    public static SimpleObjectPool instance;

    public GameObject prefab;
    public int poolSize = 5;

    private List<GameObject> pool = new List<GameObject>();

    void Awake()
    {
        // Gán Singleton duy nhất
        instance = this;
    }

    void Start()
    {
        if (prefab == null)
        {
            Debug.LogError("Error: Pipe Prefab trong SimpleObjectPool chưa được gán! Đừng quên kéo Prefab vào ô Inspector của script.");
            return;
        }

        // Tạo trước một lượng ống nhắm vào Pool
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }

    private GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }

    public GameObject GetFromPool()
    {
        // Tìm 1 object đang không hoạt động trong pool
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // Nếu pool hết, tự động mở rộng thêm (Expandable pool)
        return CreateNewObject();
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
