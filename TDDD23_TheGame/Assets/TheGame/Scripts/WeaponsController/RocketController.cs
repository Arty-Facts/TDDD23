using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : BaseController
{
    public bool Spawn = true;
    public GameObject ammo;
    private List<GameObject> ammos = new List<GameObject>();
    private int capacity = 5;
    private float SpawnEvery = .1f;
    private float SleepFore = 3f;
    

    void Start()
    {
        selected = GameObject.Find("Main Camera").GetComponent<Transform>();
        StartCoroutine(SpawnRockets());
    }
    
    IEnumerator SpawnRockets(){
        while(true){
            while(Spawn){
                yield return new WaitForSeconds(SpawnEvery);
                if(ammos.Count < capacity){
                    GameObject rocket = Instantiate(ammo, transform.position,  Random.rotation);
                    Physics.IgnoreCollision(rocket.GetComponent<Collider>(), GetComponent<Collider>());
                    RocketMovment rocketMovment = rocket.GetComponent<RocketMovment>();
                    rocketMovment.Select(selected);
                    rocketMovment.AddController(this);
                    ammos.Add(rocket);
                }
                if (ammos.Count == capacity){
                    Spawn = false;
                }
            }
            yield return new WaitForSeconds(SleepFore);
            if (ammos.Count == 0){
                    Spawn = true;
                }
        }
    }

    public void Hitt(GameObject usedAmmo){
        ammos.Remove(usedAmmo);
    }

}
