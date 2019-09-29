using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovment : BaseMovment
{
    private float force = 50000f;
    private float speed = 1;

    override protected void trajectory(){
        Vector3 relativePos = selected.transform.position -  transform.position ;
        Quaternion toRotation = Quaternion.LookRotation(relativePos, Vector3.up);
        float dist = Vector3.Distance(selected.transform.position, transform.position);

        transform.rotation = toRotation;
        transform.position = Vector3.Lerp(transform.position, selected.transform.position, Time.deltaTime*speed);
        speed += 1;
        //GetComponent<Rigidbody>().AddForce(Vector3.Normalize(transform.forward) * force * Time.deltaTime);
        //GetComponent<Rigidbody>().AddForce(Vector3.Normalize(selected.position - transform.position) * force * Time.deltaTime);
    }

}
