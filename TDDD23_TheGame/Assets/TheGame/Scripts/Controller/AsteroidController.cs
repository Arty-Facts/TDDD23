using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : BaseController
{
    public GameManager gameManager;
    public GameObject Target;  
    public List<GameObject> Astroids = new List<GameObject>();
    private float SpawnEvery = 2f;
    public bool Spawn = true;
    private float SpawnRange = 400f;
    private float MinRange = 50f;

    private int StartCount = 50;

    private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }

    override public void SetUp(){
        StartCoroutine(SpawnAstroids());
        float maxRange = SpawnRange;
        for (float i = MinRange; i < maxRange; i+= (maxRange - MinRange)/StartCount){
            SpawnRange = i;
            spawnOne();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnAstroids(){
        while(Spawn){
            spawnOne();
            yield return new WaitForSeconds(SpawnEvery);
        }
    }

    private Vector3 SpawnPosition(){
        return new Vector3(Random.Range(-100.0f, 100.0f), Random.Range(0.0f, 30.0f), SpawnRange);
    }
    private void spawnOne(){
            GameObject astroid = Instantiate(Astroids[counter], SpawnPosition(), Quaternion.identity);
            astroid.transform.parent = transform;
            astroid.GetComponent<AsteroidMovment>().Select(Target.transform);
            counter += 1;
            counter %= Astroids.Count;
    }

}
