using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pooler : MonoBehaviour
{
    [SerializeField]
    private Road roadPrefab;

    [SerializeField]
    private GameObject player;

    private ObjectPool<Road> pool;

    // Start is called before the first frame update
    void Start()
    {
        pool = new ObjectPool<Road>(() => {
            return Instantiate(roadPrefab);
        }, road => {
            road.gameObject.SetActive(true);
        }, road => {
            road.gameObject.SetActive(false);
        }, road => {
            Destroy(road.gameObject);
        }, true, 10, 20);

        SpawnRoads();
    }

    private void SpawnRoads()
    {
        if (player.transform.position.z == 0)
        {
            Vector3 position = new Vector3(0, 0, player.transform.position.z - 10);
            for (int i = 0; i < 10; i++)
            {
                var road = pool.Get();
                road.transform.position = position;
                road.Init(RemoveRoad);
                position.z += 10;
            }
        }
        else
        {
            var road = pool.Get();
            road.transform.position = new Vector3(0, 0, (int) player.transform.position.z + 80);
            road.Init(RemoveRoad);
        }
    }

    private void RemoveRoad(Road road)
    {
        pool.Release(road);
        SpawnRoads();
    }
}
