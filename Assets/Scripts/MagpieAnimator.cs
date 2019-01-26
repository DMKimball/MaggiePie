using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagpieAnimator : MonoBehaviour
{
    [SerializeField] private float m_fIdleSpeedTolerance = 0.1f;

    private Animator m_MagpieAnim;
    private Rigidbody2D m_Rigidbody2D;
    private GrabAction m_Grabber;

    private bool m_bIsFlying = false;

    // Start is called before the first frame update
    void Start()
    {
        m_MagpieAnim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Grabber = GetComponent<GrabAction>();

        m_MagpieAnim.SetBool("InAir", false);
        m_MagpieAnim.SetBool("Moving", false);
        m_MagpieAnim.SetBool("Carrying", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Rigidbody2D)
        {
            m_MagpieAnim.SetBool("Moving", m_Rigidbody2D.velocity.magnitude > m_fIdleSpeedTolerance);
        }

        if (m_Grabber)
        {
            m_MagpieAnim.SetBool("Carrying", m_Grabber.IsGrabbing());
        }

        m_MagpieAnim.SetBool("InAir", m_bIsFlying);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Grabbable>() == null)
        {
            m_bIsFlying = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Grabbable>() == null)
        {
            m_bIsFlying = true;
        }
    }
}
