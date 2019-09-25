using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovment : BaseMovment
{
    private float force = 30000;
    private float rotationSpeed = .01f;


    public GameObject explotion;
 

    override protected void trajectory(){

        Vector3 relativePos = selected.position -  transform.position ;
        Quaternion toRotation = Quaternion.LookRotation(relativePos, Vector3.up);

        //float dist = Vector3.Distance(selected.position, transform.position);
        //float totDist = Vector3.Distance(selected.position, transform.position);
        //GetComponent<CapsuleCollider>().radius =  Mathf.Min(dist/totDist, 0.001f) *1000;

        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime );
        GetComponent<Rigidbody>().AddForce(Vector3.Normalize(transform.forward) * force * Time.deltaTime);
    }

    override protected void goalAchived(){
        Instantiate(explotion, transform.position, Quaternion.identity);
        Destroy(gameObject, 1.5f);
        
    }
}
