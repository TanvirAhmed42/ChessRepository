using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece {

	public override void PossibleMove ()
	{
		moves.Clear ();

		//Right Up
		if (KnightMove (CurrentX + 2, CurrentY + 1)) {
			Move move = new Move (CurrentX, CurrentY, CurrentX + 2, CurrentY + 1, isWhite);
			if (move.isLegalMove) {
				moves.Add (move);
			}
		}

		//Right Down
		if (KnightMove (CurrentX + 2, CurrentY - 1)) {
			Move move = new Move (CurrentX, CurrentY, CurrentX + 2, CurrentY - 1, isWhite);
			if (move.isLegalMove) {
				moves.Add (move);
			}
		}

		//Left Up
		if (KnightMove (CurrentX - 2, CurrentY + 1)) {
			Move move = new Move (CurrentX, CurrentY, CurrentX - 2, CurrentY + 1, isWhite);
			if (move.isLegalMove) {
				moves.Add (move);
			}
		}

		//Left Down
		if (KnightMove (CurrentX - 2, CurrentY - 1)) {
			Move move = new Move (CurrentX, CurrentY, CurrentX - 2, CurrentY - 1, isWhite);
			if (move.isLegalMove) {
				moves.Add (move);
			}
		}

		//Top Right
		if (KnightMove (CurrentX + 1, CurrentY + 2)) {
			Move move = new Move (CurrentX, CurrentY, CurrentX + 1, CurrentY + 2, isWhite);
			if (move.isLegalMove) {
				moves.Add (move);
			}
		}

		//Top Left
		if (KnightMove (CurrentX - 1, CurrentY + 2)) {
			Move move = new Move (CurrentX, CurrentY, CurrentX - 1, CurrentY + 2, isWhite);
			if (move.isLegalMove) {
				moves.Add (move);
			}
		}

		//Bottom Right
		if (KnightMove (CurrentX + 1, CurrentY - 2)) {
			Move move = new Move (CurrentX, CurrentY, CurrentX + 1, CurrentY - 2, isWhite);
			if (move.isLegalMove) {
				moves.Add (move);
			}
		}

		//Bottom Left
		if (KnightMove (CurrentX - 1, CurrentY - 2)) {
			Move move = new Move (CurrentX, CurrentY, CurrentX - 1, CurrentY - 2, isWhite);
			if (move.isLegalMove) {
				moves.Add (move);
			}
		}
	}

	public bool KnightMove (int x, int y) {
		ChessPiece c;

		if (x >= 0 && x < 8 && y >= 0 && y < 8) {
			c = BoardManager.Instance.ChessPieces [x, y];
			if (c == null) {
				return true;
			} else {
				if (c.isWhite != isWhite) {
					return true;
				}
			}
		}

		return false;
	}

	public override void CanCheckKing ()
	{
		checkKing = false;

		//Right Up
		if (KnightMove (CurrentX + 2, CurrentY + 1)) {
			ChessPiece c;
			c = BoardManager.Instance.ChessPieces [CurrentX + 2, CurrentY + 1];
			if (c != null && c.GetType () == typeof(King) && c.isWhite != isWhite) {
				checkKing = true;
				return;
			}
		}

		//Right Down
		if (KnightMove (CurrentX + 2, CurrentY - 1)) {
			ChessPiece c;
			c = BoardManager.Instance.ChessPieces [CurrentX + 2, CurrentY - 1];
			if (c != null && c.GetType () == typeof(King) && c.isWhite != isWhite) {
				checkKing = true;
				return;
			}
		}

		//Left Up
		if (KnightMove (CurrentX - 2, CurrentY + 1)) {
			ChessPiece c;
			c = BoardManager.Instance.ChessPieces [CurrentX - 2, CurrentY + 1];
			if (c != null && c.GetType () == typeof(King) && c.isWhite != isWhite) {
				checkKing = true;
				return;
			}
		}

		//Left Down
		if (KnightMove (CurrentX - 2, CurrentY - 1)) {
			ChessPiece c;
			c = BoardManager.Instance.ChessPieces [CurrentX - 2, CurrentY - 1];
			if (c != null && c.GetType () == typeof(King) && c.isWhite != isWhite) {
				checkKing = true;
				return;
			}
		}

		//Top Right
		if (KnightMove (CurrentX + 1, CurrentY + 2)) {
			ChessPiece c;
			c = BoardManager.Instance.ChessPieces [CurrentX + 1, CurrentY + 2];
			if (c != null && c.GetType () == typeof(King) && c.isWhite != isWhite) {
				checkKing = true;
				return;
			}
		}

		//Top Left
		if (KnightMove (CurrentX - 1, CurrentY + 2)) {
			ChessPiece c;
			c = BoardManager.Instance.ChessPieces [CurrentX - 1, CurrentY + 2];
			if (c != null && c.GetType () == typeof(King) && c.isWhite != isWhite) {
				checkKing = true;
				return;
			}
		}

		//Bottom Right
		if (KnightMove (CurrentX + 1, CurrentY - 2)) {
			ChessPiece c;
			c = BoardManager.Instance.ChessPieces [CurrentX + 1, CurrentY - 2];
			if (c != null && c.GetType () == typeof(King) && c.isWhite != isWhite) {
				checkKing = true;
				return;
			}
		}

		//Bottom Left
		if (KnightMove (CurrentX - 1, CurrentY - 2)) {
			ChessPiece c;
			c = BoardManager.Instance.ChessPieces [CurrentX - 1, CurrentY - 2];
			if (c != null && c.GetType () == typeof(King) && c.isWhite != isWhite) {
				checkKing = true;
				return;
			}
		}
	}
}
