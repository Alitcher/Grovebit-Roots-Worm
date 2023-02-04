using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    public static Vector3[] HeadDirection = new Vector3[4] { new Vector3(0, 0, 0), new Vector3(0, 0, 180), new Vector3(0, 0, 90), new Vector3(0, 0, 270) };

    public static Direction dir = Direction.Up;
    public static Direction PrevDir;
    public Vector3 PrevPos;

    public virtual void Start()
    {
    }

    protected virtual void Move()
    {

    }
}
public enum Direction
{
    Up,
    Down,
    Left,
    Right
}