using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSelection : MonoBehaviour
{
    public string terrainName;
    private Transform myTrans;
    public void buildTerrain(string str){
        if (str == "Clear"){
            gameObject.AddComponent<ClearTerrain>();
        }
    }

    public void setTerrainName(string str){
        terrainName = str;
    }

    void Start()
    {
        if (terrainName == "Plains"){
            gameObject.AddComponent<Plains>();
        }
        myTrans = gameObject.GetComponent<Transform>();
        buildTerrain(gameObject.tag);
    }
    
        public void OnMouseEnter()
    {
        GameObject.Find("GameMaster").GetComponent<UIController>().getCursor().GetComponent<Transform>().position = new Vector3(myTrans.position.x, myTrans.position.y, myTrans.position.z);
    }
}
