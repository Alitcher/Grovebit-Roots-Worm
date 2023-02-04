using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    [SerializeField] private GameObject WormTailPrefab;


    [SerializeField] private GameObject WormHead;
    [SerializeField] private List<GameObject> WormBody;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private Vector3[] HeadDirection;
    private Direction dir = Direction.Up;
    private bool isMoving;
    private int minSize = 5;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", 0, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = true;
        }

        if (Input.GetKeyDown(KeyCode.W) && dir != Direction.Down)
        {
            dir = Direction.Up;
        }
        if (Input.GetKeyDown(KeyCode.S) && dir != Direction.Up)
        {
            dir = Direction.Down;
        }
        if (Input.GetKeyDown(KeyCode.A) && dir != Direction.Right)
        {
            dir = Direction.Left;
        }
        if (Input.GetKeyDown(KeyCode.D) && dir != Direction.Left)
        {
            dir = Direction.Right;
        }
    }

    private void Move()
    {
        if (isMoving)
        {
            WormHead.transform.eulerAngles = HeadDirection[(int)dir];

            WormHead.transform.position += transform.up * MoveSpeed;

            //switch (dir)
            //{
            //    case Direction.Up:
                    
            //        break;
            //    case Direction.Down:
            //        WormHead.transform.position -= transform.up * MoveSpeed;
            //        break;
            //    case Direction.Left:
            //        WormHead.transform.position -= transform.right * MoveSpeed;
            //        break;
            //    case Direction.Right:
            //        WormHead.transform.position += transform.right * MoveSpeed;
            //        break;
            //    default:
            //        break;
            //}

        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.tag == "Head" && other.tag == "Food") 
        {
            print("hittingsmth");
            other.gameObject.SetActive(false);
            Instantiate(WormTailPrefab,new Vector3(0,-1,0), Quaternion.identity);
        }
    }

    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
