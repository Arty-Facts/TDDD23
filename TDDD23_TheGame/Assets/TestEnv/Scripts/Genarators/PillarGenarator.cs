using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarGenarator : MonoBehaviour
{
    public GameObject pillar;
    public GameObject mainCharector;
    private Vector3 oldPos = new Vector3(0,0,0);
    private float pillarFreq = 4f;
    private int pillarCount = 8;
    private List<GameObject> activePillars = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < pillarCount; x++)
        {
            for(int z = 0; z < pillarCount; z++)
            {
                if ( (x-pillarCount/2)*pillarFreq != 0)
                    activePillars.Add(Instantiate(pillar, new Vector3((x-pillarCount/2)*pillarFreq, 0, (z-pillarCount/2)*pillarFreq), Quaternion.identity));
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltha = mainCharector.transform.position - oldPos;

        if (Mathf.Abs(deltha.x) > pillarFreq){
            updatePillarsX();
        }

        if (Mathf.Abs(deltha.z) > pillarFreq){
            updatePillarsZ();
        }
    }
    private void updatePillarsX(){
        foreach(GameObject tile in activePillars)
        {
            float dist = tile.transform.position.x - mainCharector.transform.position.x;
            if (Mathf.Abs(dist) > pillarFreq*pillarCount/2){
                float dir = dist/Mathf.Abs(dist);
                tile.transform.position = new Vector3((int)tile.transform.position.x - dir * pillarCount*pillarFreq, 0, tile.transform.position.z );
            }
            oldPos.x = (int) mainCharector.transform.position.x;
        }
    }
    private void updatePillarsZ(){
        foreach(GameObject tile in activePillars)
        {
            float dist = tile.transform.position.z - mainCharector.transform.position.z;
            if (Mathf.Abs(dist) > pillarFreq*pillarCount/2){
                float dir = dist/Mathf.Abs(dist);
                tile.transform.position = new Vector3(tile.transform.position.x, 0, (int)tile.transform.position.z - dir * pillarCount*pillarFreq);
            }
            oldPos.z = (int) mainCharector.transform.position.z;
        }
    }
}
