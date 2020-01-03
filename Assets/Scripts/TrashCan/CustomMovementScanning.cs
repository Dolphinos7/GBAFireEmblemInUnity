using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TestingComment
public class CustomMovementScanning : MonoBehaviour
{
    List<Tile> movableTiles = new List<Tile>();
    Tile startingTile;
    int movement;
    public void getMoveableTiles()
    {
        startingTile = getTargetTile(gameObject);
        startingTile.current = true;
        bool hasChanged = true;
        while (hasChanged)
        {
            int size = movableTiles.Count;





            if (size == movableTiles.Count)
            {
                hasChanged = false;
            }
        }
    }

    public Tile getTargetTile(GameObject target)
    {
        Tile toReturn = null;
        Vector3 halfExtents = new Vector3(.25f, .25f, 1f);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(target.transform.position.x, target.transform.position.y), new Vector2(.25f, .25f), 0f);

        foreach (Collider2D collider in colliders)
        {

            if (collider.tag == "Terrain")
            {
                Tile tile = collider.GetComponent<Tile>();
                toReturn = tile;
            }
        }
        return toReturn;
    }

    public void scanUp(Tile tile){

    }
    public void scanDown(Tile tile){
        
    }
    public void scanLeft(Tile tile){

    }
    public void scanRight(Tile tile){

    }

}
