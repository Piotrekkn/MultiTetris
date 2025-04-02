using UnityEngine;
using MultiTetris.Core;
public class Player
{
    public string playerName;
    public int score, x, y;
    public GameObject[,] blockArray;
    public GameObject[] frameArray = new GameObject[4];
    public GridControler gridControler;
    public bool canMove = false;
    public bool gameOver = false;
    public Player(string playerName, int x, int y)
    {
        score = 0;
        this.x = x;
        this.y = y;
        this.playerName = playerName;
        blockArray = new GameObject[x, y];
        frameArray = new GameObject[4];
        gridControler = new GridControler(x, y);
    }

}
