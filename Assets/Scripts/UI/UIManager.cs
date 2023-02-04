using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI LevelTxt;

    [SerializeField] private GameObject GameoverPanel;

    void SetLevel() 
    {
        LevelTxt.text = GameManager.Instance.Level.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameOver += ShowGameOver;
        GameoverPanel.SetActive(false);
    }

    void ShowGameOver() 
    {
        GameoverPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
