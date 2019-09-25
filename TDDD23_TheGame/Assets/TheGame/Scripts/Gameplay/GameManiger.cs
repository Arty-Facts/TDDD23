using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManiger : MonoBehaviour
{
    public Camera Menu;
    public Camera InGame;
    public Camera EndGame;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(changeCamara());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator changeCamara(){
    while(true){
        Menu.enabled = true;
        InGame.enabled = false;
        EndGame.enabled = false;
        yield return new WaitForSeconds(5f);
        Menu.enabled = false;
        InGame.enabled = true;
        EndGame.enabled = false;
        yield return new WaitForSeconds(5f);
        Menu.enabled = false;
        InGame.enabled = false;
        EndGame.enabled = true;
        yield return new WaitForSeconds(5f);
        // if (selected.GetComponent<Gameplay>().GetText().Length < 1){
        //     break;
        // }
        }
    }
}
