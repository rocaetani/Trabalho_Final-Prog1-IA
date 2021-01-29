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
        Vector2Int vector2Int = new Vector2Int(Mathf.RoundToInt(vector3.x),Mathf.RoundToInt(vector3.y));
        return vector2Int;
    }
    
    public static Vector3Int Vector3ToVector3Int(Vector3 vector3)
    {
        Vector3Int vector3Int = new Vector3Int(Mathf.RoundToInt(vector3.x),Mathf.RoundToInt(vector3.y), Mathf.RoundToInt(vector3.z));
        return vector3Int;
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

    public static bool EqualVector3Int(Vector3 vector3)
    {
        if (vector3.Equals(Vector3ToVector3Int(vector3)))
        {
            return true;
        }

        return false;
    }

    public static bool IsAlmostEqualVector3Int(Vector3 vector3)
    {
        Vector3 roundPosition = Vector3ToVector3Int(vector3);
        if (Distance(vector3, roundPosition) < 0.08)
        {
            return true;
        }

        return false;
    }

    public static float Distance(Vector3 fromPosition, Vector3 toPosition)
    {
        return Mathf.Abs(fromPosition.x - toPosition.x) + Mathf.Abs(fromPosition.y - toPosition.y);
    }

    public static Vector2Int DirectionToVector2Int(Vector2Int position, Direction direction)
    {
        switch (direction)
        {
            case Direction.Right: return position + Vector2Int.right;
            case Direction.Left: return position + Vector2Int.left;
            case Direction.Up: return position + Vector2Int.up;
            case Direction.Down: return position + Vector2Int.down;
        }
      
        return Vector2Int.zero;
    }



}
