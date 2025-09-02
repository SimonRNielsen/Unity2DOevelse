using UnityEngine;
using System.Collections.Generic;

public class EnemyPool : MonoBehaviour
{

    public GameObject enemyPrefab;
    public int startSize;
    private Stack<GameObject> objectPool = new Stack<GameObject>();
    private const float respawnTimer = 2.5f;
    private float timer = 0;
    private int spawned = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        for (int i = 0; i < startSize; i++)
        {
            GameObject newObj = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newObj.SetActive(false);
            objectPool.Push(newObj);
        }

    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (timer > respawnTimer && spawned < 20)
        {
            timer = 0;
            GameObject spawn = GetObject();
            if (spawn != null)
            {
                spawn.SetActive(true);
                EnemyStats stats = spawn.GetComponent<EnemyStats>();
                if (stats != null)
                    stats.Load();
            }
        }

    }

    public void ReturnObject(GameObject obj)
    {

        obj.SetActive(false);
        objectPool.Push(obj);

    }

    public GameObject GetObject()
    {

        if (objectPool.Count > 0)
        {
            spawned++;
            return objectPool.Pop();
        }

        return null;

    }
}
