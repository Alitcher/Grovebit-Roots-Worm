using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : Worm
{
    [SerializeField] private Worm Parent;
    [SerializeField] private GameObject Child;


    public override void Start()
    {
        base.Start();
        InvokeRepeating("Move", 0, GameManager.Instance.GameSpeed);

    }

    private void Move()
    {
        if (GameManager.Instance.IsMoving)
        {
           //PrevPos = Parent.PrevPos;
            PrevPos =  Parent.transform.position;
            this.transform.position = PrevPos;
        }


    }

    public void SetParent(Head parent)
    {
        this.Parent = (Worm)parent;
    }

    public void SetParent(Tail parent)
    {
        this.Parent = (Worm)parent;
    }

    public void SetTailChild(Tail child)
    {
        this.Child = child.gameObject;
    }


}
