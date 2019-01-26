using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAction : MonoBehaviour
{
	[SerializeField] private float m_fJumpSpeed = 10.0f;
	[SerializeField] private float m_fHorizontalSpeed = 2.5f;
	[SerializeField] private float m_fHorizontalInputCutoff = 0.75f;
	[SerializeField] private float m_fJumpResetAngleCutoff = 45.0f;
    [SerializeField] private int m_iMaxJumps = 1;

    private SpriteRenderer m_SpriteRenderer = null;
    private Rigidbody2D m_Rigidbody = null;
    private bool m_bJumpIsPressed = false;
	private int m_iNumJumps = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_bJumpIsPressed = false;
        m_iNumJumps = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Jump") > m_fHorizontalInputCutoff || Input.GetAxis("Vertical") > m_fHorizontalInputCutoff)
        {
            if (!m_bJumpIsPressed)
            {
                if (m_iNumJumps < m_iMaxJumps)
                {
                    Vector2 vSpeed = m_Rigidbody.velocity;
                    vSpeed.y = m_fJumpSpeed;
                    m_Rigidbody.velocity = vSpeed;
                    m_iNumJumps++;
                }
            }
            m_bJumpIsPressed = true;
        }
        else
        {
            m_bJumpIsPressed = false;
        }

		float fHorizontal = Input.GetAxis ("Horizontal");
        if (m_SpriteRenderer)
        {
            if (fHorizontal > m_fHorizontalInputCutoff)
            {
                m_SpriteRenderer.flipX = true;
            }
            else if (fHorizontal < -m_fHorizontalInputCutoff)
            {
                m_SpriteRenderer.flipX = false;
            }
        }
        if (Mathf.Abs(fHorizontal) > m_fHorizontalInputCutoff)
        {
            Vector2 vSpeed = m_Rigidbody.velocity;
            vSpeed.x = fHorizontal * m_fHorizontalSpeed;
            m_Rigidbody.velocity = vSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            if (Vector2.Dot(collision.contacts[i].normal, new Vector2(0.0f, 1.0f)) > Mathf.Cos(m_fJumpResetAngleCutoff * Mathf.Deg2Rad))
            {
                m_iNumJumps = 0;
                break;
            }
        }
    }

    public void OnGrabbedObjectCollision(Collision2D collision)
    {
        OnCollisionEnter2D(collision);
    }
}
