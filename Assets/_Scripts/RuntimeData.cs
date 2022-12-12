using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "RuntimeData")]
public class RuntimeData : ScriptableObject{
    public Player CurrentPlayer;
    public Player PreviousPlayer;
    public Piece[,] Board;
    public Dictionary<Piece, List<Move>> LegalMoves = new Dictionary<Piece, List<Move>>();
    public Move CurrentMoveLegalMove;
    public string FEN;
    public Tuple<int,int> EnPassantSquare;
    public bool isCheck;
    public Move[] moves;
    public int moveNum = 1;
    public int halfMoveNum = 0;
    public bool isGameOver;

    public Player White;
    public Player Black;

    public int[] highlightedFromSquare = new int[2];
    public int[] highlightedToSquare  = new int[2];

    public enum SpecialMoves: int{
        Enpassant,
        Promote,
        Castling,
        TwoPawnPush
    }
}
