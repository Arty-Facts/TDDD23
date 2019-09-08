using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform selected; 
    private float force = 10000f;
    private float speed = 100f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selected != null){
            seak();
        }
        
    }

    public void Select(Transform target){
        selected = target;

    }

    private void seak(){
        Vector3 relativePos = selected.transform.position -  transform.position ;
        Quaternion toRotation = Quaternion.LookRotation(relativePos, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime );
        GetComponent<Rigidbody>().AddForce(Vector3.Normalize(transform.forward) * force * Time.deltaTime);
        //GetComponent<Rigidbody>().AddForce(Vector3.Normalize(selected.position - transform.position) * force * Time.deltaTime);
    }
    void OnCollisionEnter (Collision col)
    {
        Destroy(gameObject,0.1f);
    }
}
