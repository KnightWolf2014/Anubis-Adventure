using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] mapPrefabs;
    public float zSpawn = 0;
    public float mapLenght = 25;
    public int numberOfMaps = 10;

    public int size;
    public int mapNum;

    private float valuePLayerManager;

    private List<GameObject> activeMaps = new List<GameObject>();

    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        valuePLayerManager = 0;
        size = mapPrefabs.Length;

        for (int i = 0; i < numberOfMaps; i++)
        {
            if (i == 0)
            {
                for (int j = 0; j < 2; j++)
                {
                    spawnMap(0, 0);
                }
            }
            else
            {
                spawnMap(0, Random.Range(1, mapPrefabs.Length));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        valuePLayerManager += 0.1f;

        if (Mathf.Approximately(valuePLayerManager, 1.0f))
        {

            PlayerManager.numberOfMeters += 1;
            PlayerManager.numberOfPoints += 1;

            valuePLayerManager = 0;
        }

        if (playerTransform.position.z > zSpawn - (numberOfMaps*mapLenght))
        {

            spawnMap(Random.Range(17, mapPrefabs.Length), Random.Range(1, mapPrefabs.Length));
            deleteMap();
        }
    }

    public void spawnMap(int mapIndex, int mapNextIndex)
    {

        if (mapNextIndex == 3 || mapNextIndex == 4 || mapNextIndex == 11 || mapNextIndex == 12)
        {
            GameObject go = Instantiate(mapPrefabs[mapIndex], transform.forward * zSpawn, transform.rotation);
            activeMaps.Add(go);
            zSpawn += 2 * mapLenght;

            GameObject nextGo = Instantiate(mapPrefabs[mapNextIndex], transform.forward * zSpawn, transform.rotation);
            activeMaps.Add(nextGo);
            zSpawn += 2 * mapLenght;

        }
        else
        {
            GameObject go = Instantiate(mapPrefabs[mapIndex], transform.forward * zSpawn, transform.rotation);
            activeMaps.Add(go);
            zSpawn += mapLenght;

            GameObject nextGo = Instantiate(mapPrefabs[mapNextIndex], transform.forward * zSpawn, transform.rotation);
            activeMaps.Add(nextGo);
            zSpawn += mapLenght;

        }
    }

    private void deleteMap()
    {
        Destroy(activeMaps[0]);
        activeMaps.RemoveAt(0);
        Destroy(activeMaps[0]);
        activeMaps.RemoveAt(0);
    }
}
