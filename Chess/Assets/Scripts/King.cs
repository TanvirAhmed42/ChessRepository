using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece {

	public override bool[,] PossibleMove ()
	{
		bool[,] possibleMoves = new bool[8, 8];
		ChessPiece c = null;
		int i, j;

		//Top Side
		i = CurrentX - 1;
		j = CurrentY + 1;
		if (CurrentY != 7) {
			for (int k = 0; k < 3; k++) {
				if (i >= 0 && i < 8) {
					c = BoardManager.Instance.ChessPieces [i, j];
					if (c == null)
						possibleMoves [i, j] = true;
					else {
						if (c.isWhite != isWhite) {
							possibleMoves [i, j] = true;
							if (c.GetType () == typeof(King)) {
								checkKing = true;
							}
						}
					}
				}

				i++;
			}
		}

		//Down Side
		i = CurrentX - 1;
		j = CurrentY - 1;
		if (CurrentY != 0) {
			for (int k = 0; k < 3; k++) {
				if (i >= 0 && i < 8) {
					c = BoardManager.Instance.ChessPieces [i, j];
					if (c == null)
						possibleMoves [i, j] = true;
					else {
						if (c.isWhite != isWhite) {
							possibleMoves [i, j] = true;
							if (c.GetType () == typeof(King)) {
								checkKing = true;
							}
						}
					}
				}

				i++;
			}
		}

		//Middle Left
		if (CurrentX != 0) {
			c = BoardManager.Instance.ChessPieces [CurrentX - 1, CurrentY];
			if (c == null)
				possibleMoves [CurrentX - 1, CurrentY] = true;
			else {
				if (c.isWhite != isWhite) {
					possibleMoves [CurrentX - 1, CurrentY] = true;
					if (c.GetType () == typeof(King)) {
						checkKing = true;
					}
				}
			}
		}

		//Middle Right
		if (CurrentX != 7) {
			c = BoardManager.Instance.ChessPieces [CurrentX + 1, CurrentY];
			if (c == null)
				possibleMoves [CurrentX + 1, CurrentY] = true;
			else {
				if (c.isWhite != isWhite) {
					possibleMoves [CurrentX + 1, CurrentY] = true;
					if (c.GetType () == typeof(King)) {
						checkKing = true;
					}
				}
			}
		}


		return possibleMoves;
	}
}
