using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryColission : MonoBehaviour
{
    private BoxCollider box;
    private AudioSource winSound;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider>();
        winSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            winSound.Play();
            UIManager.instance.ShowScreen("WinScreen");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
