using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] m_AudioSources;

    [SerializeField] private AudioClip[] m_JumpSounds;
    [SerializeField] private AudioClip[] m_CarryJumpSounds;
    [SerializeField] private AudioClip[] m_GrabSounds;
    [SerializeField] private AudioClip[] m_ReleaseSounds;
    [SerializeField] private AudioClip m_WalkSound;
    [SerializeField] private AudioClip m_MainTheme;
    [SerializeField] private AudioClip m_VictoryJingle;
    [SerializeField] private AudioClip m_FallingLoop;

    void Start()
    {
        m_AudioSources = GetComponents<AudioSource>();
    }

    public void PlayJump()
    {
        if (m_AudioSources.Length >= 1 && m_JumpSounds.Length >= 1 )
        {
            m_AudioSources[0].clip = m_JumpSounds[Random.Range(0, m_JumpSounds.Length)];
            m_AudioSources[0].Play();
            m_AudioSources[0].loop = false;
        }
    }

    public void PlayCarryJump()
    {
        if (m_AudioSources.Length >= 1 && m_CarryJumpSounds.Length >= 1)
        {
            m_AudioSources[0].clip = m_CarryJumpSounds[Random.Range(0, m_CarryJumpSounds.Length)];
            m_AudioSources[0].Play();
            m_AudioSources[0].loop = false;
        }
    }

    public void PlayWalk()
    {
        if (m_AudioSources.Length >= 1 && !m_AudioSources[0].isPlaying)
        {
            m_AudioSources[0].clip = m_WalkSound;
            m_AudioSources[0].Play();
            m_AudioSources[0].loop = true;
        }
    }

    public void StopWalk()
    {
        if (m_AudioSources.Length >= 1 && m_AudioSources[0].clip == m_WalkSound)
        {
            m_AudioSources[0].Stop();
            m_AudioSources[0].loop = false;
        }
    }

    public void PlayGrab()
    {
        if (m_AudioSources.Length >= 2 && m_GrabSounds.Length >= 1)
        {
            m_AudioSources[1].clip = m_GrabSounds[Random.Range(0, m_GrabSounds.Length)];
            m_AudioSources[1].Play();
            m_AudioSources[1].loop = false;
        }
    }

    public void PlayRelease()
    {
        if (m_AudioSources.Length >= 2 && m_ReleaseSounds.Length >= 1)
        {
            m_AudioSources[1].clip = m_ReleaseSounds[Random.Range(0, m_ReleaseSounds.Length)];
            m_AudioSources[1].Play();
            m_AudioSources[1].loop = false;
        }
    }

    public void PlayMainTheme()
    {
        if (m_AudioSources.Length >= 3 && m_MainTheme != null)
        {
            m_AudioSources[2].clip = m_MainTheme;
            m_AudioSources[2].Play();
        }
    }

    public void PlayVictoryJingle()
    {
        if (m_AudioSources.Length >= 3 && m_VictoryJingle != null)
        {
            m_AudioSources[2].clip = m_VictoryJingle;
            m_AudioSources[2].Play();
        }
    }

    public void PlayFall()
    {
        if (m_AudioSources.Length >= 4 && !m_AudioSources[3].isPlaying)
        {
            m_AudioSources[3].Play();
        }
    }

    public void StopFall()
    {
        if (m_AudioSources.Length >= 4)
        {
            m_AudioSources[3].Stop();
        }
    }

}
