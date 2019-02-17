using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    Vector3 moveDirection;

    Collider[] colliders;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
            Application.Quit();
        }

        if (Input.GetKeyDown("space") || (Input.GetKeyDown("joystick button 0")))
        {
            Bark();
        }
    }

    void Bark()
    {
        colliders = Physics.OverlapSphere(gameObject.transform.position, 100.0f);

        foreach(Collider obj in colliders)
        {
            if (obj.name.Contains("Guard"))
            {
                GameObject currentObj = obj.gameObject;
                currentObj.GetComponent<GuardAI>().SetSearch(gameObject.transform);
            }
        }
    }

}
