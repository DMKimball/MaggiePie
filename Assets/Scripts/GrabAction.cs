using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAction : MonoBehaviour
{
    [SerializeField] private Transform m_GrabAnchor = null;
    [SerializeField] private SoundManager m_SoundManager = null;
    [SerializeField] private bool m_bIsPlayer = false;

    private FixedJoint2D m_ConnectingJoint2D = null;
    private Grabbable m_GrabbableScript = null;
    private float _grabDisabledTime;

    // Start is called before the first frame update
    void Start()
    {
        m_SoundManager = GetComponent<SoundManager>();
        m_ConnectingJoint2D = null;
        m_GrabbableScript = null;
	_grabDisabledTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
	_grabDisabledTime = Mathf.Max(0, _grabDisabledTime - Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
	Grabbable grabbableScript = collider.gameObject.GetComponent<Grabbable>();
	if (collider.attachedRigidbody != null)
        {
            GrabObject(grabbableScript);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
	Grabbable grabbableScript = collision.gameObject.GetComponent<Grabbable>();
	if (collision.rigidbody != null)
        {
            GrabObject(grabbableScript);
        }
    }

    public void OnGrabbedObjectCollisionEnter2D(Collision2D collision)
    {
	    var jumpAction = GetComponent<JumpAction>();
	    if (jumpAction)
		    jumpAction.OnGrabbedObjectEnterCollision(collision);
    }

    public void OnGrabbedObjectCollisionExit2D(Collision2D collision)
    {
	    var jumpAction = GetComponent<JumpAction>();
	    if (jumpAction)
		    jumpAction.OnGrabbedObjectExitCollision(collision);
    }

    public void GrabObject(Grabbable grabbableScript)
    {
	    if (!(m_GrabbableScript == null && grabbableScript != null))
		    return;

	    if (_grabDisabledTime > 0)
		    return;

        ReleaseObject(0);

        if (m_SoundManager)
        {
            m_SoundManager.PlayGrab();
        }

        m_GrabbableScript = grabbableScript;
        m_GrabbableScript.ReleaseSelf(1);
        m_GrabbableScript.SetGrabber(this);
        if (m_GrabAnchor != null)
        {
            Vector3 vAnchorOffset = transform.position - m_GrabAnchor.position;
            transform.position = transform.position + vAnchorOffset * 0.5f;

            grabbableScript.transform.position = m_GrabAnchor.position;
        }
        var grabbedRigidbody2D = grabbableScript.GetComponent<Rigidbody2D>();
        grabbedRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        var grabbedCollider = grabbableScript.GetComponent<Collider2D>();
	    grabbedCollider.isTrigger = !m_bIsPlayer;
        m_ConnectingJoint2D = gameObject.AddComponent<FixedJoint2D>();
        m_ConnectingJoint2D.connectedBody = grabbedRigidbody2D;
        if (!m_bIsPlayer)
        {
            SendMessage("OnGrabObject", grabbableScript);
        }
    }

    public void ReleaseObject(float disableGrabTime)
    {
        if (m_GrabbableScript)
        {
            if (m_SoundManager)
            {
                m_SoundManager.PlayRelease();
            }

            m_GrabbableScript.SetGrabber(null);
	        var grabbedCollider = m_GrabbableScript.GetComponent<Collider2D>();
	        grabbedCollider.isTrigger = false;
            Destroy(m_ConnectingJoint2D);
            m_ConnectingJoint2D = null;
            if (!m_bIsPlayer)
            {
	            SendMessage("OnReleaseObject", disableGrabTime);
            }
            m_GrabbableScript = null;
	        _grabDisabledTime = disableGrabTime;
        }
    }

    public bool IsGrabbing()
    {
        return m_GrabbableScript != null;
    }

    public void OnRespawn() {
        ReleaseObject(0);
    }
}
