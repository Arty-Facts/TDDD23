using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameStats : MonoBehaviour
{
    public Text Wpm; 
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetF1(string f1){
        Wpm.text = "F1 Score: " + f1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
