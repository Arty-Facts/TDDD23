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
    public void SetWPM(string wpm){
        Wpm.text = "WPM: " + wpm;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
