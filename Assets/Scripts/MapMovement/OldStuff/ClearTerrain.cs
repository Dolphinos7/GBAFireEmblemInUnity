using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTerrain : MonoBehaviour
{
    TerrainStats stats;
    
    // Start is called before the first frame update
    void Start()
    {
        stats = new TerrainStats(1, 0, 0);
        
    }


}
