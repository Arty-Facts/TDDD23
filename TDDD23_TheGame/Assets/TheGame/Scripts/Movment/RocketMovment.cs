using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovment : BaseMovment
{
    private float force = 10000;
    private float rotationSpeed = 15f;

    public RocketController controller;
    public GameObject explotion;
    public void AddController(RocketController c){
        controller = c;
    }

    override protected void trajectory(){

        Vector3 relativePos = selected.position -  transform.position ;
        Quaternion toRotation = Quaternion.LookRotation(relativePos, transform.forward);

        //float dist = Vector3.Distance(selected.position, transform.position);
        float totDist = Mathf.Max(Vector3.Distance(selected.position, transform.position)/10, 10);
        //GetComponent<CapsuleCollider>().radius =  Mathf.Min(dist/totDist, 0.001f) *1000;
        float dist = Vector3.Distance(transform.position, selected.transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed/totDist * Time.deltaTime );
        GetComponent<Rigidbody>().AddForce(Vector3.Normalize(transform.forward) *  (force + force * (dist/100))* Time.deltaTime);
    }

    override protected void goalAchived(){
        Instantiate(explotion, transform.position, Quaternion.identity);
        controller.Hitt(gameObject);
        base.goalAchived();
        
    }
    
}
