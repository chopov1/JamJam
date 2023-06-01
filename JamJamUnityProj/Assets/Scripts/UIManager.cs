using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    private Player playerReference;
    [SerializeField] TextMeshProUGUI soulsText;
    [SerializeField] TextMeshProUGUI timeText;
    private int minutesCount;
    private int secondsCount;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerReference = FindObjectOfType<Player>();
        minutesCount = 0;
        secondsCount = 0;
    }
    void Update()
    {
        soulsText.text = $"Souls Reaped: {playerReference.soulsReaped}";
        secondsCount = (int)Mathf.Round(gameManager.waveTime % 60f);
        minutesCount = 0;
        if(gameManager.waveTime >= 60f)
        {
            minutesCount = (int)Mathf.Round(gameManager.waveTime / 60f);
        }

        timeText.text = $"Time: {minutesCount}:{secondsCount}";
    }
}
