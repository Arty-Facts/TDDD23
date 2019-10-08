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
	// Use this for initialization
	void Start () {

		_gazeAware = GetComponent<GazeAware>();
		weaponsController = GameObject.Find("Base").GetComponent<TurretController>();
		// if (TobiiAPI.IsConnected){
		// 	weaponsController.autoSelector = true;
		// 	weaponsController.Select(gameObject);

		// }else{
		// 	weaponsController.autoSelector = false;
		// }
		weaponsController.ImIn(gameObject);

	}
	
	// Update is called once per frame
	void Update () {
		if (_gazeAware.HasGazeFocus)
        {
            weaponsController.Select(gameObject);
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
        	weaponsController.Hitt(gameObject);
    }
    void OnDestroy() {
        
    }
}