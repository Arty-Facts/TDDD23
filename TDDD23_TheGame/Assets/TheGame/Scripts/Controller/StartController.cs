using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController : MonoBehaviour
{
    public GameObject Asteroid;
    private GameObject asteroid;
    public GameManager gameManager;
    public TurretController weaponsController;
    private Gameplay gameplay;
    private bool StartGame = true;
    // Start is called before the first frame update
    void OnEnable()
    {
        Setup();
    }

    void OnDisable()
    {
        Teardown();
    }

    private void Teardown(){
        if(asteroid != null){
            Destroy(asteroid);
        }
    }

    public void Setup(){
        StartGame = true;
        asteroid = Instantiate(Asteroid , new Vector3(0,5,20), Quaternion.identity);
        gameplay = asteroid.GetComponent<Gameplay>();
        gameplay.SetText("start");
    }

    // Update is called once per frame
    void Update()
    {
        //weaponsController.Select(asteroid);
        if (gameplay != null && gameplay.GetText().Length == 0 && StartGame){
            gameManager.SwitchState();
            StartGame = false;
        }
    }
}
