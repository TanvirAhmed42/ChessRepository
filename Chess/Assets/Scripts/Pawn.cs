using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece {

	public override bool[,] PossibleMove ()
	{
		bool[,] possibleMoves = new bool[8, 8];
		ChessPiece c1 = null;
		ChessPiece c2 = null;

		//White

		if (isWhite) {
			//Diagonal Left
			if (CurrentX != 0 && CurrentY != 7) {
				c1 = BoardManager.Instance.ChessPieces [CurrentX - 1, CurrentY + 1];
				if (c1 != null && !c1.isWhite)
					possibleMoves [CurrentX - 1, CurrentY + 1] = true;
			}

			//Diagonal Right
			if (CurrentX != 7 && CurrentY != 7) {
				c1 = BoardManager.Instance.ChessPieces [CurrentX + 1, CurrentY + 1];
				if (c1 != null && !c1.isWhite)
					possibleMoves [CurrentX + 1, CurrentY + 1] = true;
			}

			//One Move Forward
			if (CurrentY != 7) {
				c1 = BoardManager.Instance.ChessPieces [CurrentX, CurrentY + 1];
				if (c1 == null)
					possibleMoves [CurrentX, CurrentY + 1] = true;
			}

			//Two Move Forward At First Row
			if (CurrentY == 1) {
				c1 = BoardManager.Instance.ChessPieces [CurrentX, CurrentY + 1];
				c2 = BoardManager.Instance.ChessPieces [CurrentX, CurrentY + 2];
				if (c1 == null && c2 == null)
					possibleMoves [CurrentX, CurrentY + 2] = true;
			}
		}

		//Black

		else {
			//Diagonal Left
			if (CurrentX != 7 && CurrentY != 0) {
				c1 = BoardManager.Instance.ChessPieces [CurrentX + 1, CurrentY - 1];
				if (c1 != null && c1.isWhite)
					possibleMoves [CurrentX + 1, CurrentY - 1] = true;
			}

			//Diagonal Right
			if (CurrentX != 0 && CurrentY != 0) {
				c1 = BoardManager.Instance.ChessPieces [CurrentX - 1, CurrentY - 1];
				if (c1 != null && c1.isWhite)
					possibleMoves [CurrentX - 1, CurrentY - 1] = true;
			}

			//One Move Forward
			if (CurrentY != 0) {
				c1 = BoardManager.Instance.ChessPieces [CurrentX, CurrentY - 1];
				if (c1 == null)
					possibleMoves [CurrentX, CurrentY - 1] = true;
			}

			//Two Move Forward At First Row
			if (CurrentY == 6) {
				c1 = BoardManager.Instance.ChessPieces [CurrentX, CurrentY - 1];
				c2 = BoardManager.Instance.ChessPieces [CurrentX, CurrentY - 2];
				if (c1 == null && c2 == null)
					possibleMoves [CurrentX, CurrentY - 2] = true;
			}
		}

		return possibleMoves;
	}
}
