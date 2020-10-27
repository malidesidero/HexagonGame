using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHex : MonoBehaviour
{
    TextMesh bombText;
    public int bombCount;
    private Game gameScript;

    void Start()
    {
        bombCount = 7;
        gameScript = FindObjectOfType<Game>();
        bombText = GameObject.Find("text").GetComponent<TextMesh>();   
    }

    // Update is called once per frame
    void Update()
    {
        bombText.text = bombCount.ToString();
        if (bombCount == 0)
        {
            gameScript.gameOver();
        }
    }
}
