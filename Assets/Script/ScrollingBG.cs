using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBG : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public float spriteWidth = 14.2f;
    private Transform bgA, bgB;

    void Start()
    {
        bgA = transform.GetChild(0);
        bgB = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
        if (bgA.position.x < -spriteWidth)
        {
            Vector3 pos = bgA.position;
            pos.x = bgB.position.x + spriteWidth;
            bgA.position = pos;
        }

        if (bgB.position.x < - spriteWidth)
        {
            Vector3 pos = bgB.position;
            pos.x = bgA.position.x + spriteWidth;
            bgB.position = pos;
        }
    }
}
