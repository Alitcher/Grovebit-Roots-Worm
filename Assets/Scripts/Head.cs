using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : Worm
{


    [SerializeField] private Tail WormTailPrefab;


    [SerializeField] private GameObject WormHead;
    [SerializeField] private List<Tail> WormTails;


    private int minSize = 5;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        InvokeRepeating("Move", 0, GameManager.Instance.GameSpeed);
        GameManager.Instance.WormPos.Add(WormHead.transform.position);
        foreach (var item in WormTails)
        {
            GameManager.Instance.WormPos.Add(item.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.IsGameover)
        {
            GameManager.Instance.IsMoving = true;
        }

        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            SpawnTail();
        }

        if (GameManager.Instance.IsMoving) 
        {
            if (Input.GetKeyDown(KeyCode.W) && dir != Direction.Down)
            {
                PrevDir = dir;

                dir = Direction.Up;
            }
            if (Input.GetKeyDown(KeyCode.S) && dir != Direction.Up)
            {
                PrevDir = dir;
                dir = Direction.Down;
            }
            if (Input.GetKeyDown(KeyCode.A) && dir != Direction.Right)
            {
                PrevDir = dir;
                dir = Direction.Left;
            }
            if (Input.GetKeyDown(KeyCode.D) && dir != Direction.Left)
            {
                PrevDir = dir;
                dir = Direction.Right;
            }
        }

    }

    private void updateWormPos() 
    {
        for (int i = 0; i < GameManager.Instance.WormPos.Count; i++)
        {
            if (i == 0)
                GameManager.Instance.WormPos[i] = WormHead.transform.position;
            else 
            {
                GameManager.Instance.WormPos[i] = WormTails[i-1].transform.position;
            }
        }
    }

    protected override void Move()
    {
        if (GameManager.Instance.IsMoving)
        {
            this.transform.eulerAngles = HeadDirection[(int)dir];

            PrevPos = this.transform.position;
            this.transform.position += transform.up * 1;

        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.tag == "Head" && other.tag == "Food")
        {
            updateWormPos();
            GameManager.Instance.OnFoodRepos.Invoke();
            SpawnTail();

        }
        else if (other.tag == "Tail") 
        {
            GameManager.Instance.IsMoving = false;
            GameManager.Instance.OnGameOver.Invoke();
        }


    }

    private void SpawnTail() 
    {
        GameManager.Instance.WormPos.Add(newTailPos());
        Tail newTail = Instantiate(WormTailPrefab, newTailPos(), Quaternion.identity, GameManager.Instance.Map);
        WormTails.Add(newTail);
        newTail.name = $"WormTail ({WormTails.Count - 1})";
        if ((WormTails.Count == 0))
            newTail.SetParent(this);
        else
            newTail.SetParent(WormTails[WormTails.Count - 2]);

        WormTails[WormTails.Count - 2].SetTailChild(newTail);

        Vector3 newTailPos()
        {
            switch (dir)
            {
                case Direction.Up:
                    return WormTails[WormTails.Count - 1].transform.position + Vector3.down;
                case Direction.Down:
                    return WormTails[WormTails.Count - 1].transform.position + Vector3.up;
                case Direction.Left:
                    return WormTails[WormTails.Count - 1].transform.position + Vector3.right;
                case Direction.Right:
                    return WormTails[WormTails.Count - 1].transform.position + Vector3.left;
                default:
                    return Vector3.zero;
            }
        }

    }

}
