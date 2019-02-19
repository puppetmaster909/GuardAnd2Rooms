﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        UIManager.instance.Play();
    }

    public void Restart()
    {
        UIManager.instance.Restart();
    }

    public void Controls()
    {
        UIManager.instance.Controls();
    }
}
