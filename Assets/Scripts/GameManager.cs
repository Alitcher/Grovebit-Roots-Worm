using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField] private int[] MapBoarder;
    [SerializeField] private GameObject FoodPrefab;
    [SerializeField] private List<GameObject> ObsPrefab;
    public int SpeedDivisor => OccupiedPos.Count;

    public List<Vector3> OccupiedPos = new List<Vector3>();
    public int Level = 0;
    public float gameSpeed;
    public float GameSpeed => gameSpeed ;
    public bool IsMoving;
    public bool IsGameover;
    public Vector3 NextFoodPosition;
    public Transform Map, Worm;
    // Start is called before the first frame update

    public Action OnFoodRepos;
    public Action OnGameOver;

    void Start()
    {
        NextFoodPosition = new Vector3(UnityEngine.Random.Range(MapBoarder[2], MapBoarder[3]), UnityEngine.Random.Range(MapBoarder[1], MapBoarder[0]), 0);
        SpawnFood();
        OnFoodRepos = SetPosFood;
        OnGameOver += SetGameOver;

        SoundManager.Instance.Play2("BGM");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Gameplay");
        }
    }

    [SerializeField] private GameObject Food;
    public void SpawnFood()
    {
        if (Food == null)
        {
            Food = Instantiate(FoodPrefab, NextFoodPosition, Quaternion.identity, Map);
            SetFoodPos();
        }
    }

    public void SetPosFood()
    {
        SoundManager.Instance.Play2("eat");

        SetFoodPos();


        Food.transform.position = NextFoodPosition;

    }

    private void SetFoodPos()
    {
        NextFoodPosition = new Vector3(UnityEngine.Random.Range(MapBoarder[2], MapBoarder[3]), UnityEngine.Random.Range(MapBoarder[1], MapBoarder[0]), 0);

        for (int i = 0; i < OccupiedPos.Count; i++)
        {

            if (NextFoodPosition != OccupiedPos[i])
            {
                continue;
            }
            NextFoodPosition = new Vector3(UnityEngine.Random.Range(MapBoarder[2], MapBoarder[3]), UnityEngine.Random.Range(MapBoarder[1], MapBoarder[0]), 0);

        }

    }

    public void SetGameOver()
    {
        IsGameover = true;
        SoundManager.Instance.Stop("BGM");
        SoundManager.Instance.Play2("over");
    }
}
