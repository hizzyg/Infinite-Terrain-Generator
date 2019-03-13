using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator m_anim;
    public bool m_Testanim;

    // Use this for initialization
    void Start()
    {
        m_anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_anim.SetBool("OpenTheDoor", true);
        }
    }

    private void Update()
    {
        if(m_Testanim == true)
        {
            m_anim.SetBool("OpenTheDoor", true);
        }
    }
}
