using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_LeftCameraAnchor = null;
    [SerializeField] private Transform m_RightCameraAnchor = null;
    [SerializeField] private Vector2 m_vDistanceTolerance = new Vector2(1.5f, 3.0f);
    [SerializeField] private Vector2 m_vMaxDistance = new Vector2(10.0f, 8.0f);
    [SerializeField] private Vector2 m_vMoveSpeed = new Vector2(3.0f, 3.0f);
    [SerializeField] private float m_fCameraVertAdjust = 2.0f;
    [SerializeField] private float m_fVertAdjustInputCutoff = 0.5f;

    private Transform m_CurrentFollowAnchor = null;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentFollowAnchor = m_RightCameraAnchor;
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
        if (fVertical > m_fVertAdjustInputCutoff)
        {
            Vector3 vLocalAnchorPos = m_CurrentFollowAnchor.localPosition;
            vLocalAnchorPos.y = m_fCameraVertAdjust;
            m_CurrentFollowAnchor.localPosition = vLocalAnchorPos;
        }
        else if (fVertical < -m_fVertAdjustInputCutoff)
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
            if (Mathf.Abs(vOffset.x) > m_vDistanceTolerance.x)
            {
                float fMin = vOffset.x < 0 ? -m_vMaxDistance.x : 0.0f;
                float fMax = vOffset.x > 0 ? m_vMaxDistance.x : 0.0f;
                vOffset.x = Mathf.Clamp(vOffset.x - Mathf.Sign(vOffset.x) * m_vMoveSpeed.x * Time.deltaTime, fMin, fMax);
            }
            if (Mathf.Abs(vOffset.y) > m_vDistanceTolerance.y)
            {
                float fMin = vOffset.y < 0 ? -m_vMaxDistance.y : 0.0f;
                float fMax = vOffset.y > 0 ? m_vMaxDistance.y : 0.0f;
                vOffset.y = Mathf.Clamp(vOffset.y - Mathf.Sign(vOffset.y) * m_vMoveSpeed.y * Time.deltaTime, fMin, fMax);
            }
            Vector3 vNewPos = m_CurrentFollowAnchor.position + vOffset;
            vNewPos.z = transform.position.z;
            transform.position = vNewPos;
        }
    }
}
