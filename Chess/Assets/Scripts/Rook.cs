using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPiece {

	public override bool[,] PossibleMove ()
	{
		bool[,] possibleMoves = new bool[8, 8];

		ChessPiece c = null;
		int i;
		checkKing = false;

		//Right
		i = CurrentX;
		while (true) {
			i++;
			if (i >= 8)
				break;

			c = BoardManager.Instance.ChessPieces [i, CurrentY];
			if (c == null)
				possibleMoves [i, CurrentY] = true;
			else {
				if (c.isWhite != isWhite) {
					possibleMoves [i, CurrentY] = true;
					if (c.GetType () == typeof(King)) {
						checkKing = true;
					}
				}

				break;
			}
		}

		//Left
		i = CurrentX;
		while (true) {
			i--;
			if (i < 0)
				break;

			c = BoardManager.Instance.ChessPieces [i, CurrentY];
			if (c == null)
				possibleMoves [i, CurrentY] = true;
			else {
				if (c.isWhite != isWhite) {
					possibleMoves [i, CurrentY] = true;
					if (c.GetType () == typeof(King)) {
						checkKing = true;
					}
				}

				break;
			}
		}

		//Up
		i = CurrentY;
		while (true) {
			i++;
			if (i >= 8)
				break;

			c = BoardManager.Instance.ChessPieces [CurrentX, i];
			if (c == null)
				possibleMoves [CurrentX, i] = true;
			else {
				if (c.isWhite != isWhite) {
					possibleMoves [CurrentX, i] = true;
					if (c.GetType () == typeof(King)) {
						checkKing = true;
					}
				}

				break;
			}
		}

		//Down
		i = CurrentY;
		while (true) {
			i--;
			if (i < 0)
				break;

			c = BoardManager.Instance.ChessPieces [CurrentX, i];
			if (c == null)
				possibleMoves [CurrentX, i] = true;
			else {
				if (c.isWhite != isWhite) {
					possibleMoves [CurrentX, i] = true;
					if (c.GetType () == typeof(King)) {
						checkKing = true;
					}
				}

				break;
			}
		}

		return possibleMoves;
	}
}
