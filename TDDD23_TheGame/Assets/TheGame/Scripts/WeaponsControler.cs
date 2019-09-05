using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsControler : MonoBehaviour
{
    public GameObject ammo;
    public GameObject debugHighlighter;
    public GameObject mainCharector;    
    private List<GameObject> ammos = new List<GameObject>();
    private Transform selected;
    private Vector3 rest = new Vector3(0,0,0);
    private GameObject highlighter;
    private bool hover = true;
    private bool fire = true;

    private float weaponOffset = 0.8f;

    private int capacity = 30;
    private float force = 40000;
    private float hoverForce = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        highlighter = Instantiate(debugHighlighter, new Vector3(0, 0, 0), Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1)){
            if(ammos.Count < capacity){
                ammos.Add(Instantiate(ammo, mainCharector.transform.position, Quaternion.identity));
            }
        }

        if(selected == null){
            hoverInPlace();
        }

    }
    public void Select(Transform other){
        selected = other;
        highlighter.transform.position = other.position;
        highlighter.transform.LookAt(mainCharector.transform.position);
        hunt();
    }
    
    public void ReleseSelected(){
        selected = null;
        highlighter.transform.position = new Vector3(0,0,0);
    }
    public void Hitt(GameObject usedAmmo){
        ammos.Remove(usedAmmo);
        Destroy(usedAmmo, 0.3f);
        selected = null;
        highlighter.transform.position = new Vector3(0,0,0);
    }

    private void hunt(){
        foreach(GameObject a in ammos)
        {
            a.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(selected.position - a.transform.position) * force * Time.deltaTime);
        }
    }

    private void hoverInPlace(){
        foreach(GameObject a in ammos)
        {
            Vector3 side = Vector3.Cross(mainCharector.transform.forward, Vector3.up);
            Vector3 pos = mainCharector.transform.position + 1* mainCharector.transform.forward + weaponOffset * Vector3.Normalize(side);
            a.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(pos - a.transform.position) * force * Time.deltaTime * hoverForce);
        }
    }
}
