using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : BaseController
{
    public GameObject Target;  
    public List<GameObject> Astroids = new List<GameObject>();
    public float SpawnEvery = 1f;
    public bool Spawn = true;

    private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAstroids());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnAstroids(){
        while(Spawn){
            GameObject astroid = Instantiate(Astroids[counter], SpawnPosition(), Quaternion.identity);
            astroid.GetComponent<AsteroidMovment>().Select(Target.transform);
            counter += 1;
            counter %= Astroids.Count;
            yield return new WaitForSeconds(SpawnEvery);
        }
    }

    private Vector3 SpawnPosition(){
        return new Vector3(Random.Range(-300.0f, 300.0f), Random.Range(0.0f, 100.0f), 200);
    }

}
