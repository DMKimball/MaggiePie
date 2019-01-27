using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private GrabAction m_GrabberScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGrabber(GrabAction grabScript)
    {
        m_GrabberScript = grabScript;
    }

    public void ReleaseSelf()
    {
        if (m_GrabberScript)
        {
            m_GrabberScript.ReleaseObject();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_GrabberScript)
        {
            m_GrabberScript.OnGrabbedObjectCollisionEnter2D(collision);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (m_GrabberScript)
        {
            m_GrabberScript.OnGrabbedObjectCollisionExit2D(collision);
        }
    }

    public bool IsGrabbedByEnemy() {
        return m_GrabberScript && m_GrabberScript.gameObject.name != "MagpiePC";
    }
}
