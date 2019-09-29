using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : BaseController
{
    // Start is called before the first frame update
    public GameManager gameManager;

    public GameObject Ship;
    public GameObject Target;  
    private float SpawnEvery = 3f;
    public bool Spawn = true;
    private float SpawnRange = 300f;
    private float SpawnVarience = 100;
    void Start()
    {
        //SetUp();
    }
    override public void SetUp(){
        SpawnEvery =  3f;
        StartCoroutine(SpawnEnemyShips());
    }

    IEnumerator SpawnEnemyShips(){
        while(Spawn){
            yield return new WaitForSeconds(SpawnEvery);
            SpawnOne();
            SpawnEvery *= 2;
        }
    }

    private Vector3 SpawnPosition(){
        return new Vector3(Random.Range(-300.0f, 300.0f), Random.Range(0.0f, 100.0f), Random.Range(SpawnRange-SpawnVarience,SpawnRange+SpawnVarience));
    }
    private Vector3 LookPosition(){
        return new Vector3(Random.Range(-500.0f, 500.0f), Random.Range(-30.0f, 200.0f), 0);
    }

    private void SpawnOne(){
        GameObject ship = Instantiate(Ship, SpawnPosition(), Random.rotation);
        ship.transform.parent = transform;
        ship.GetComponent<ShipMovment>().Select(Target.transform);
        ship.transform.LookAt(LookPosition());
    }
}
