using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 2D Array Cube
    /// </summary>
    public GameObject[,] m_ACubes;
    /// <summary>
    /// 3D Array Cube
    /// </summary>
    public GameObject[,,] m_A3Cubes;
    /// <summary>
    /// Cube
    /// </summary>
    public GameObject m_Cube;
    /// <summary>
    /// Brown
    /// </summary>
    public GameObject m_DoorCube;
    /// <summary>
    /// Door
    /// </summary>
    public GameObject m_Door;
    /// <summary>
    /// Pressure Plate
    /// </summary>
    public GameObject m_PressurePlate;
    /// <summary>
    /// Map Size (X)
    /// </summary>
    public int m_MapSizeX = 25;
    /// <summary>
    /// Map Size Z
    /// </summary>
    public int m_MapSizeZ = 25;
    /// <summary>
    /// Map Height
    /// </summary>
    public int m_HeightScale = 5;
    /// <summary>
    /// Map Detail
    /// </summary>
    public float m_DetailScale = 5f;
    /// <summary>
    /// World Seed
    /// </summary>
    float m_seed;
    /// <summary>
    /// NavMeshSurface
    /// </summary>
    public NavMeshSurface[] m_Surfaces;

    public Mesh m_WallMesh;
    public Material m_WallMaterial;
    private void Awake()
    {
        // Generate the World
        GenerateWorld();
        BakeWorld();
        AddDoor(24, 13, m_DoorCube, m_Door, m_PressurePlate);
    }

    // Use this for initialization
    void Start()
    {
        // Add the NavMeshLinks
        AddNavMeshLinks();
    }

    // Update is called once per frame
    void Update()
    {
    }
    /// <summary>
    /// Add the NavMeshLinks (Function)
    /// </summary>
    void AddNavMeshLinks()
    {
        GameObject currentCube;
        GameObject nextCube;
        NavMeshLink navMeshLink;
        for (int x = 0; x < m_ACubes.GetLength(0); x++)
        {
            for (int z = 0; z < m_ACubes.GetLength(1); z++)
            {
                // CURRENT CUBE
                currentCube = m_ACubes[x, z];
                if (currentCube.CompareTag("Cube"))
                {
                    // RECHTS
                    if ((x + 1) < m_ACubes.GetLength(0))
                    {
                        // NEXT CUBE
                        nextCube = m_ACubes[x + 1, z];
                        if (nextCube.CompareTag("Cube"))
                        {
                            if (currentCube.transform.position.y != nextCube.transform.position.y)
                            {
                                navMeshLink = new GameObject("NavMeshLink", typeof(NavMeshLink)).GetComponent<NavMeshLink>();
                                navMeshLink.startPoint = new Vector3(currentCube.transform.position.x,
                                                                     currentCube.transform.position.y + 0.5f,
                                                                     currentCube.transform.position.z);
                                navMeshLink.endPoint = new Vector3(nextCube.transform.position.x,
                                                                   nextCube.transform.position.y + 0.5f,
                                                                   nextCube.transform.position.z);
                            }
                        }
                    }
                    // OBEN
                    if ((z + 1) < m_ACubes.GetLength(1))
                    {
                        // NEXT CUBE
                        nextCube = m_ACubes[x, z + 1];
                        if (nextCube.CompareTag("Cube"))
                        {
                            if (currentCube.transform.position.y != nextCube.transform.position.y)
                            {
                                navMeshLink = new GameObject("NavMeshLink", typeof(NavMeshLink)).GetComponent<NavMeshLink>();
                                navMeshLink.startPoint = new Vector3(currentCube.transform.position.x,
                                                                     currentCube.transform.position.y + 0.5f,
                                                                     currentCube.transform.position.z);
                                navMeshLink.endPoint = new Vector3(nextCube.transform.position.x,
                                                                   nextCube.transform.position.y + 0.5f,
                                                                   nextCube.transform.position.z);
                            }
                        }
                    }
                    // LINKS ECKE
                    if (((x + 1) < m_ACubes.GetLength(0)) && ((z + 1) < m_ACubes.GetLength(1)))
                    {
                        // NEXT CUBE
                        nextCube = m_ACubes[x + 1, z + 1];
                        if (nextCube.CompareTag("Cube"))
                        {
                            if (currentCube.transform.position.y != nextCube.transform.position.y)
                            {
                                navMeshLink = new GameObject("NavMeshLink", typeof(NavMeshLink)).GetComponent<NavMeshLink>();
                                navMeshLink.startPoint = new Vector3(currentCube.transform.position.x,
                                                                     currentCube.transform.position.y + 0.5f,
                                                                     currentCube.transform.position.z);
                                navMeshLink.endPoint = new Vector3(nextCube.transform.position.x,
                                                                   nextCube.transform.position.y + 0.5f,
                                                                   nextCube.transform.position.z);
                            }
                        }
                    }
                    // RECHTS ECKE
                    if (((x - 1) >= 0) && ((z + 1) < m_ACubes.GetLength(1)))
                    {
                        // NEXT CUBE
                        nextCube = m_ACubes[x - 1, z + 1];
                        if (nextCube.CompareTag("Cube"))
                        {
                            if (currentCube.transform.position.y != nextCube.transform.position.y)
                            {
                                navMeshLink = new GameObject("NavMeshLink", typeof(NavMeshLink)).GetComponent<NavMeshLink>();
                                navMeshLink.startPoint = new Vector3(currentCube.transform.position.x,
                                                                     currentCube.transform.position.y + 0.5f,
                                                                     currentCube.transform.position.z);
                                navMeshLink.endPoint = new Vector3(nextCube.transform.position.x,
                                                                   nextCube.transform.position.y + 0.5f,
                                                                   nextCube.transform.position.z);
                            }
                        }
                    }
                }
            }
        }
    }
    int AddDoor(int _x, int _z, GameObject _gameObject, GameObject _door)
    {
        int highestblock = -1;
        for (int y = 0; y < m_HeightScale; y++)
        {
            if (m_A3Cubes[_x, y, _z] != null)
            {
                highestblock = y;
                if (m_A3Cubes[_x, highestblock, _z].CompareTag("Cube"))
                {
                    GameObject go = Instantiate(_gameObject) as GameObject;
                    go.transform.position = new Vector3(_x, highestblock, _z);
                    Destroy(m_A3Cubes[_x, highestblock, _z]);
                    GameObject door = Instantiate(_door) as GameObject;
                    door.transform.position = new Vector3(go.transform.position.x + 0.49f,
                        go.transform.position.y + 0.55f,
                        go.transform.position.z - 0.014f);
                }
            }
        }
        if (highestblock == -1)
        {
            Debug.Log("The highest point has been setted");
            GameObject go = Instantiate(_gameObject) as GameObject;
            go.transform.position = new Vector3(_x, 3, _z);
            GameObject door = Instantiate(_door) as GameObject;
            door.AddComponent<Door>();
            door.transform.position = new Vector3(go.transform.position.x + 0.49f,
                go.transform.position.y + 0.55f,
                go.transform.position.z - 0.014f);
        }
        return highestblock;
    }
    int AddDoor(int _x, int _z, GameObject _gameObject, GameObject _door, GameObject _pressureplate)
    {
        int highestblock = -1;
        for (int y = 0; y < m_HeightScale; y++)
        {
            if (m_A3Cubes[_x, y, _z] != null)
            {
                highestblock = y;
                if (m_A3Cubes[_x, highestblock, _z].CompareTag("Cube"))
                {
                    GameObject go = Instantiate(_gameObject) as GameObject;
                    go.transform.position = new Vector3(_x, highestblock, _z);
                    Destroy(m_A3Cubes[_x, highestblock, _z]);
                    GameObject door = Instantiate(_door) as GameObject;
                    door.transform.position = new Vector3(_x + 0.49f, highestblock + 0.55f, _z - 0.014f);
                    GameObject pp = Instantiate(_pressureplate) as GameObject;
                    pp.transform.position = new Vector3(_x, highestblock + 0.45f, _z);
                    pp.AddComponent<PressurePlate>();
                }
            }
        }
        if (highestblock == -1)
        {
            Debug.Log("The highest point has been setted");
            GameObject go = Instantiate(_gameObject) as GameObject;
            go.transform.position = new Vector3(_x, 3, _z);
            GameObject door = Instantiate(_door) as GameObject;
            door.transform.position = new Vector3(_x + 0.49f, highestblock + 0.55f, _z - 0.014f);
            GameObject pp = Instantiate(_pressureplate) as GameObject;
            pp.transform.position = new Vector3(_x, 3.45f, _z);
            pp.AddComponent<PressurePlate>();
        }
        return highestblock;
    }
    /// <summary>
    /// Generate the World (Function)
    /// </summary>
    void GenerateWorld()
    {
        m_ACubes = new GameObject[m_MapSizeX, m_MapSizeZ];
        m_A3Cubes = new GameObject[m_MapSizeX, m_HeightScale, m_MapSizeZ];

        int height;
        for (int x = 0; x < m_MapSizeX; x++)
        {
            for (int z = 0; z < m_MapSizeZ; z++)
            {
                height = (int)(Mathf.PerlinNoise((x + m_seed) / m_DetailScale, (z + m_seed) / m_DetailScale) * m_HeightScale);
                m_seed = (float)Network.time;
                GameObject g = Instantiate(m_Cube) as GameObject;
                g.transform.position = new Vector3(x, height, z);
                g.transform.SetParent(this.transform);
                m_ACubes[x, z] = g;
                m_A3Cubes[x, height, z] = g;
            }
        }

        /// Create Wall
        // First Wall
        GameObject plane1 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane1.GetComponent<MeshFilter>().sharedMesh = m_WallMesh;
        plane1.GetComponent<Renderer>().material = m_WallMaterial;
        Destroy(plane1.GetComponent<MeshCollider>());
        plane1.AddComponent<BoxCollider>();
        plane1.transform.position = new Vector3(13.19f, 3.78f, 24.55f);
        plane1.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        plane1.transform.localScale = new Vector3(1f, 15f, 30f);
        // Second Wall
        GameObject plane2 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane2.GetComponent<MeshFilter>().sharedMesh = m_WallMesh;
        plane2.GetComponent<Renderer>().material = m_WallMaterial;
        Destroy(plane2.GetComponent<MeshCollider>());
        plane2.AddComponent<BoxCollider>();
        plane2.transform.position = new Vector3(24.55f, 3.78f, 13.19f);
        plane2.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        plane2.transform.localScale = new Vector3(1f, 15f, 30f);
    }
    /// <summary>
    /// Bake the World (Function)
    /// </summary>
    void BakeWorld()
    {
        for (int i = 0; i < m_Surfaces.Length; i++)
        {
            m_Surfaces[i].BuildNavMesh();
        }
    }

    
}
