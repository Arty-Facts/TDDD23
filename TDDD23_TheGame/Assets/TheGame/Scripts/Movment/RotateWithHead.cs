using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class RotateWithHead : MonoBehaviour
{
	// Use this for initialization
	private float speed = 0.001f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		HeadPose headPose = TobiiAPI.GetHeadPose();
		if (headPose.IsRecent())
        {
			transform.rotation = Quaternion.Lerp(transform.rotation, headPose.Rotation, Mathf.Min(Time.time * speed, 0.02f));
        }
	}
}
