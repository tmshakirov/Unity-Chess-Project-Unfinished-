using System;
using UnityEngine;

[Serializable]
public class Tile
{
    [SerializeField] private Vector2 position;
    public bool occupied;

    public Tile (Vector2 _position)
    {
        position = _position;
    }

    public bool ContainsPos (Vector2 _position)
    {
        return Vector3.Distance (new Vector3 (position.x, 0, position.y), new Vector3(_position.x, 0, _position.y)) < 0.5f;
    }

    public Vector2 GetPosition()
    {
        return position;
    }

    public float GetX()
    {
        return position.x;
    }
    public float GetY()
    {
        return position.y;
    }
}
