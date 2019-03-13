using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float m_Speed = 1;

    NavMeshAgent meshAgent;
    public float m_JumpValue = 5;

    private void Awake()
    {
        meshAgent = GetComponent<NavMeshAgent>();
    }

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        rb.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }

    void Movement()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hAxis, 0f, vAxis) * m_Speed * Time.deltaTime;

        rb.MovePosition(transform.position + movement);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, m_JumpValue, 0), ForceMode.Impulse);
        }
    }
}
