using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileList : List<Tile>
{
    private MoveCostComparer comparator = new MoveCostComparer();
    public MoveCostComparer getComparator(){
        return comparator;
    }
     public class MoveCostComparer : Comparer<Tile>{
         public override int Compare(Tile tile1, Tile tile2){
             return tile1.distance - tile2.distance;
         }
     }

        
    





   

}



