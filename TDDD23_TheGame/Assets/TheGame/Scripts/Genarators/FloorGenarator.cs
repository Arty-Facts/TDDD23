using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenarator : MonoBehaviour
{
    public GameObject floorTile;
    public GameObject mainCharector;
    private Vector3 oldPos = new Vector3(0,0,0);
    private float floorTileRadius = 6f;
    private int mapSize = 10;

    private List<GameObject> activeFloorTiles = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < mapSize; x++)
        {
            for(int z = 0; z < mapSize; z++)
            {
                activeFloorTiles.Add(Instantiate(floorTile, new Vector3((x-mapSize/2)*floorTileRadius, 0, (z-mapSize/2)*floorTileRadius), Quaternion.identity));
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltha = mainCharector.transform.position - oldPos;

        if (Mathf.Abs(deltha.x) > floorTileRadius){
            updateFloorTilesX();
        }

        if (Mathf.Abs(deltha.z) > floorTileRadius){
            updateFloorTilesZ();
        }
        // for (int i = 0; i < 10; i++)
        // {
        //     Instantiate(floorTile, new Vector3(i * 2.0F, 0, 0), Quaternion.identity);
        // }
    }
    private void updateFloorTilesX(){
        foreach(GameObject tile in activeFloorTiles)
        {
            float dist = tile.transform.position.x - mainCharector.transform.position.x;
            if (Mathf.Abs(dist) > floorTileRadius*mapSize/2){
                float dir = dist/Mathf.Abs(dist);
                tile.transform.position = new Vector3((int)tile.transform.position.x - dir * mapSize*floorTileRadius, tile.transform.position.y, tile.transform.position.z );
            }
            oldPos.x = (int) mainCharector.transform.position.x;
        }
    }
    private void updateFloorTilesZ(){
        foreach(GameObject tile in activeFloorTiles)
        {
            float dist = tile.transform.position.z - mainCharector.transform.position.z;
            if (Mathf.Abs(dist) > floorTileRadius*mapSize/2){
                float dir = dist/Mathf.Abs(dist);
                tile.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, (int)tile.transform.position.z - dir * mapSize*floorTileRadius);
            }
            oldPos.z = (int) mainCharector.transform.position.z;
        }
    }
}
