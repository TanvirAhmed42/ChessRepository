using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessPiece {

	public override bool[,] PossibleMove ()
	{
		bool[,] possibleMoves = new bool[8, 8];
		ChessPiece c = null;
		int i, j;

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
				if (c.isWhite != isWhite)
					possibleMoves [i, CurrentY] = true;

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
				if (c.isWhite != isWhite)
					possibleMoves [i, CurrentY] = true;

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
				if (c.isWhite != isWhite)
					possibleMoves [CurrentX, i] = true;

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
				if (c.isWhite != isWhite)
					possibleMoves [CurrentX, i] = true;

				break;
			}
		}

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
				if (c.isWhite != isWhite)
					possibleMoves [i, j] = true;

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
				if (c.isWhite != isWhite)
					possibleMoves [i, j] = true;

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
				if (c.isWhite != isWhite)
					possibleMoves [i, j] = true;

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
				if (c.isWhite != isWhite)
					possibleMoves [i, j] = true;

				break;
			}
		}

		return possibleMoves;
	}
}
