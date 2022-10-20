using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piece : MonoBehaviour{
    /*
    using binary to differentiate pieces
    */

    public const int Empty = 0;
    public const int Pawn = 1;
    public const int Horsie = 2;
    public const int Bishop = 3;
    public const int Rook = 4;
    public const int Queen = 5;
    public const int King = 6;

    // king: 0110


    public const int White = 8;
    public const int Black = 16;
    // white: 1000

    const int blackMask = 0b10000;
    const int whiteMask = 0b01000;
    const int colourMask = whiteMask | blackMask;
    const int pieceMask = 0b00111;

    int piece=Piece.Empty;
    
    [SerializeField] SpriteRenderer _renderer;
    
    public static bool IsColor(int piece, int color){
        return (piece & colourMask) == color;
    }
    void Start(){
        Debug.Log("Piece!");
    }   
}