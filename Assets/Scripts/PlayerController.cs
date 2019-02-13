using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Vector3 moveDirection;

    Transform[] guards;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        moveDirection = (moveHorizontal * transform.right + moveVertical * transform.forward).normalized;

        if (Input.GetKeyDown("b"))
        {
            Bark();
        }
    }

    void Bark()
    {
        foreach(Transform tr in guards)
        {

        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = moveDirection * speed * Time.deltaTime;
    }
}
