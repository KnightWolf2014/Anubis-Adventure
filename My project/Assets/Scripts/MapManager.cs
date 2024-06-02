using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject[] mapPrefabs;

    private float zSpawn = 0;
    private float mapLenght = 25;
    private int initScreenMaps = 5;

    private int size;
    private int mapNum;

    private Vector3 startPosMap;

    private float valuePLayerManager;

    private List<GameObject> activeMaps = new List<GameObject>();

    private bool stopSpawn;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject pyramid;

    // Start is called before the first frame update
    void Start() {
        valuePLayerManager = 0;
        size = mapPrefabs.Length;
        startPosMap = transform.position;
        startPosMap.z += mapLenght / 2.0f;

        stopSpawn = false;


        initSpawn();
        StartCoroutine(initDestroyPyramid());

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

        if (Vector3.Distance(playerTransform.position, startPosMap + playerTransform.forward * zSpawn) < 200.0f && !stopSpawn) {
            spawnMap(Random.Range(25, mapPrefabs.Length), Random.Range(1, mapPrefabs.Length));
            deleteMap();

        }
    }

    public void spawnMap(int mapIndex, int mapNextIndex)
    {

        if (mapNextIndex == 3 || mapNextIndex == 4 || mapNextIndex == 11 || mapNextIndex == 12 || mapNextIndex == 19) {
            GameObject go = Instantiate(mapPrefabs[mapIndex], startPosMap + playerTransform.forward * zSpawn, playerTransform.rotation);
            activeMaps.Add(go);
            zSpawn += 2 * mapLenght;

            GameObject nextGo = Instantiate(mapPrefabs[mapNextIndex], startPosMap + playerTransform.forward * zSpawn, playerTransform.rotation);
            activeMaps.Add(nextGo);
            zSpawn += 2 * mapLenght;

        } else if (mapNextIndex == 7 || mapNextIndex == 18) {
            GameObject go = Instantiate(mapPrefabs[mapIndex], startPosMap + playerTransform.forward * zSpawn, playerTransform.rotation);
            activeMaps.Add(go);
            zSpawn += mapLenght;

            GameObject nextGo = Instantiate(mapPrefabs[mapNextIndex], startPosMap + playerTransform.forward * zSpawn, playerTransform.rotation * Quaternion.Euler(0f, 180f, 0f));
            activeMaps.Add(nextGo);
            zSpawn += mapLenght;
        } else if (mapNextIndex == 8 || mapNextIndex == 20) {
            GameObject go = Instantiate(mapPrefabs[mapIndex], startPosMap + playerTransform.forward * zSpawn, playerTransform.rotation);
            activeMaps.Add(go);
            zSpawn += 2 * mapLenght;

            GameObject nextGo = Instantiate(mapPrefabs[mapNextIndex], startPosMap + playerTransform.forward * zSpawn, playerTransform.rotation * Quaternion.Euler(0f, 180f, 0f));
            activeMaps.Add(nextGo);
            zSpawn += 2 * mapLenght;

        } else if (mapNextIndex == 21 || mapNextIndex == 22 || mapNextIndex == 23 || mapNextIndex == 24) {
            GameObject go = Instantiate(mapPrefabs[mapIndex], startPosMap + playerTransform.forward * zSpawn, playerTransform.rotation);
            activeMaps.Add(go);
            zSpawn += mapLenght;

            GameObject nextGo = Instantiate(mapPrefabs[mapNextIndex], startPosMap + playerTransform.forward * zSpawn + playerTransform.right * 54.5f, playerTransform.rotation * Quaternion.Euler(0f, 180f, 0f));
            activeMaps.Add(nextGo);
            zSpawn += mapLenght;

            stopSpawn = true;


        }  else {
            GameObject go = Instantiate(mapPrefabs[mapIndex], startPosMap + playerTransform.forward * zSpawn, playerTransform.rotation);
            activeMaps.Add(go);
            zSpawn += mapLenght;

            GameObject nextGo = Instantiate(mapPrefabs[mapNextIndex], startPosMap + playerTransform.forward * zSpawn, playerTransform.rotation);
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

    public void changeDirection(Vector3 spawn) {
        stopSpawn = false;

        startPosMap = spawn;
        zSpawn = 0;
    }



    private void initSpawn() {

        for (int i = 0; i < initScreenMaps; i++) {

            if (i <= 2) {
                spawnMap(0, 0);
                
            } else {
                int newPart = Random.Range(1, mapPrefabs.Length);
                if (newPart == 21 || newPart == 22 || newPart == 23 || newPart == 24) newPart = 0;
                spawnMap(0, newPart);
            }
        }
    }


    IEnumerator initDestroyPyramid() {
        yield return new WaitForSecondsRealtime(4.0f);
        Destroy(pyramid);

    }
}
