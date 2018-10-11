using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece {

	public override bool[,] PossibleMove ()
	{
		bool[,] possibleMoves = new bool[8, 8];

		//Right Up
		KnightMove(CurrentX + 2, CurrentY + 1, ref possibleMoves);

		//Right Down
		KnightMove(CurrentX + 2, CurrentY - 1, ref possibleMoves);

		//Left Up
		KnightMove(CurrentX - 2, CurrentY + 1, ref possibleMoves);

		//Left Down
		KnightMove(CurrentX - 2, CurrentY - 1, ref possibleMoves);

		//Top Right
		KnightMove(CurrentX + 1, CurrentY + 2, ref possibleMoves);

		//Top Left
		KnightMove(CurrentX - 1, CurrentY + 2, ref possibleMoves);

		//Bottom Right
		KnightMove(CurrentX + 1, CurrentY - 2, ref possibleMoves);

		//Bottom Left
		KnightMove(CurrentX - 1, CurrentY - 2, ref possibleMoves);

		return possibleMoves;
	}

	public void KnightMove (int x, int y, ref bool[,] possibleMoves) {
		ChessPiece c = null;

		if (x >= 0 && x < 8 && y >= 0 && y < 8) {
			c = BoardManager.Instance.ChessPieces [x, y];
			if (c == null)
				possibleMoves [x, y] = true;
			else {
				if (c.isWhite != isWhite)
					possibleMoves [x, y] = true;
			}
		}
	}
}
