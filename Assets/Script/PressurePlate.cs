using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    // Animator of PressurePlate
    Animator m_anim;
    // Animator of Door
    Animator m_door;
    // Audio Clip
    public AudioClip m_EffectClip;
    public AudioSource m_AudioSource;


    public bool baban;

    // Use this for initialization
    void Start()
    {
        m_anim = GetComponent<Animator>();
        m_door = GetComponent<Animator>();
        m_AudioSource.clip = m_EffectClip;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_anim.SetBool("trigger", true);
            m_door.SetBool("OpenTheDoor", true);
            m_AudioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
