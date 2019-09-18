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
    private List<Transform> SelectList = new List<Transform>();

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

    IEnumerator StartShooting(){
        while(true){
            yield return new WaitForSeconds(0.2f);
            shoot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SelectList.Count != 0 && selected == null){
            Transform chosen = SelectList[0];
            float min_dist = Vector3.Distance(Charector.transform.position, chosen.position);
            foreach(Transform candidate in SelectList){
                if (candidate == null)
                    continue;
                float dist = Vector3.Distance(Charector.transform.position, candidate.position);
                if (dist < min_dist){
                    min_dist = dist;
                    chosen = candidate;
                }
            }
            selected = chosen;
            print(SelectList.Count);
        }
        if (selected != null){
            target();
            if (Input.GetMouseButtonDown(0)){
                StartCoroutine(StartShooting());
                shoot();
            }
        }


    }
    private void shoot(){
        if (selected != null){
            GameObject gun = Turrets[counter].transform.Find("GunPos").gameObject;
            GameObject tip = gun.transform.Find("Tip").gameObject;

            GameObject bullet = Instantiate(ammo, tip.transform.position, new Quaternion(0,0,0,1));
            bullet.GetComponent<BulletMovment>().Select(selected);
            counter += 1;
            counter %= Turrets.Count;
            SelectList.Remove(selected);
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
            SelectList.Add(other);
		}else{
            selected = other;
        }
    }
    public void Hitt(Transform obj){
        SelectList.Remove(obj);
    }

}
