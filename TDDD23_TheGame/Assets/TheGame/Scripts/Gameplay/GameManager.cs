 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject AstriodController;
    public GameObject EnemyController;

    public GameObject StartController; 
    public GameObject TurretController; 

    public GameObject Menu;
    public GameObject MainCamara;

    public GameObject MiniMap;
    public GameObject EndScreen;

    public EndGameStats endGameStats;

    private int wordsTyped;
    private int keyTyped;
    private int correctKeyTyped;
    public float time;

    public float WPM;
    public float KPM;
    public float CKPM;

    public float ACC;

    private int State = 0;
    public void SetState(int newState){
        State = newState;
    }
    public void AddPoint(){
        wordsTyped += 1;
    }
    public void AddPress(){
        keyTyped += 1;
    }public void AddCorrectPress(){
        correctKeyTyped += 1;
    }

    public void GoToBenchmark(){
        GotoStart();
        State = 1;
    }
    public void GoToGame(){
        GotoStart();
        State = 2;
    }

    public void Exit(){
        Application.Quit();
    }

    void Start()
    {
        setUpMenu();
    }
    void Update()
    {
        if (State == 0 || State == 3){
            updateData();
        }
    }


    // Update is called once per frame
    public void SwitchState()
    {
        StartCoroutine(Signal());
    }
    IEnumerator Signal(){
        yield return new WaitForSeconds(0.5f);
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
        initData();
        EndScreen.SetActive(false);
        MiniMap.SetActive(false);
        MainCamara.SetActive(false);
        Menu.SetActive(true);
        AstriodController.SetActive(true);
        EnemyController.SetActive(true);
        TurretController.GetComponent<TurretController>().AutoShoot();
    }
    private void GotoStart(){
        Menu.SetActive(false);
        MainCamara.SetActive(true);
        StartController.SetActive(true);
        AstriodController.SetActive(false);
        EnemyController.SetActive(false);
        TurretController.GetComponent<TurretController>().EnableControlls();
    }
    private void setUpGame(){
        initData();
        StartController.SetActive(false);
        MiniMap.SetActive(true);
        Menu.SetActive(false);
        MainCamara.SetActive(true);
        AstriodController.SetActive(true);
        EnemyController.SetActive(true);
        State = 3;
    }
    private void setUpBenchmark(){ 
        initData();
        MiniMap.SetActive(true);
        StartController.SetActive(false);
        Menu.SetActive(false);
        MainCamara.SetActive(true);
        AstriodController.SetActive(true);
        State = 3;
    }
    private void setUpEndScreen(){

        endGameStats.SetWPM(WPM.ToString());
        MainCamara.SetActive(false);
        MiniMap.SetActive(false);
        AstriodController.SetActive(false);
        EnemyController.SetActive(false);
        EndScreen.SetActive(true);
        State = 0;
    }
    private void initData(){
        wordsTyped = 0;
        time = Time.time-10; 
        keyTyped = 0;
        correctKeyTyped = 0;
    }
    private void updateData(){
        float t = Time.time - time;
        WPM = wordsTyped/(t/60f);
        KPM = keyTyped/(t/60f);
        CKPM = correctKeyTyped/(t/60f);
        ACC = (float)correctKeyTyped/keyTyped;
        print("WPM: " + WPM.ToString() + " | KPM: " + KPM.ToString() + " | CKPM: " + CKPM.ToString() + " | ACC: " + ACC.ToString());
    }



}
