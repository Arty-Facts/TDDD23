using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovment : BaseMovment
{
    private float tumble = 0.5f;
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

}
