using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovment : BaseMovment
{
    private float force = 10000f;
    private float speed = 100f;


    override protected void trajectory(){
        Vector3 relativePos = selected.transform.position -  transform.position ;
        Quaternion toRotation = Quaternion.LookRotation(relativePos, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime );
        GetComponent<Rigidbody>().AddForce(Vector3.Normalize(transform.forward) * force * Time.deltaTime);
        //GetComponent<Rigidbody>().AddForce(Vector3.Normalize(selected.position - transform.position) * force * Time.deltaTime);
    }

}
