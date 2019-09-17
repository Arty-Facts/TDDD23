using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Transform selected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void target(){
        throw new Exception("missing function target in chield object");
    }
    virtual public void Select(Transform other){
        selected = other;
    }
    
    public void ReleseSelected(){
        selected = null;
    }
}
