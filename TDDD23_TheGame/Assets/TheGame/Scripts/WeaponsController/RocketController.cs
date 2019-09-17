using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : BaseController
{
    public bool Spawn = true;
    public GameObject ammo;
    private List<GameObject> ammos = new List<GameObject>();
    private int capacity = 3;
    public float SpawnEvery = .5f;
    public float SleepFore = 5f;
    

    void Start()
    {
        selected = GameObject.Find("Main Camera").GetComponent<Transform>();
        StartCoroutine(SpawnRockets());
    }
    
    IEnumerator SpawnRockets(){
        while(Spawn){

            yield return new WaitForSeconds(SpawnEvery);
            if(ammos.Count < capacity){
                GameObject rocket = Instantiate(ammo, transform.position,  Random.rotation);
                Physics.IgnoreCollision(rocket.GetComponent<Collider>(), GetComponent<Collider>());
                RocketMovment rocketMovment = rocket.GetComponent<RocketMovment>();
                rocketMovment.Init(this, selected);
                ammos.Add(rocket);
            }
        }

        yield return new WaitForSeconds(SleepFore);
    }

    public void Hitt(GameObject usedAmmo){
        ammos.Remove(usedAmmo);
    }

}
