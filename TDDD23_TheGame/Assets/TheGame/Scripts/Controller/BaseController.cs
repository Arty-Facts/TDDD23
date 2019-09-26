using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected GameObject selected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    virtual protected void target(){
        throw new Exception("missing function target in chield object");
    }
    virtual public void Select(GameObject other){
        selected = other;
    }
    
    public void ReleseSelected(){
        selected = null;
    }
}
