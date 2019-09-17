using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : BaseController
{
    public GameObject ammo;
    private List<GameObject> ammos = new List<GameObject>();
    private int capacity = 300;
    

    void Start()
    {
        selected = GameObject.Find("Main Camera").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)){
            if(ammos.Count < capacity){
                GameObject rocket = Instantiate(ammo, transform.position,  Random.rotation);
                RocketMovment rocketMovment = rocket.GetComponent<RocketMovment>();
                rocketMovment.Init(this, selected);
                ammos.Add(rocket);
            }
        }
    }

    public void Hitt(GameObject usedAmmo){
        ammos.Remove(usedAmmo);
    }

}
