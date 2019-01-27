using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    [SerializeField] private Transform m_RespawnPoint = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRespawnPoint(Transform respawnPoint) {
	    m_RespawnPoint = respawnPoint;
    }

    public void Respawn()
    {
        if (m_RespawnPoint)
        {
            transform.position = m_RespawnPoint.position;

            GrabAction grabber = GetComponent<GrabAction>();
            if (grabber)
            {
                grabber.ReleaseObject(0);
            }

            Grabbable grabbable = GetComponent<Grabbable>();
            if (grabbable)
            {
                grabbable.ReleaseSelf(0);
            }

            EnemyBird enemyBird = GetComponent<EnemyBird>();
            if (enemyBird)
            {
                enemyBird.Start();
            }
        }
    }
}
