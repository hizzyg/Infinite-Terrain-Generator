using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


public class LandScape : MonoBehaviour
{
    public static LandScape instance;

    public GameObject cube;
    public int m_MapSizeX = 25;
    public int m_MapSizeY = 25;

    public float m_HeightScale = 5f;
    public float m_DetailScale = 5f;

    float seed;

    public NavMeshSurface[] surfaces;
    public GameManager gameManager;

    void Awake()
    {
        /*GenerateWorld();*/
    }

    // Use this for initialization
    void Start()
    {
        BakeWorld();
    }

    void GenerateWorld()
    {
        if (instance == null)
            instance = this;
        gameManager.m_ACubes = new GameObject[m_MapSizeX,m_MapSizeY];
        for (int x = 0; x < m_MapSizeX; x++)
        {
            for (int z = 0; z < m_MapSizeY; z++)
            {
                int y = (int)(Mathf.PerlinNoise((x + seed) / m_DetailScale, (z + seed) / m_DetailScale) * m_HeightScale);
                seed = (float)Network.time;
                GameObject g = Instantiate(cube) as GameObject;
                g.transform.position = new Vector3(x, y, z);
                g.transform.SetParent(this.transform);
                gameManager.m_ACubes[x, z] = g;
            }
        }
    }

    void BakeWorld()
    {
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }
}
