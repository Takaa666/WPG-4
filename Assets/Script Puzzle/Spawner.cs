using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> objectToSpawn = new List<GameObject>();
    public bool isRandomized;
    // Start is called before the first frame update
    public void SpawnObject()
{
    int index = isRandomized ? Random.Range(0, objectToSpawn.Count) : 0;
    if(objectToSpawn.Count > 0)
    {
        // Generate a random position within a certain range
        Vector3 randomPosition = new Vector3(
            Random.Range(-5.0f, 1.6f),
            Random.Range(-5.0f, 1.6f),
            Random.Range(-5.0f, 1.6f)
        );

        // Instantiate the GameObject at the random position
        Instantiate(objectToSpawn[index], randomPosition, transform.rotation);
    }
}
    void Start()
    {
        SpawnObject();
       // Instantiate(objectToSpawn, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
