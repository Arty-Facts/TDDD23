using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController : MonoBehaviour
{
    public GameObject Asteroid;
    public GameManager gameManager;
    private Gameplay gameplay;
    private bool StartGame = true;
    // Start is called before the first frame update
    void Start()
    {
        GameObject asteroid = Instantiate(Asteroid , new Vector3(0,5,20), Quaternion.identity);
        gameplay = asteroid.GetComponent<Gameplay>();
        gameplay.SetText("start");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameplay != null && gameplay.GetText().Length == 0 && StartGame){
            gameManager.SwitchState();
            StartGame = false;
        }
    }
}
