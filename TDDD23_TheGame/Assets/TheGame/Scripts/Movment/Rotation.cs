using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public bool ClockWise = true;
    public float Speed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ClockWise){
            transform.RotateAround(transform.position , transform.forward , Speed * Time.deltaTime);
        }else{
            transform.RotateAround(transform.position , -transform.forward , Speed * Time.deltaTime);
        }
    }
}
