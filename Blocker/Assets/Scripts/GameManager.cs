using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject blowParticles;
    [SerializeField] float timeToBlow = 1.5f;
    [SerializeField] GameObject blockSpawners;

    public static GameManager instance=null;

    private List<GameObject> blocks;
    private List<GameObject> blockOnTheSameLine;

    int blockLayer;
    int closedBlockLayer;

    private void Awake()
    {
        blocks = new List<GameObject>();
        blockOnTheSameLine = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);//this

        closedBlockLayer = LayerMask.NameToLayer("ClosedBlocks");
        blockLayer = LayerMask.NameToLayer("Blocks");
    }

    public GameManager(GameObject blowParticles)
    {
        this.blowParticles = blowParticles;
    }

    public void CombineBlocks(GameObject block)
    {
        blockOnTheSameLine.Add(block);
        //Debug.Log(blockOnTheSameLine.Count);
        if (blockOnTheSameLine.Count == GetNumberOfSpawner())
        {
            blockOnTheSameLine=DeleteBlocks(blockOnTheSameLine);
        }
    }
    
    private List<GameObject> DeleteBlocks(List<GameObject> blocks)
    {
        List<GameObject> remainingBlocks = new List<GameObject>();
        foreach(GameObject block in blocks)
        {
            if (block.layer == closedBlockLayer)
            {
                ChangeTheView(block);
                ShowParticles(block);
                remainingBlocks.Add(block);
            }
            else if (block.layer == blockLayer)
            {
                Destroy(block);
                UIManager.instanse.Score();
                ShowParticles(block);
            }
        }
        blocks.Clear();
        return remainingBlocks;
    }

    public void ShowParticles(GameObject block)
    {
        GameObject blockParticles=Instantiate(blowParticles, block.transform.position, block.transform.rotation);
        blockParticles.GetComponent<ParticleSystem>().Play();
        Destroy(blockParticles, timeToBlow);
        AudioManager.PlayBlowSound();
    }

    private void ChangeTheView(GameObject block)
    {
        block.layer = blockLayer;
        block.GetComponent<SpriteRenderer>().enabled = false;
        Transform blockTr = block.transform;
        foreach (Transform child in blockTr)
        {
            child.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private int GetNumberOfSpawner()
    {
        Transform spawnerTransform = blockSpawners.transform;
        int numberOfSpawners = 0;
        foreach (Transform child in spawnerTransform)
        {
            numberOfSpawners++;
        }
        return numberOfSpawners;
    }

    public void PlayerDeath()
    {
        Time.timeScale = 0; //Остановить все блоки
    }
}
