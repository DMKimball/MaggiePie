using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAction : MonoBehaviour
{
    [SerializeField] private Transform m_GrabAnchor = null;
    [SerializeField] private JumpAction m_JumpActionScript= null;
    [SerializeField] private SoundManager m_SoundManager = null;

    private Rigidbody2D m_Rigidbody2D = null;
    private Rigidbody2D m_GrabbedRigidbody2D = null;
    private FixedJoint2D m_ConnectingJoint2D = null;
    private Grabbable m_GrabbableScript = null;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_SoundManager = GetComponent<SoundManager>();
        m_GrabbedRigidbody2D = null;
        m_ConnectingJoint2D = null;
        m_GrabbableScript = null;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
	Grabbable grabbableScript = collider.gameObject.GetComponent<Grabbable>();
	if (m_GrabbableScript == null && grabbableScript != null && collider.attachedRigidbody != null)
        {
            GrabObject(grabbableScript);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
	Grabbable grabbableScript = collision.gameObject.GetComponent<Grabbable>();
	if (m_GrabbableScript == null && grabbableScript != null && collision.otherRigidbody != null)
        {
            GrabObject(grabbableScript);
        }
    }

    public void OnGrabbedObjectCollisionEnter2D(Collision2D collision)
    {
        if (m_JumpActionScript != null)
        {
            m_JumpActionScript.OnGrabbedObjectEnterCollision(collision);
        }
    }

    public void OnGrabbedObjectCollisionExit2D(Collision2D collision)
    {
        if (m_JumpActionScript != null)
        {
            m_JumpActionScript.OnGrabbedObjectExitCollision(collision);
        }
    }

    public void GrabObject(Grabbable grabbableScript)
    {
        ReleaseObject();
        if (m_SoundManager)
        {
            m_SoundManager.PlayGrab();
        }

        m_GrabbableScript = grabbableScript;
        m_GrabbableScript.SetGrabber(this);
        if (m_GrabAnchor != null)
        {
            Vector3 vAnchorOffset = transform.position - m_GrabAnchor.position;
            transform.position = transform.position + vAnchorOffset * 0.5f;

            grabbableScript.transform.position = m_GrabAnchor.position;
        }
        m_GrabbedRigidbody2D = grabbableScript.GetComponent<Rigidbody2D>();
        m_GrabbedRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        m_ConnectingJoint2D = gameObject.AddComponent<FixedJoint2D>();
        m_ConnectingJoint2D.connectedBody = m_GrabbedRigidbody2D;
    }

    public void ReleaseObject()
    {
        if (m_GrabbableScript)
        {
            if (m_SoundManager)
            {
                m_SoundManager.PlayRelease();
            }

            m_GrabbableScript.SetGrabber(null);
            Destroy(m_ConnectingJoint2D);
            m_ConnectingJoint2D = null;
            m_GrabbableScript = null;
        }
    }

    public bool IsGrabbing()
    {
        return m_GrabbableScript != null;
    }
}
