using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurceManager : NetworkBehaviour
{
    public List<SpawnArea> spawnAreas;

    [Header("Resources")]
    [SerializeField] private GameObject rock;
    [SerializeField] private GameObject tree;
    [SerializeField] private GameObject iron;
    [SerializeField] private GameObject gold;
    [SerializeField] private GameObject diamond;

    [Server]
    void Start()
    {
        spawnAreas = new List<SpawnArea>();
        GameObject[] spawnAreasGO = GameObject.FindGameObjectsWithTag("SpawnArea");
        for (int i = 0; i < spawnAreasGO.Length; i++)
        {
            spawnAreas.Add(spawnAreasGO[i].GetComponent<SpawnArea>());
        }
        for (int i = 0; i < spawnAreas.Count; i++)
        {
            spawnResourcesInArea(spawnAreas[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [Server]
    private void spawnResourcesInArea(SpawnArea area)
    {
        if (area.canSpawnRock && rock != null)
        {
            for (int i = 0; i < area.rockCount; i++)
            {
                Vector3 pos = area.GetRandomPosition();
                Vector3 rotation = new Vector3(0, Random.Range(0, 360), 0);
                Vector3 scale = new Vector3(Random.Range(0.8f, 1.2f), Random.Range(0.8f, 1.2f), Random.Range(0.8f, 1.2f)) * 1.5f;
                GameObject resourceInstance = Instantiate(rock, pos, Quaternion.identity);
                resourceInstance.transform.Rotate(rotation);
                resourceInstance.transform.localScale = scale;
                resourceInstance.GetComponent<MinableResource>().setModel(Random.Range(0, resourceInstance.GetComponent<MinableResource>().getModelsLength()));
                NetworkServer.Spawn(resourceInstance);
            }
        }
        if (area.canSpawnTree && tree != null)
        {
            for (int i = 0; i < area.treeCount; i++)
            {
                Vector3 pos = area.GetRandomPosition();
                Vector3 rotation = new Vector3(0, Random.Range(0, 360), 0);
                Vector3 scale = new Vector3(Random.Range(0.8f, 1.2f), Random.Range(0.8f, 1.2f), Random.Range(0.8f, 1.2f)) * 1.5f;
                GameObject resourceInstance = Instantiate(tree, pos, Quaternion.identity);
                resourceInstance.transform.Rotate(rotation);
                resourceInstance.transform.localScale = scale;
                resourceInstance.GetComponent<MinableResource>().setModel(Random.Range(0, resourceInstance.GetComponent<MinableResource>().getModelsLength()));
                NetworkServer.Spawn(resourceInstance);
            }
        }
        if (area.canSpawnIron && iron != null)
        {
            for (int i = 0; i < area.ironCount; i++)
            {
                Vector3 pos = area.GetRandomPosition();
                Vector3 rotation = new Vector3(0, Random.Range(0, 360), 0);
                Vector3 scale = new Vector3(Random.Range(0.8f, 1.2f), Random.Range(0.8f, 1.2f), Random.Range(0.8f, 1.2f)) * 1.5f;
                GameObject resourceInstance = Instantiate(iron, pos, Quaternion.identity);
                resourceInstance.transform.Rotate(rotation);
                resourceInstance.transform.localScale = scale;
                resourceInstance.GetComponent<MinableResource>().setModel(Random.Range(0, resourceInstance.GetComponent<MinableResource>().getModelsLength()));
                NetworkServer.Spawn(resourceInstance);
            }
        }
        if (area.canSpawnGold && gold != null)
        {
            for (int i = 0; i < area.goldCount; i++)
            {
                Vector3 pos = area.GetRandomPosition();
                Vector3 rotation = new Vector3(0, Random.Range(0, 360), 0);
                Vector3 scale = new Vector3(Random.Range(0.8f, 1.2f), Random.Range(0.8f, 1.2f), Random.Range(0.8f, 1.2f)) * 1.5f;
                GameObject resourceInstance = Instantiate(gold, pos, Quaternion.identity);
                resourceInstance.transform.Rotate(rotation);
                resourceInstance.transform.localScale = scale;
                resourceInstance.GetComponent<MinableResource>().setModel(Random.Range(0, resourceInstance.GetComponent<MinableResource>().getModelsLength()));
                NetworkServer.Spawn(resourceInstance);
            }
        }
        if (area.canSpawnDiamond && diamond != null)
        {
            for (int i = 0; i < area.diamondCount; i++)
            {
                Vector3 pos = area.GetRandomPosition();
                Vector3 rotation = new Vector3(0, Random.Range(0, 360), 0);
                Vector3 scale = new Vector3(Random.Range(0.8f, 1.2f), Random.Range(0.8f, 1.2f), Random.Range(0.8f, 1.2f)) * 1.5f;
                GameObject resourceInstance = Instantiate(diamond, pos, Quaternion.identity);
                resourceInstance.transform.Rotate(rotation);
                resourceInstance.transform.localScale = scale;
                resourceInstance.GetComponent<MinableResource>().setModel(Random.Range(0, resourceInstance.GetComponent<MinableResource>().getModelsLength()));
                NetworkServer.Spawn(resourceInstance);
            }
        }


    }

}
