using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovment : BaseMovment
{
    public GameObject explotion;
    private float tumble = 0.2f;
    private Vector3 angularVelocity;
    void Start()
    {
        Thrust = 10000f;
        angularVelocity = Random.insideUnitSphere;
    }
    override protected void trajectory(){
        Vector3 relativePos = selected.position -  transform.position ;
        GetComponent<Rigidbody>().AddForce(Vector3.Normalize(relativePos) * Thrust * Time.deltaTime);
        GetComponent<Rigidbody>().angularVelocity = angularVelocity * tumble;

    }
    override protected void goalAchived(){
        if(gameObject.GetComponent<Gameplay>().GetText() == ""){
            Instantiate(explotion, transform.position, Quaternion.identity);
            base.goalAchived();
        }
        
    }

}
