﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))]
public class TargetRocker : MonoBehaviour
{
    private GazeAware _gazeAware;
	private static TurretControler weaponsControler;
	private bool wasSelected = false;
	// Use this for initialization
	void Start () {
		_gazeAware = GetComponent<GazeAware>();
		weaponsControler = GameObject.Find("TurretControler").GetComponent<TurretControler>();
	}
	
	// Update is called once per frame
	void Update () {
		if (_gazeAware.HasGazeFocus)
        {
            weaponsControler.Select(transform);
			wasSelected = true;
        }else{
			if(wasSelected){
            	//weaponsControler.ReleseSelected();
				wasSelected = false;
			}
        }
	}
	void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.tag == "Ammo")
        {
            //weaponsControler.Hitt(col.gameObject);
        }
    }
}
