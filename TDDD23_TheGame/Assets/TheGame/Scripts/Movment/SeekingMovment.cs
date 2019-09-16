using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingMovment : MonoBehaviour
{
    private Transform selected; 
    public float Thrust = 10000f;
    public float RotationSpeed = 100f;
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

        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, RotationSpeed * Time.deltaTime );
        GetComponent<Rigidbody>().AddForce(Vector3.Normalize(transform.forward) * Thrust * Time.deltaTime);
    }
    void OnCollisionEnter (Collision col)
    {
        Destroy(gameObject,0.1f);
    }
}
