using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private bool score = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !score)
        {
            score = true;
            AudioManager.instance.PlayScore();
            GameManager.instance.AddScore();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
