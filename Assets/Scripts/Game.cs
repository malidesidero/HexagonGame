using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject hexMap;
    public Text Score;
    public GameObject ScoreText;
    public GameObject GameOver;
    public Text GameOverScore;

    void Update()
    {
        Score.text=hexMap.GetComponent<HexGrid>().scorePoint.ToString();
    }
    public void gameOver() 
    {
        string score = hexMap.GetComponent<HexGrid>().scorePoint.ToString();
        GameOver.SetActive(true);
        hexMap.SetActive(false);
        ScoreText.SetActive(false);
        GameOverScore.text = "score= "+score;
    }
    public void restartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
