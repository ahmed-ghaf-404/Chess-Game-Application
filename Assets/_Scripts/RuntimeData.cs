using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "RuntimeData")]
public class RuntimeData : ScriptableObject{
    public Player CurrentPlayer;
    public Player PreviousPlayer;
    public Piece[,] Board;
    public string FEN;
    public bool isCheck;
    public Move[] moves;
    public int moveNum = 1;
    public int halfMoveNum = 0;
    public bool isGameOver;

    public Player White;
    public Player Black;

    public int[] highlightedFromSquare = new int[2];
    public int[] highlightedToSquare  = new int[2];
}
