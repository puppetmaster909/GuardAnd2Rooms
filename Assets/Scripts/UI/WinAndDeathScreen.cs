using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAndDeathScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        UIManager.instance.Restart();
    }

    public void Quit()
    {
        UIManager.instance.Quit();
    }
}
