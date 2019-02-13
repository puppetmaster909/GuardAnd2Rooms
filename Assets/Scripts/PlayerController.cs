using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Vector3 moveDirection;

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
        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //rb.AddForce(moveDirection * speed);
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
