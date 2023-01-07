using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0;
    }

    public void Replay()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    public void Exit()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.GoTo("main");
    }
}