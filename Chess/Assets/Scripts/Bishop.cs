using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiece {

	public override void PossibleMove ()
	{
		moves.Clear ();
		ChessPiece c;
		int i, j;

		//Top Right
		i = CurrentX;
		j = CurrentY;
		while (true) {
			i++;
			j++;
			if (i >= 8 || j >= 8) {
				break;
			}

			c = BoardManager.Instance.ChessPieces [i, j];
			if (c == null) {
				Move move = new Move (CurrentX, CurrentY, i, j, isWhite);
				if (move.isLegalMove) {
					moves.Add (move);
				}
			} else {
				if (c.isWhite != isWhite) {
					Move move = new Move (CurrentX, CurrentY, i, j, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
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
			if (c == null) {
				Move move = new Move (CurrentX, CurrentY, i, j, isWhite);
				if (move.isLegalMove) {
					moves.Add (move);
				}
			} else {
				if (c.isWhite != isWhite) {
					Move move = new Move (CurrentX, CurrentY, i, j, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
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
			if (c == null) {
				Move move = new Move (CurrentX, CurrentY, i, j, isWhite);
				if (move.isLegalMove) {
					moves.Add (move);
				}
			} else {
				if (c.isWhite != isWhite) {
					Move move = new Move (CurrentX, CurrentY, i, j, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
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
			if (c == null) {
				Move move = new Move (CurrentX, CurrentY, i, j, isWhite);
				if (move.isLegalMove) {
					moves.Add (move);
				}
			} else {
				if (c.isWhite != isWhite) {
					Move move = new Move (CurrentX, CurrentY, i, j, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
					}
				}

				break;
			}
		}
	}

	public override void CanCheckKing ()
	{
		ChessPiece c;
		checkKing = false;
		int i, j;

		//Top Right
		i = CurrentX;
		j = CurrentY;
		while (true) {
			i++;
			j++;
			if (i >= 8 || j >= 8) {
				break;
			}

			c = BoardManager.Instance.ChessPieces [i, j];
			if (c != null && (c.GetType () != typeof(King) || c.isWhite == isWhite)) {
				break;
			}
			if (c != null && c.GetType () == typeof(King) && c.isWhite != isWhite) {
				checkKing = true;
				return;
			}
		}

		//Top Left
		i = CurrentX;
		j = CurrentY;
		while (true) {
			i--;
			j++;
			if (i < 0 || j >= 8) {
				break;
			}

			c = BoardManager.Instance.ChessPieces [i, j];
			if (c != null && (c.GetType () != typeof(King) || c.isWhite == isWhite)) {
				break;
			}
			if (c != null && c.GetType () == typeof(King) && c.isWhite != isWhite) {
				checkKing = true;
				return;
			}
		}

		//Bottom right
		i = CurrentX;
		j = CurrentY;
		while (true) {
			i++;
			j--;
			if (i >= 8 || j < 0) {
				break;
			}

			c = BoardManager.Instance.ChessPieces [i, j];
			if (c != null && (c.GetType () != typeof(King) || c.isWhite == isWhite)) {
				break;
			}
			if (c != null && c.GetType () == typeof(King) && c.isWhite != isWhite) {
				checkKing = true;
				return;
			}
		}

		//Bottom Left
		i = CurrentX;
		j = CurrentY;
		while (true) {
			i--;
			j--;
			if (i < 0 || j < 0) {
				break;
			}

			c = BoardManager.Instance.ChessPieces [i, j];
			if (c != null && (c.GetType () != typeof(King) || c.isWhite == isWhite)) {
				break;
			}
			if (c != null && c.GetType () == typeof(King) && c.isWhite != isWhite) {
				checkKing = true;
				return;
			}
		}
	}
}
