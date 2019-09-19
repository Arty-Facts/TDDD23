using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))]
public class TurretSelectOnGaze : MonoBehaviour
{
    private GazeAware _gazeAware;
	private static TurretController weaponsController;
	private bool wasSelected = false;
	public TextAsset textFile; 
	private string text;
	// Use this for initialization
	void Start () {
		text = textFile.text;  //this is the content as string

        //Print the text from the file
        Debug.Log(text);

		_gazeAware = GetComponent<GazeAware>();
		weaponsController = GameObject.Find("Base").GetComponent<TurretController>();
		if(!TobiiAPI.IsConnected || true){
			weaponsController.Select(transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (_gazeAware.HasGazeFocus)
        {
            //weaponsController.Select(transform);
			wasSelected = true;
        }else{
			if(wasSelected){
            	//weaponsController.ReleseSelected();
				wasSelected = false;
			}
        }
	}
	void OnCollisionEnter (Collision col)
    {
		if (col.collider.tag != GetComponent<Collider>().tag)
        	weaponsController.Hitt(transform);
    }
}