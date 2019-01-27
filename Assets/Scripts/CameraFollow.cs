using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_FollowTarget = null;
    [SerializeField] private Transform m_LeftCameraAnchor = null;
    [SerializeField] private Transform m_RightCameraAnchor = null;
    [SerializeField] private Vector2 m_vDistanceTolerance = new Vector2(1.5f, 3.0f);
    [SerializeField] private Vector2 m_vSpeedUpDistance = new Vector2(6.0f, 6.0f);
    [SerializeField] private Vector2 m_vMaxDistance = new Vector2(10.0f, 8.0f);
    [SerializeField] private Vector2 m_vMoveSpeed = new Vector2(3.0f, 3.0f);
    [SerializeField] private float m_fCameraVertAdjust = 2.0f;
    [SerializeField] private float m_fVertAdjustInputCutoff = 0.5f;
    [SerializeField] private float m_fIdleSpeedTolerance = 0.1f;

    private Transform m_CurrentFollowAnchor = null;

    private bool m_bFollowing = false;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentFollowAnchor = m_RightCameraAnchor;
        m_bFollowing = false;
    }

    // Update is called once per frame
    void Update()
    {
        float fHorizontal = Input.GetAxis("Horizontal");
        if (fHorizontal < 0)
        {
            m_CurrentFollowAnchor = m_LeftCameraAnchor;
        }
        if (fHorizontal > 0)
        {
            m_CurrentFollowAnchor = m_RightCameraAnchor;
        }

        float fVertical = Input.GetAxis("Vertical");
        if (fVertical < -m_fVertAdjustInputCutoff)
        {
            Vector3 vLocalAnchorPos = m_CurrentFollowAnchor.localPosition;
            vLocalAnchorPos.y = -m_fCameraVertAdjust;
            m_CurrentFollowAnchor.localPosition = vLocalAnchorPos;
        }
        else
        {
            Vector3 vLocalAnchorPos = m_CurrentFollowAnchor.localPosition;
            vLocalAnchorPos.y = 0;
            m_CurrentFollowAnchor.localPosition = vLocalAnchorPos;
        }

        if (m_CurrentFollowAnchor)
        {
            Vector3 vOffset = transform.position - m_CurrentFollowAnchor.position;
            if (Mathf.Abs(vOffset.x) > m_vDistanceTolerance.x || Mathf.Abs(vOffset.y) > m_vDistanceTolerance.y)
            {
                m_bFollowing = true;
            }
            else if ( m_FollowTarget == null || m_FollowTarget.velocity.magnitude < m_fIdleSpeedTolerance )
            {
                m_bFollowing = false;
            }


            if (m_bFollowing)
            {
                Vector2 vFollowSpeed = m_vMoveSpeed;
                if (m_FollowTarget != null)
                {
                    float fTargetSpeedX = Vector2.Dot(Vector2.right, m_FollowTarget.velocity);
                    float fTargetSpeedY = Vector2.Dot(Vector2.up, m_FollowTarget.velocity);
                    if (Mathf.Abs(vOffset.x) > m_vSpeedUpDistance.x && Mathf.Abs(vFollowSpeed.x) < Mathf.Abs(fTargetSpeedX))
                    {
                        vFollowSpeed.x = fTargetSpeedX;
                    }
                    if (Mathf.Abs(vOffset.y) > m_vSpeedUpDistance.y && Mathf.Abs(vFollowSpeed.y) < Mathf.Abs(fTargetSpeedY))
                    {
                        vFollowSpeed.y = fTargetSpeedY;
                    }
                }

                if (Mathf.Abs(vOffset.x) < vFollowSpeed.x * Time.deltaTime)
                {
                    vOffset.x = 0.0f;
                }
                else
                {
                    vOffset.x = Mathf.Clamp( vOffset.x - Mathf.Sign(vOffset.x) * vFollowSpeed.x * Time.deltaTime, -m_vMaxDistance.x, m_vMaxDistance.x);
                }
                if (Mathf.Abs(vOffset.y) < vFollowSpeed.y * Time.deltaTime)
                {
                    vOffset.y = 0.0f;
                }
                else
                {
                    vOffset.y = Mathf.Clamp(vOffset.y - Mathf.Sign(vOffset.y) * vFollowSpeed.y * Time.deltaTime, -m_vMaxDistance.y, m_vMaxDistance.y);
                }

                Vector3 vNewPos = m_CurrentFollowAnchor.position + vOffset;
                vNewPos.z = transform.position.z;
                transform.position = vNewPos;
            }
        }
    }
}
