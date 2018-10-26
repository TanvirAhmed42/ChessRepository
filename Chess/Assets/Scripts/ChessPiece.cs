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
	public Move[] movesArray;

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

	public Move[] CopyMoveList () {
		movesArray = new Move[moves.Count];

		for (int i = moves.Count - 1; i >= 0; i--) {
			movesArray [i] = moves [i];
		}

		return movesArray;
	}

	public virtual void CanCheckKing () {
		return;
	}
}
