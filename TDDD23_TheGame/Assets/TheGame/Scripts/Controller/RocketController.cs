using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : BaseController
{
    private static GameManager gameManager;

    public bool Spawn = true;
    public GameObject ammo;
    private List<GameObject> ammos = new List<GameObject>();
    private int capacity = 3;
    private float SpawnEvery;
    private float SleepFore;
    

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        selected = GameObject.Find("Target");
        //SetUp();
    }
    override public void SetUp(){
        StartCoroutine(SpawnRockets());
        SleepFore = 5f;
        capacity = 3;
        SpawnEvery = 0.3f;
    }

    void Update() {
        SleepFore = 5f;

        capacity = (int) gameManager.WPM/4;
    }
    
    IEnumerator SpawnRockets(){
        while(true){
            while(Spawn){
                yield return new WaitForSeconds(SpawnEvery);
                if(ammos.Count < capacity){
                    GameObject rocket = Instantiate(ammo, transform.position,  Random.rotation);
                    rocket.transform.parent = transform.parent.transform;
                    Physics.IgnoreCollision(rocket.GetComponent<Collider>(), GetComponent<Collider>());
                    RocketMovment rocketMovment = rocket.GetComponent<RocketMovment>();
                    rocketMovment.Select(selected.transform);
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
                    SleepFore *= 2;
            }
        }
    }

    public void Hitt(GameObject usedAmmo){
        ammos.Remove(usedAmmo);
    }

}
