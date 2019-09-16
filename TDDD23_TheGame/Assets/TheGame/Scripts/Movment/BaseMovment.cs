using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovment : MonoBehaviour
{
    protected Transform selected; 
    public float Thrust = 10000f;
    public float RotationSpeed = 100f;
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if (selected != null){
            trajectory();
        }else{
            goalAchived();
        }
        
    }

    public void Select(Transform target){
        selected = target;

    }

    virtual protected void trajectory(){
        Vector3 relativePos = selected.transform.position -  transform.position ;
        Quaternion toRotation = Quaternion.LookRotation(relativePos, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, RotationSpeed * Time.deltaTime );
        GetComponent<Rigidbody>().AddForce(Vector3.Normalize(transform.forward) * Thrust * Time.deltaTime);
    }
    void OnCollisionEnter (Collision col){
        goalAchived();
    }

    private void goalAchived(){
        Destroy(gameObject,0.1f);
    }
}
