using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float m_LookRadius = 5f;

    Transform m_target;
    NavMeshAgent m_agent;

    // Use this for initialization
    void Start()
    {
        m_target = PlayerManager.instance.m_Player.transform;
        m_agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float m_distance = Vector3.Distance(m_target.transform.position, transform.position);
        m_agent.SetDestination(m_target.transform.position);
        Vector3[] corner = m_agent.path.corners;
        /*Debug.DrawLine(transform.position, Vector3.up + transform.position, Color.red, 100f);*/
        foreach (Vector3 corners in corner)
        {
            Debug.DrawLine(corners, Vector3.up + corners, Color.red, 100f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_LookRadius);
    }
}
