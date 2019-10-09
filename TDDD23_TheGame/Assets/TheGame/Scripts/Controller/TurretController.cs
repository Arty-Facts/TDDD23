using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class TurretController : BaseController
{
    public GameManager gameManager;

    public GameObject Turret;
    public GameObject Charector;
    public GameObject ammo;  
    public GameObject Higlighter; 
    public GameObject WarningHiglighter; 
    public GameObject WarningSound;
    private List<GameObject> Turrets = new List<GameObject>();
    private List<GameObject> SelectList = new List<GameObject>();

    private int capacity = 300;
    private float force = 500;
    private float speed = 6f;
    private int counter = 0;

    private bool endGame = true;

    public bool Controlle = true;
    private bool soundOn = false;
    // Start is called before the first frame update
    void Start()
    {
        Turrets.Add(Instantiate(Turret,  new Vector3(0.82f, 0.07f, 1.7f), Quaternion.identity));
        Turrets.Add(Instantiate(Turret,  new Vector3(0f, -0.08f, 4.1f), Quaternion.identity));
        Turrets.Add(Instantiate(Turret,  new Vector3(-0.82f, 0.07f, 1.7f), Quaternion.identity));
        Higlighter = Instantiate(Higlighter,  new Vector3(0f, 0f, -5f), Quaternion.identity);
        WarningHiglighter = Instantiate(WarningHiglighter,  new Vector3(0f, 0f, -5f), Quaternion.identity);
        
        
    }

    IEnumerator StartShooting(){
        yield return new WaitForSeconds(1f);
        while(!Controlle){
            float dist = 100;
            if (selected != null)
                dist = Vector3.Distance(Charector.transform.position, selected.transform.position);
            yield return new WaitForSeconds(dist/200);
            if (selected != null)
                if(selected.GetComponent<Gameplay>().GetText().Length > 0)
                    gameManager.AddPress();
                    shoot();
            
        }
    }

    // Update is called once per frame
    void Update()
    {   
        string input = "";
        SelectList.Remove(null);

        if (!Controlle && SelectList.Count > 0 && selected == null){
            AutoTarget();
        }

        if (SelectList.Count > 0){
            Warning();
        }

        if (selected != null){
            target();
            input = Input.inputString;

            if (input != "" && validate(input)){
                shoot();
            }
            
            if (selected.GetComponent<Gameplay>().GetText().Length < 1){
                selected.GetComponent<Gameplay>().UpdateText();
                SelectList.Remove(selected);
                selected.GetComponent<GazeAware>().enabled = false;
                selected.GetComponent<BaseMovment>().Kill();
                Higlighter.GetComponent<TextMesh>().text = "";
                selected = null;
            }
        }else{
            Higlighter.transform.Find("Sphere").GetComponent<SphereCollider>().enabled = false;
        }


    }

    private void AutoTarget(){
        GameObject chosen = SelectList[0];
        if(chosen != null){
            float min_dist = Vector3.Distance(Charector.transform.position, chosen.transform.position);
            foreach(GameObject candidate in SelectList){
                if (candidate == null)
                    continue;
                float dist = Vector3.Distance(Charector.transform.position, candidate.transform.position);
                if (dist < min_dist){
                    min_dist = dist;
                    chosen = candidate;
                }
            }
            selected = chosen;
        }

    }

    private void Warning(){
        GameObject chosen = SelectList[0];
        if(chosen != null){
            float min_dist = Vector3.Distance(Charector.transform.position, chosen.transform.position);
            foreach(GameObject candidate in SelectList){
                if (candidate == null)
                    continue;
                float dist = Vector3.Distance(Charector.transform.position, candidate.transform.position);
                if (dist < min_dist){
                    min_dist = dist;
                    chosen = candidate;
                }
            }
            if(min_dist < 30f && chosen != selected){
                if(!soundOn){
                    WarningSound.GetComponent<AudioSource>().Play();
                }
                Vector3 dir = chosen.transform.position - Charector.transform.position;
                WarningHiglighter.transform.position = Vector3.Lerp(WarningHiglighter.transform.position, Charector.transform.position + 5f* Vector3.Normalize(dir),  speed * Time.deltaTime);
                WarningSound.transform.position = chosen.transform.position;
                soundOn = true;
            }else{
                WarningSound.GetComponent<AudioSource>().Stop();
                WarningHiglighter.transform.position = new Vector3(0f, 0f, -5f);
                WarningSound.transform.position = new Vector3(0f, 0f, -5f);
                soundOn = false;
            }
            Vector3 lookPos = WarningHiglighter.transform.position - Charector.transform.position;
            WarningHiglighter.transform.LookAt(Charector.transform.position);

        }
    }

    public void AutoShoot(){
        Controlle = false;
        StartCoroutine(StartShooting());
    }
    public void EnableControlls(){
        SelectList.Clear();
        Controlle = true;
        Higlighter.GetComponent<TextMesh>().text = "";
        selected = null;
        endGame = true;
    }
    private bool validate(string input){
        gameManager.AddPress();
        if (input[0] == selected.GetComponent<Gameplay>().GetText()[0]){
            gameManager.AddCorrectPress();
            return true; 
        }
        return false;
    }
    private void shoot(){
        if (selected != null){
            GameObject gun = Turrets[counter].transform.Find("GunPos").gameObject;
            GameObject tip = gun.transform.Find("Tip").gameObject;

            GameObject bullet = Instantiate(ammo, tip.transform.position, new Quaternion(0,0,0,1));
            bullet.GetComponent<BulletMovment>().Select(selected.transform);
            counter += 1;
            counter %= Turrets.Count;
            selected.GetComponent<Gameplay>().UpdateText();
            //SelectList.Remove(selected);
        }
    }

    private void target(){
        foreach(GameObject turret in Turrets)
        {
            Vector3 relativePos = selected.transform.position - turret.transform.position;
            Quaternion toRotation = Quaternion.FromToRotation(turret.transform.position, relativePos);
            var newRot = Quaternion.LookRotation(relativePos);

            turret.transform.rotation = Quaternion.Lerp(turret.transform.rotation, newRot, speed * Time.deltaTime);
            turret.transform.rotation = new Quaternion(0,turret.transform.rotation.y,0, 1);

            GameObject gun = turret.transform.Find("GunPos").gameObject;

            gun.transform.rotation = Quaternion.Lerp(gun.transform.rotation, newRot, speed * Time.deltaTime);
            gun.transform.rotation = new Quaternion(gun.transform.rotation.x, turret.transform.rotation.y, 0, 1);

        }
        Vector3 dir = selected.transform.position - Charector.transform.position;
        Higlighter.transform.position = Vector3.Lerp(Higlighter.transform.position, Charector.transform.position + 5f* Vector3.Normalize(dir),  speed * Time.deltaTime);
        Vector3 lookPos = Higlighter.transform.position - Charector.transform.position;
        Higlighter.transform.LookAt(Charector.transform.position + 10 * Vector3.Normalize(lookPos));
        Higlighter.GetComponent<TextMesh>().text = selected.GetComponent<Gameplay>().GetText();

    }
    override public void Select(GameObject other){
        if(Controlle){       
            selected = other;
            Higlighter.transform.Find("Sphere").GetComponent<SphereCollider>().enabled = true;
        }
    }

    public void ImIn(GameObject other){
        SelectList.Add(other);
    }
    public void Hitt(GameObject obj){

    }
    void OnCollisionEnter (Collision col){
        if (endGame){
            gameManager.SwitchState();
            endGame = false;
        }
    }

    override public void SetUp(){
    }
    public void Disable() {
        WarningSound.GetComponent<AudioSource>().Stop();
        SelectList.Clear();
        //Controlle = false;
        Higlighter.GetComponent<TextMesh>().text = "";
        selected = null;
        
    }

}
