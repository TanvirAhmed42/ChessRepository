using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPiece : MonoBehaviour {

	public int CurrentX{ get; set; }
	public int CurrentY{ get; set; }
	public bool isWhite;
	public bool checkKing;
	public int pieceValue;
	public List<Move> moves;

	public void SetPosition (int x, int y) {
		CurrentX = x;
		CurrentY = y;
	}

	public virtual void PossibleMove () {
		return;
	}

	public bool[,] MovesListToBoolArray (List<Move> moves) {
		bool[,] possibleMoves = new bool[8, 8];
		foreach (Move move in moves) {
			int targetX = move.targetX;
			int targetY = move.targetY;

			possibleMoves [targetX, targetY] = true;
		}

		return possibleMoves;
	}

	public void AddMove (Move move) {
		moves.Add (move);
	}

	public virtual void CanCheckKing () {
		return;
	}
}
