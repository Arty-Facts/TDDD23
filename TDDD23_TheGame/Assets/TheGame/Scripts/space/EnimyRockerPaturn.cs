using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnimyRockerPaturn : MonoBehaviour
{
    public GameObject ammo;
    public GameObject Target;  
    public GameObject Mother;    
    private List<GameObject> ammos = new List<GameObject>();

    private int capacity = 300;
    private float force = 1000;
    private float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButtonDown(1)){
            if(ammos.Count < capacity){
                GameObject rocket = Instantiate(ammo, Mother.transform.position, Quaternion.identity);
                rocket.GetComponent<Rigidbody>().AddForce(Vector3.up * force * Time.deltaTime);
                ammos.Add(rocket);
            }
        }

        hunt();
    }

    private void hunt(){
        foreach(GameObject a in ammos)
        {
            Vector3 direction = a.transform.position - Target.transform.position  ;
            Quaternion toRotation = Quaternion.FromToRotation(a.transform.forward, direction);
            a.transform.rotation = Quaternion.Lerp(a.transform.rotation, toRotation, speed * Time.deltaTime);
            a.GetComponent<Rigidbody>().AddForce(-1*Vector3.Normalize(a.transform.forward) * force * Time.deltaTime);
        }
    }
}
