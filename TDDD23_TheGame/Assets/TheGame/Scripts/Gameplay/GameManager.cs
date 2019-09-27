using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject AstriodController;
    public GameObject EnemyController;

    public GameObject StartController; 

    public GameObject Menu;
    public GameObject MainCamara;

    public GameObject MiniMap;
    public GameObject EndScreen;

    private int State = 0;
    public void SetState(int newState){
        State = newState;
    }

    private void GotoStart(){
        Menu.SetActive(false);
        MainCamara.SetActive(true);
        StartController.SetActive(true);
    }

    public void GoToBenchmark(){
        GotoStart();
        State = 1;
    }
    public void GoToGame(){
        GotoStart();
        State = 2;
    }

    void Start()
    {
        setUpMenu();
    }

    // Update is called once per frame
    public void SwitchState()
    {
        StartCoroutine(Signal());
    }
    IEnumerator Signal(){
        yield return new WaitForSeconds(1);
        stateChange();

    }
    private void stateChange(){
        switch (State){
            case 0:
                setUpMenu();
                break;
            case 1:
                setUpBenchmark();
                break;
            case 2:
                setUpGame();
                break;
            case 3:
                setUpEndScreen();
                break;
            default:
                //Console.WriteLine("Default case");
                break;
        }
    }
    private void setUpMenu(){
        EndScreen.SetActive(false);
        MiniMap.SetActive(false);
        MainCamara.SetActive(false);
        Menu.SetActive(true);
    }
    private void setUpGame(){
        StartController.SetActive(false);
        MiniMap.SetActive(true);
        Menu.SetActive(false);
        MainCamara.SetActive(true);
        AstriodController.SetActive(true);
        EnemyController.SetActive(true);
        State = 3;
    }
    private void setUpBenchmark(){
        MiniMap.SetActive(true);
        StartController.SetActive(false);
        Menu.SetActive(false);
        MainCamara.SetActive(true);
        AstriodController.SetActive(true);
        EnemyController.SetActive(true);
        State = 3;
    }
    private void setUpEndScreen(){
        MainCamara.SetActive(false);
        MiniMap.SetActive(false);
        AstriodController.SetActive(false);
        EnemyController.SetActive(false);
        EndScreen.SetActive(true);
        State = 0;
    }


}
