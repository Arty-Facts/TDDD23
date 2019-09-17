using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : BaseController
{
    // Start is called before the first frame update
    public GameObject Ship;
    public GameObject Target;  
    public float SpawnEvery = 10f;
    public bool Spawn = true;
    public float SpawnRange = 200f;
    void Start()
    {
        StartCoroutine(SpawnEnemyShips());
    }

    IEnumerator SpawnEnemyShips(){
        while(Spawn){
            yield return new WaitForSeconds(SpawnEvery);
            GameObject ship = Instantiate(Ship, SpawnPosition(), Random.rotation);
            //Ship.GetComponent<ShipMovment>().Select(Target.transform);
            ship.transform.LookAt(LookPosition());
        }
    }

    private Vector3 SpawnPosition(){
        return new Vector3(Random.Range(-300.0f, 300.0f), Random.Range(0.0f, 100.0f), SpawnRange);
    }
    private Vector3 LookPosition(){
        return new Vector3(Random.Range(-200.0f, 200.0f), Random.Range(-30.0f, 100.0f), 0);
    }
}
