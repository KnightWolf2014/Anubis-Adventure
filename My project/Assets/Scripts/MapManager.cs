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

    private Vector3 startPosMap;

    private float valuePLayerManager;

    private List<GameObject> activeMaps = new List<GameObject>();

    public Transform playerTransform;  

    // Start is called before the first frame update
    void Start() {
        valuePLayerManager = 0;
        size = mapPrefabs.Length;
        startPosMap = transform.position;
        startPosMap.z += mapLenght / 2.0f;

        initNewDirectionSpawn();

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

            spawnMap(Random.Range(23, mapPrefabs.Length), Random.Range(1, mapPrefabs.Length));
            deleteMap();
        }
        
    }

    public void spawnMap(int mapIndex, int mapNextIndex)
    {

        if (mapNextIndex == 3 || mapNextIndex == 4 || mapNextIndex == 11 || mapNextIndex == 12 || mapNextIndex == 19) {
            GameObject go = Instantiate(mapPrefabs[mapIndex], startPosMap + transform.forward * zSpawn, transform.rotation);
            activeMaps.Add(go);
            zSpawn += 2 * mapLenght;

            GameObject nextGo = Instantiate(mapPrefabs[mapNextIndex], startPosMap + transform.forward * zSpawn, transform.rotation);
            activeMaps.Add(nextGo);
            zSpawn += 2 * mapLenght;

        } else if (mapNextIndex == 7 || mapNextIndex == 18) {
            GameObject go = Instantiate(mapPrefabs[mapIndex], startPosMap + transform.forward * zSpawn, transform.rotation);
            activeMaps.Add(go);
            zSpawn += mapLenght;

            GameObject nextGo = Instantiate(mapPrefabs[mapNextIndex], startPosMap + transform.forward * zSpawn, Quaternion.Euler(0f, 180f, 0f));
            activeMaps.Add(nextGo);
            zSpawn += mapLenght;
        } else if (mapNextIndex == 8 || mapNextIndex == 20) {
            GameObject go = Instantiate(mapPrefabs[mapIndex], startPosMap + transform.forward * zSpawn, transform.rotation);
            activeMaps.Add(go);
            zSpawn += 2 * mapLenght;

            GameObject nextGo = Instantiate(mapPrefabs[mapNextIndex], startPosMap + transform.forward * zSpawn, Quaternion.Euler(0f, 180f, 0f));
            activeMaps.Add(nextGo);
            zSpawn += 2 * mapLenght;

        } else if (mapNextIndex == 21 || mapNextIndex == 22) {
            GameObject go = Instantiate(mapPrefabs[mapIndex], startPosMap + transform.forward * zSpawn, transform.rotation);
            activeMaps.Add(go);
            zSpawn += mapLenght;

            GameObject nextGo = Instantiate(mapPrefabs[mapNextIndex], startPosMap + transform.forward * zSpawn, Quaternion.Euler(0f, 180f, 0f));
            activeMaps.Add(nextGo);
            zSpawn += mapLenght;


        }  else {
            GameObject go = Instantiate(mapPrefabs[mapIndex], startPosMap + transform.forward * zSpawn, transform.rotation);
            activeMaps.Add(go);
            zSpawn += mapLenght;

            GameObject nextGo = Instantiate(mapPrefabs[mapNextIndex], startPosMap + transform.forward * zSpawn, transform.rotation);
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

    private void initNewDirectionSpawn() {

        for (int i = 0; i < numberOfMaps; i++) {

            if (i == 0) {
                for (int j = 0; j < 2; j++) {
                    spawnMap(0, 0);
                }

            } else {
                int newPart = Random.Range(1, mapPrefabs.Length);

                if (newPart == 21 || newPart == 22) newPart = 0;
                spawnMap(0, newPart);
            }
        }
    }
}
