using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGeneration : MonoBehaviour
{
    public bool makeClouds = false;
    public GameObject[] clouds;

    public float spawnInterval;

    public GameObject endPoint;

    private float endPos;

    Vector2 startPos;
    // Start is called before the first frame update
    void Start() {     
        if(makeClouds){
            startPos = transform.position;
            Prewarm();
            Invoke("AttemptSpawn", spawnInterval);  
        }
    }

    void SpawnCloud(Vector2 startPos)
    {
        //spawn random cloud prefab
        GameObject cloud = Instantiate(clouds[UnityEngine.Random.Range(0, clouds.Length)]);

        float startY = UnityEngine.Random.Range(startPos.y -3f, startPos.y +3f);
        cloud.transform.position = new Vector2(startPos.x, startY); 

        float scale = UnityEngine.Random.Range(0.8f, 1.2f);

        cloud.transform.localScale = new Vector2 (scale, scale);

        cloud.GetComponent<CloudMovement>().StartFloating(UnityEngine.Random.Range(0.5f, 1.8f), endPos);
    }

    void AttemptSpawn()
    {
        startPos = transform.position;
        endPos = endPoint.transform.position.x;

        SpawnCloud(startPos);

        Invoke("AttemptSpawn", spawnInterval);
    }
    
    void Prewarm()
    {
        for (int i = 0; i <= 20; i++) 
        {
            Vector2 spawnPos = startPos + Vector2.left * (i * 3);
            SpawnCloud(spawnPos);
        }
    }
}
