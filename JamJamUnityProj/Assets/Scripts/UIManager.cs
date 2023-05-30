using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    private Player playerReference;
    [SerializeField] TextMeshProUGUI soulsText;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerReference = FindObjectOfType<Player>();
    }
    void Update()
    {
        soulsText.text = $"Souls Reaped: {playerReference.soulsReaped}";
    }
}
