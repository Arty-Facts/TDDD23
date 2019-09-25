using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovment : BaseMovment
{
    private float force = 30000;
    private float rotationSpeed = .5f;

    public RocketController controller;
    public GameObject explotion;
    public void AddController(RocketController c){
        controller = c;
    }

    override protected void trajectory(){

        Vector3 relativePos = selected.position -  transform.position ;
        Quaternion toRotation = Quaternion.LookRotation(relativePos, transform.forward);

        //float dist = Vector3.Distance(selected.position, transform.position);
        //float totDist = Vector3.Distance(selected.position, transform.position);
        //GetComponent<CapsuleCollider>().radius =  Mathf.Min(dist/totDist, 0.001f) *1000;

        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime );
        GetComponent<Rigidbody>().AddForce(Vector3.Normalize(transform.forward) * force * Time.deltaTime);
    }

    override protected void goalAchived(){
        if(gameObject.GetComponent<Gameplay>().GetText() == ""){
            Instantiate(explotion, transform.position, Quaternion.identity);
            controller.Hitt(gameObject);
            base.goalAchived();
        }
    }
    
}
