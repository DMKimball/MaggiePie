using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
	[SerializeField] private float m_fJumpForce = 10.0f;
	[SerializeField] private float m_fHorizontalSpeed = 2.5f;
	[SerializeField] private float m_fHorizontalInputCutoff = 0.75f;

	private Rigidbody2D m_Rigidbody = null;
	private bool m_bIsJumping = false;

    // Start is called before the first frame update
    void Start()
    {
		m_Rigidbody = GetComponent<Rigidbody2D>();
		m_bIsJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetAxis ("Jump") != 0.0f) {
			if (!m_bIsJumping) {
				m_Rigidbody.AddForce (new Vector2 (0.0f, 1.0f) * m_fJumpForce, ForceMode2D.Impulse);
			}
			m_bIsJumping = true;
		} else {
			m_bIsJumping = false;
		}

		float fHorizontal = Input.GetAxis ("Horizontal");
		if (Mathf.Abs(fHorizontal) > m_fHorizontalInputCutoff) {
			Vector2 vSpeed = m_Rigidbody.velocity;
			vSpeed.x = fHorizontal * m_fHorizontalSpeed;
			m_Rigidbody.velocity = vSpeed;
		}
    }
}
