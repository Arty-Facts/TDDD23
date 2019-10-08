using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

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
    private GazePoint gazePoint;

    private int wordsTyped;
    private int keyTyped;
    private int correctKeyTyped;
    public float time;
    public float timeOfScreen;

    public float WPM;
    public float KPM;
    public float CKPM;

    public float ACC;
    public float CT;

    private int State = 0;

    private Collection saveData;
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
        
    }
    IEnumerator updater(){
        while(State == 0 || State == 3){
            yield return new WaitForSeconds(1f);
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
                break;
            case 2:
                setUpGame();
                break;
            case 3:
                setUpEndScreen();
                break;
            case 4:
                setUpMenu();
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
        State = 0;
        StartCoroutine(updater());
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
        StartCoroutine(updater());
    }
    private void setUpBenchmark(){ 
        initData();
        MiniMap.SetActive(true);
        StartController.SetActive(false);
        Menu.SetActive(false);
        MainCamara.SetActive(true);
        AstriodController.SetActive(true);
        State = 3;
        StartCoroutine(updater());
    }
    private void setUpEndScreen(){
        endGameStats.SetWPM(WPM.ToString());
        MainCamara.SetActive(false);
        MiniMap.SetActive(false);
        AstriodController.SetActive(false);
        EnemyController.SetActive(false);
        EndScreen.SetActive(true);
        save();
        State = 4;
    }
    private void initData(){
        wordsTyped = 0;
        time = Time.time-10; 
        keyTyped = 0;
        correctKeyTyped = 0;
        timeOfScreen = 0;
        CT = 0;
    }
    private void updateData(){
        float t = Time.time - time;
        WPM = wordsTyped/(t/60f);
        KPM = keyTyped/(t/60f);
        CKPM = correctKeyTyped/(t/60f);
        ACC = (float)correctKeyTyped/keyTyped;
        gazePoint = TobiiAPI.GetGazePoint();
        if (!gazePoint.IsRecent()){
            timeOfScreen += Time.smoothDeltaTime;
        }
        CT = 1 - (timeOfScreen/ (t+10));
        
        //print("WPM: " + WPM + " | KPM: " + KPM + " | CKPM: " + CKPM + " | ACC: " + ACC + " | CT: " + CT);
    }

    private void save(){
        saveData = JsonUtility.FromJson<Collection>(load());
        if (saveData == null){
            saveData = new Collection();
        }
        Storing data = new Storing();
        data.WPM = WPM;
        data.KPM = KPM;
        data.CKPM = CKPM;
        data.ACC = ACC;
        data.CT  = CT;
        data.Time = Time.time - time;
        saveData.items.Add(data);
        string dataToJason = JsonUtility.ToJson(saveData, true);
        store(dataToJason);
        print(dataToJason);
    }
    static void store(string data)
    {
        string path = "Assets/Resources/score.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(data);
        writer.Close();
    }
 
    static string load(){
    
        string path = "Assets/Resources/score.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path); 
        string data = reader.ReadToEnd();
        reader.Close();
        return data;
    }
    
    



}
