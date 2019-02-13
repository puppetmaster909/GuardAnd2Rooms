using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Vector3 moveDirection;

    Collider[] colliders;
    //List<GameObject> guardsInSphere = new List<GameObject>();

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

        if (Input.GetKeyDown("space"))
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

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = moveDirection * speed * Time.deltaTime;
    }
}
