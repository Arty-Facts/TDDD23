using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsControler : MonoBehaviour
{
    public GameObject ammo;
    public GameObject mainCharector;    
    private List<GameObject> ammos = new List<GameObject>();
    private Vector3 selected = new Vector3(0,0,0);
    private bool fallow = true;

    private int capacity = 1;
    private float force = 1000;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < capacity; i++)
        {
            ammos.Add(Instantiate(ammo, new Vector3(0, 0, 0), Quaternion.identity));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            fier();
            fallow = false;
        }

        if (Input.GetMouseButtonDown(1)){
            fallow = true;
        }

        if(fallow){
            ready();
        }

    }
    public void Select(Vector3 position){
        selected = position;
    }

    private void fier(){
        foreach(GameObject a in ammos)
        {
            a.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(selected - a.transform.position) * force);
        }
    }

    private void ready(){
        foreach(GameObject a in ammos)
        {
            a.transform.position = mainCharector.transform.position + 1* mainCharector.transform.forward;
        }
    }
}
