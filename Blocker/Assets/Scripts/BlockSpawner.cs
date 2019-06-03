using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> blocks;
    [SerializeField] float timeRespawn = 3f;
    [SerializeField] float minTimeRespwn = 3f;
    [SerializeField] float maxTimeRespawn = 6f;

    private float timeSinceLastSpawn = 0f;
    private float randomTimeRespawn;
    private float newMaxTimeRespawn;


    // Update is called once per frame
    void Update()
    {
        if (timeSinceLastSpawn >= timeRespawn)
        {
            randomTimeRespawn = Random.Range(minTimeRespwn, maxTimeRespawn);
            Spawn();
            timeRespawn = randomTimeRespawn;
            if (maxTimeRespawn >= minTimeRespwn + 4)
            {
                maxTimeRespawn--;
            }
            else if(minTimeRespwn>=2)
            {
                minTimeRespwn--;
            }
        }
        timeSinceLastSpawn += Time.deltaTime;

        
    }

    private void Spawn()
    {
        int blockIndex = Random.Range(0, blocks.Count);
        GameObject block = Instantiate(blocks[blockIndex], transform.position, transform.rotation) as GameObject;
        timeSinceLastSpawn = 0f;
    }
}
