using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : Worm
{

    [SerializeField] private Tail WormTailPrefab;


    [SerializeField] private GameObject WormHead;
    [SerializeField] private List<Tail> WormTails;
    public ParticleSystem FertEffect;


    private int minSize = 5;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        InvokeRepeating("Move", 0, GameManager.Instance.GameSpeed);
        GameManager.Instance.OccupiedPos.Add(WormHead.transform.position);
        foreach (var item in WormTails)
        {
            GameManager.Instance.OccupiedPos.Add(item.transform.position);
        }

        EnableFX();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.IsGameover)
        {
            GameManager.Instance.IsMoving = true;
        }

        //if (Input.GetKeyDown(KeyCode.Z)) 
        //{
        //    SpawnTail();
        //}

        if (GameManager.Instance.IsMoving) 
        {
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

    }

    private void updateWormPos() 
    {
        for (int i = 0; i < GameManager.Instance.OccupiedPos.Count; i++)
        {
            if (i == 0)
                GameManager.Instance.OccupiedPos[i] = WormHead.transform.position;
            else 
            {
                GameManager.Instance.OccupiedPos[i] = WormTails[i-1].transform.position;
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
        GameManager.Instance.OccupiedPos.Add(newTailPos());
        Tail newTail = Instantiate(WormTailPrefab, newTailPos(), Quaternion.identity, GameManager.Instance.Worm);
        WormTails.Add(newTail);
        newTail.name = $"WormTail ({WormTails.Count - 1})";
        if ((WormTails.Count == 0))
            newTail.SetParent(this);
        else
            newTail.SetParent(WormTails[WormTails.Count - 2]);

        WormTails[WormTails.Count - 2].SetTailChild(newTail);
        EnableFX();

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

    private void EnableFX() 
    {
        FertEffect.gameObject.SetActive(true);
        FertEffect.transform.position = WormTails[WormTails.Count - 1].transform.position;
        Invoke("DisableFX", 5f);
    }
    private void DisableFX() 
    {
        FertEffect.gameObject.SetActive(false);
    }
}
