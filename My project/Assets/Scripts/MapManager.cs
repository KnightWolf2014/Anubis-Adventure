using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] mapPrefabs;
    public float zSpawn = 0;
    public float mapLenght = 25;
    public int numberOfMaps = 10;

    public int size;
    public int mapNum;

    private List<GameObject> activeMaps = new List<GameObject>();

    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {

        size = mapPrefabs.Length;

        for (int i = 0; i < numberOfMaps; i++)
        {
            if (i == 0)
            {
                for (int j = 0; j < 4; j++)
                {
                    spawnMap(0);
                }
            }
            else
            {
                spawnMap(0);
                spawnMap(Random.Range(1, mapPrefabs.Length));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z > zSpawn - (numberOfMaps*mapLenght))
        {
            spawnMap(0);
            deleteMap();
            spawnMap(Random.Range(1, mapPrefabs.Length));
            deleteMap();
        }
    }

    public void spawnMap(int mapIndex)
    {
        GameObject go = Instantiate(mapPrefabs[mapIndex], transform.forward * zSpawn, transform.rotation);
        activeMaps.Add(go);
        zSpawn += mapLenght;
    }

    private void deleteMap()
    {
        Destroy(activeMaps[0]);
        activeMaps.RemoveAt(0);
    }
}
