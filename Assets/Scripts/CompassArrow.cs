using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassArrow : MonoBehaviour
{
    [SerializeField] private Transform m_Player;
    [SerializeField] private Transform[] m_Targets;
    [SerializeField] private float fBaseAngle = 0.0f;

    private RectTransform m_RectTransform = null;

    // Start is called before the first frame update
    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float fMinDistance = float.PositiveInfinity;
        Transform nearestTarget = null;
        foreach (Transform target in m_Targets)
        {
            BoxCollider2D boxCollider2D = target.GetComponent<BoxCollider2D>();
            float fDistance = Vector3.Distance(target.position, m_Player.position);
            if (fDistance < fMinDistance && boxCollider2D != null && boxCollider2D.enabled)
            {
                nearestTarget = target;
                fMinDistance = fDistance;
            }
        }

        float fAngle = 90.0f + fBaseAngle;
        if (nearestTarget)
        {
            Vector3 vPlayerPos = m_Player.position;
            Vector3 vTargetPos = nearestTarget.position;
            vPlayerPos.z = vTargetPos.z = 0.0f;

            Vector3 vOffset = vTargetPos - vPlayerPos;
            vOffset.Normalize();
            fAngle = Mathf.Atan2(vOffset.y, vOffset.x) * Mathf.Rad2Deg;
            
        }
        Vector3 vEulerAngles = m_RectTransform.localEulerAngles;
        vEulerAngles.z = fAngle + fBaseAngle;
        m_RectTransform.localEulerAngles = vEulerAngles;
    }
}
