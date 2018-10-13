using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiece {

	public override bool[,] PossibleMove ()
	{
		bool[,] possibleMoves = new bool[8, 8];
		ChessPiece c = null;
		int i, j;
		checkKing = false;

		//Top Right
		i = CurrentX;
		j = CurrentY;
		while (true) {
			i++;
			j++;
			if (i >= 8 || j >= 8)
				break;

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

				break;
			}
		}

		//Top Left
		i = CurrentX;
		j = CurrentY;
		while (true) {
			i--;
			j++;
			if (i < 0 || j >= 8)
				break;

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

				break;
			}
		}

		//Bottom Right
		i = CurrentX;
		j = CurrentY;
		while (true) {
			i++;
			j--;
			if (i >= 8 || j < 0)
				break;

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

				break;
			}
		}

		//Bottom Left
		i = CurrentX;
		j = CurrentY;
		while (true) {
			i--;
			j--;
			if (i < 0 || j < 0)
				break;

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

				break;
			}
		}


		return possibleMoves;
	}
}
