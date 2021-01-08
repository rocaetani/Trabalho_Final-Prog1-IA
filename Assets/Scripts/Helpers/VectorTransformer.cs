using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public static class VectorTransformer
{
    public static Vector2Int NullPoint = new Vector2Int(10000, 10000);

    public static Vector3Int Vector2IntToVector3Int(Vector2Int vector2Int)
    {
        return new Vector3Int(vector2Int.x, vector2Int.y, 0);
    }

    public static Vector2Int Vector3ToVector2Int(Vector3 vector3)
    {
        Vector2Int vector2Int = new Vector2Int(Mathf.FloorToInt(vector3.x),Mathf.FloorToInt(vector3.y));
        return vector2Int;
    }

    public static Vector2Int Vector2IntDown(Vector2Int vector2Int)
    {
        return new Vector2Int(vector2Int.x, vector2Int.y - 1);
    }
    public static Vector2Int Vector2IntLeft(Vector2Int vector2Int)
    {
        return new Vector2Int(vector2Int.x - 1, vector2Int.y);
    }

    public static Vector2Int Vector2IntRight(Vector2Int vector2Int)
    {
        return new Vector2Int(vector2Int.x + 1, vector2Int.y);
    }
    
    public static Vector2Int Vector2IntUp(Vector2Int vector2Int)
    {
        return new Vector2Int(vector2Int.x, vector2Int.y + 1);
    }

    public static Vector2 Vector3ToVector2(Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.y);
    }



}
