using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    Vector3 moveDirection;

    Collider[] colliders;

    private Rigidbody rb;

    private AudioSource playerSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);

        if (Input.GetKeyDown("escape") || (Input.GetKeyDown("joystick button 7")))
        {
            UIManager.instance.ShowScreen("PauseMenu");
        }

        if ((Input.GetKeyDown("space") || (Input.GetKeyDown("joystick button 1"))) && !CameraController.instance.menuUp)
        {
            Bark();
        }
    }

    void Bark()
    {
        playerSource.Play();
        colliders = Physics.OverlapSphere(gameObject.transform.position, 50.0f);

        foreach (Collider obj in colliders)
        {
            if (obj.name.Contains("Guard"))
            {
                GameObject currentObj = obj.gameObject;
                currentObj.GetComponent<GuardAI>().SetSearch(gameObject.transform);
            }
        }
    }

}
