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

    public List<Vector3> WormPos = new List<Vector3>();
    public int Level = 0;
    public float GameSpeed;
    public bool IsMoving;
    public bool IsGameover;
    public Vector3 NextFoodPosition;
    public Transform Map;
    // Start is called before the first frame update

    public Action OnFoodRepos;
    public Action OnGameOver;

    void Start()
    {
        NextFoodPosition = new Vector3(UnityEngine.Random.Range(MapBoarder[2], MapBoarder[3]), UnityEngine.Random.Range(MapBoarder[1], MapBoarder[0]), 0);
        SpawnFood();
        OnFoodRepos = SetPosFood;
        OnGameOver += SetGameOver;
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
            Food = Instantiate(FoodPrefab, NextFoodPosition, Quaternion.identity);
            SetFoodPos();
        }
    }

    public void SetPosFood()
    {
        Food.transform.position = NextFoodPosition;
        SetFoodPos();

    }

    private void SetFoodPos()
    {
        NextFoodPosition = new Vector3(UnityEngine.Random.Range(MapBoarder[2], MapBoarder[3]), UnityEngine.Random.Range(MapBoarder[1], MapBoarder[0]), 0);

        for (int i = 0; i < WormPos.Count; i++)
        {

            if (NextFoodPosition == WormPos[i])
            {
                NextFoodPosition = new Vector3(UnityEngine.Random.Range(MapBoarder[2], MapBoarder[3]), UnityEngine.Random.Range(MapBoarder[1], MapBoarder[0]), 0);

            }
        }
    }

    public void SetGameOver()
    {
        IsGameover = true;
    }
}
