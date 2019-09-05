using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))]
public class FollwOnGaze : MonoBehaviour
{
    private GazeAware _gazeAware;
	private GameObject mainCharector;
	private bool wasSelected = false;
    private float force = 800f;
	// Use this for initialization
	void Start () {
		_gazeAware = GetComponent<GazeAware>();
		mainCharector = GameObject.Find("FirstPersonCharacter");
	}

    // Update is called once per frame
    void Update()
    {
        if (_gazeAware.HasGazeFocus)
        {
            hover();
        }
        
    }

    private void hover(){
        Vector3 side = Vector3.Cross(mainCharector.transform.forward, Vector3.up);
        //transform.LookAt(mainCharector.transform.position);
        Vector3 pos = mainCharector.transform.position + 2* mainCharector.transform.forward+ new Vector3(0,0.5f,0) - 0.8f*side;
        GetComponent<Rigidbody>().AddForce((pos - transform.position) * force * Time.deltaTime);
    }
}
