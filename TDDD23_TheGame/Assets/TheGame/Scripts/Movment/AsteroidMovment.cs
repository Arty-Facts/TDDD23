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
        Thrust = 1000f;
        angularVelocity = Random.insideUnitSphere;
    }
    override protected void trajectory(){
        float dist = Vector3.Distance(transform.position, selected.transform.position);
        if (selected != null){
            Vector3 relativePos = selected.position -  transform.position ;
            GetComponent<Rigidbody>().AddForce(Vector3.Normalize(relativePos) * (Thrust * (dist/4)) * Time.deltaTime);
        }
        GetComponent<Rigidbody>().angularVelocity = angularVelocity * tumble;

    }
    override protected void goalAchived(){
        Instantiate(explotion, transform.position, Quaternion.identity);
        base.goalAchived();
    }
    override protected void goalMet(){
        GetComponent<Rigidbody>().angularVelocity = angularVelocity * tumble;
    }

}
