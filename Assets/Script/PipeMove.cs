using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMove : MonoBehaviour
{
    public float speed = 4f;

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f) return;
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < -15f)
        {
            SimpleObjectPool.instance.ReturnToPool(gameObject);
        }
    }
}
