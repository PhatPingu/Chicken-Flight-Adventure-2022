using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public int score;

    [SerializeField] private TMP_Text scoreFieldText;

    void Update()
    {
        scoreFieldText.text =  score.ToString();
    }




}
