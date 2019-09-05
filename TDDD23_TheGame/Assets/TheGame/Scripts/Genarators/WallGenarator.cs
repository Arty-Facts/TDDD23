using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenarator : MonoBehaviour
{
    public GameObject wallTile;
    public GameObject mainCharector;
    private Vector3 oldPos = new Vector3(0,0,0);
    private float wallTileRadius = 6f;
    private int mapSize = 8;

    private List<GameObject> activeWallTiles = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

        for(int x = 0; x < mapSize; x++)
        {
            if ( (x-mapSize/2)*wallTileRadius != 0)
                activeWallTiles.Add(Instantiate(wallTile, new Vector3((x-mapSize/2)*wallTileRadius, 0, 0), new Quaternion(0, 1, 0, 1)));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltha = mainCharector.transform.position - oldPos;

        if (Mathf.Abs(deltha.x) > wallTileRadius){
            updateWallTilesX();
        }
    }
    private void updateWallTilesX(){
        foreach(GameObject tile in activeWallTiles)
        {
            float dist = tile.transform.position.x - mainCharector.transform.position.x;
            if (Mathf.Abs(dist) > wallTileRadius*mapSize/2){
                float dir = dist/Mathf.Abs(dist);
                tile.transform.position = new Vector3((int)tile.transform.position.x - dir * mapSize*wallTileRadius, tile.transform.position.y, tile.transform.position.z );
            }
            oldPos.x = (int) mainCharector.transform.position.x;
        }
    }
}
