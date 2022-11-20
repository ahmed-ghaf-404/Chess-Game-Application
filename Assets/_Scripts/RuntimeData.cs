using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "RuntimeData")]
public class RuntimeData : ScriptableObject{
    public Player CurrentPlayer;
    public Player PreviousPlayer;
    public string FEN;
    public bool isCheck;
    public Move[] moves;
    public int moveNum;
    public int halfMoveNum;
    public bool isGameOver;

    public Player White;
    public Player Black;
}
