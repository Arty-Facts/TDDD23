using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponsControler : MonoBehaviour
{
    public GameObject ammo;
    public GameObject Target;  
    public GameObject Mother;   
    public GameObject debugHighlighter; 
    private List<GameObject> ammos = new List<GameObject>();

    private int capacity = 300;
    private float force = 800;
    private float speed = 10f;
    private GameObject highlighter;
    private Transform selected;
    // Start is called before the first frame update
    void Start()
    {
        highlighter = Instantiate(debugHighlighter, Mother.transform.position, Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)){
            if(ammos.Count < capacity){
                GameObject rocket = Instantiate(ammo, Mother.transform.position, new Quaternion(-1,-1,0,1));
                rocket.GetComponent<Rigidbody>().AddForce(Vector3.up * 10*force * Time.deltaTime);
                ammos.Add(rocket);
            }
        }

        hunt();
        //debugger.transform.position = Target.transform.position;
    }

    private void hunt(){
        foreach(GameObject a in ammos)
        {
            Vector3 relativePos = Target.transform.position -  a.transform.position ;
            Quaternion toRotation = Quaternion.LookRotation(relativePos, Vector3.up);

            float dist = Vector3.Distance(Target.transform.position, a.transform.position);
            float totDist = Vector3.Distance(Target.transform.position, Mother.transform.position);
            //a.GetComponent<CapsuleCollider>().radius =  Mathf.Min(dist/totDist, 0.001f) *1000;

            a.transform.rotation = Quaternion.Lerp(a.transform.rotation, toRotation, speed * Time.deltaTime * totDist/dist );
            a.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(a.transform.forward) * force * Time.deltaTime);
        }
    }
    public void Select(Transform other){
        selected = other;
        highlighter.transform.position = other.position;
        highlighter.transform.LookAt(Target.transform.position);
    }
    
    public void ReleseSelected(){
        selected = null;
        highlighter.transform.position = new Vector3(0,-10,0);
    }
    public void Hitt(GameObject usedAmmo){
        ammos.Remove(usedAmmo);
        Destroy(usedAmmo, 0.3f);
        selected = null;
        highlighter.transform.position = new Vector3(0,-10,0);
    }

}
