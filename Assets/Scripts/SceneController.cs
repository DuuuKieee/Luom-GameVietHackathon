using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public float timeRemaining = 3;
    public bool timerIsRunning = false;
    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                CameraController.Shake();
                gameObject.GetComponent<AudioSource>().Play();
                timeRemaining = Random.Range(1,5);
                // timerIsRunning = true;
            }
        }
    }
}
