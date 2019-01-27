using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogSpawner : MonoBehaviour
{
    [SerializeField] private int m_iSpawnCount = 1000;
    [SerializeField] private Vector2 m_vSpawnBounds = new Vector2(100, 100);
    [SerializeField] private GameObject[] m_Prefabs = { };
    [SerializeField] private float m_fDownWeight = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_iSpawnCount; i++)
        {
            GameObject prefab = m_Prefabs[Random.Range(0, m_Prefabs.Length)];
            Instantiate(prefab, GenerateRandomPoint(), Quaternion.identity);
        }
    }

    private Vector3 GenerateRandomPoint()
    {
        Vector3 vPos = transform.position;

        float xVal = (Random.value * 2.0f - 1.0f) * m_vSpawnBounds.x;
        float yVal = (Mathf.Pow(Random.value, m_fDownWeight) * 2.0f - 1.0f) * m_vSpawnBounds.y;

        vPos.x += xVal;
        vPos.y += yVal;

        return vPos;
    }
}
