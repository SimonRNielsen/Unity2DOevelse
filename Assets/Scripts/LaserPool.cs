using System.Collections.Generic;
using UnityEngine;

public class LaserPool : MonoBehaviour
{
    public GameObject prefab;
    public int startSize;
    private Stack<GameObject> objectPool = new Stack<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        for (int i = 0; i < startSize; i++)
        {
            GameObject newObj = Instantiate(prefab, transform.position, Quaternion.identity);
            newObj.SetActive(false);
            objectPool.Push(newObj);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReturnObject(GameObject obj)
    {

        obj.SetActive(false);
        objectPool.Push(obj);

    }

    public GameObject GetObject()
    {

        if (objectPool.Count > 0)
            return objectPool.Pop();

        return null;

    }
}
