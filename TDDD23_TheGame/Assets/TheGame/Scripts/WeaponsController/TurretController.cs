using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class TurretController : BaseController
{
    public GameObject Turret;
    public GameObject Charector;
    public GameObject ammo;  
    public GameObject Higlighter; 
    private List<GameObject> Turrets = new List<GameObject>();
    private Queue<Transform> SelectQue = new Queue<Transform>();

    private int capacity = 300;
    private float force = 500;
    private float speed = 6f;
    private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        Turrets.Add(Instantiate(Turret,  new Vector3(0.8f, 0.35f, 1.7f), Quaternion.identity));
        Turrets.Add(Instantiate(Turret,  new Vector3(0f, 0.25f, 4.1f), Quaternion.identity));
        Turrets.Add(Instantiate(Turret,  new Vector3(-0.8f, 0.35f, 1.7f), Quaternion.identity));
        Higlighter = Instantiate(Higlighter,  new Vector3(0f, 0f, -10f), Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SelectQue.Count != 0 && selected == null){
            selected = SelectQue.Dequeue();
            print(SelectQue.Count);
        }
        if (selected != null){
            target();
            if (Input.GetMouseButtonDown(0)){

                GameObject gun = Turrets[counter].transform.Find("GunPos").gameObject;
                GameObject tip = gun.transform.Find("Tip").gameObject;

                GameObject bullet = Instantiate(ammo, tip.transform.position, new Quaternion(0,0,0,1));
                bullet.GetComponent<BulletMovment>().Select(selected);
                counter += 1;
                counter %= 3;
            }
        }


    }

    private void target(){
        foreach(GameObject turret in Turrets)
        {
            Vector3 relativePos = selected.position - turret.transform.position;
            Quaternion toRotation = Quaternion.FromToRotation(turret.transform.position, relativePos);
            var newRot = Quaternion.LookRotation(relativePos);

            turret.transform.rotation = Quaternion.Lerp(turret.transform.rotation, newRot, speed * Time.deltaTime);
            turret.transform.rotation = new Quaternion(0,turret.transform.rotation.y,0, 1);

            GameObject gun = turret.transform.Find("GunPos").gameObject;

            gun.transform.rotation = Quaternion.Lerp(gun.transform.rotation, newRot, speed * Time.deltaTime);
            gun.transform.rotation = new Quaternion(gun.transform.rotation.x, turret.transform.rotation.y, 0, 1);

        }
        Vector3 dir = selected.position - Charector.transform.position;
        Higlighter.transform.position = Vector3.Lerp(Higlighter.transform.position, Charector.transform.position + 5f* Vector3.Normalize(dir),  speed * Time.deltaTime);
        Higlighter.transform.LookAt(Charector.transform.position);

    }
    override public void Select(Transform other){
        if(!TobiiAPI.IsConnected){
            SelectQue.Enqueue(other);
		}else{
            selected = other;
        }
    }

}
