using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessPiece {

	public override void PossibleMove ()
	{
		moves.Clear ();
		ChessPiece c;
		int i, j;

		//Right
		i = CurrentX;
		while (true) {
			i++;
			if (i >= 8)
				break;

			c = BoardManager.Instance.ChessPieces [i, CurrentY];
			if (c == null) {
				Move move = new Move (CurrentX, CurrentY, i, CurrentY, isWhite);
				if (move.isLegalMove) {
					moves.Add (move);
				}
			} else {
				if (c.isWhite != isWhite) {
					Move move = new Move (CurrentX, CurrentY, i, CurrentY, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
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
			if (c == null) {
				Move move = new Move (CurrentX, CurrentY, i, CurrentY, isWhite);
				if (move.isLegalMove) {
					moves.Add (move);
				}
			} else {
				if (c.isWhite != isWhite) {
					Move move = new Move (CurrentX, CurrentY, i, CurrentY, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
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
			if (c == null) {
				Move move = new Move (CurrentX, CurrentY, CurrentX, i, isWhite);
				if (move.isLegalMove) {
					moves.Add (move);
				}
			} else {
				if (c.isWhite != isWhite) {
					Move move = new Move (CurrentX, CurrentY, CurrentX, i, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
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
			if (c == null) {
				Move move = new Move (CurrentX, CurrentY, CurrentX, i, isWhite);
				if (move.isLegalMove) {
					moves.Add (move);
				}
			} else {
				if (c.isWhite != isWhite) {
					Move move = new Move (CurrentX, CurrentY, CurrentX, i, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
					}
				}

				break;
			}
		}

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
		checkKing = false;
		ChessPiece c;
		int i, j;

		//Right
		i = CurrentX;
		while (true) {
			i++;
			if (i >= 8) {
				break;
			}

			c = BoardManager.Instance.ChessPieces [i, CurrentY];
			if (c != null && (c.GetType () != typeof(King) || c.isWhite == isWhite)) {
				break;
			}
			if (c != null && c.GetType () == typeof(King) && c.isWhite != isWhite) {
				checkKing = true;
				return;
			}
		}

		//Left
		i = CurrentX;
		while (true) {
			i--;
			if (i < 0) {
				break;
			}

			c = BoardManager.Instance.ChessPieces [i, CurrentY];
			if (c != null && (c.GetType () != typeof(King) || c.isWhite == isWhite)) {
				break;
			}
			if (c != null && c.GetType () == typeof(King) && c.isWhite != isWhite) {
				checkKing = true;
				return;
			}
		}

		//Up
		i = CurrentY;
		while (true) {
			i++;
			if (i >= 8) {
				break;
			}

			c = BoardManager.Instance.ChessPieces [CurrentX, i];
			if (c != null && (c.GetType () != typeof(King) || c.isWhite == isWhite)) {
				break;
			}
			if (c != null && c.GetType () == typeof(King) && c.isWhite != isWhite) {
				checkKing = true;
				return;
			}
		}

		//Down
		i = CurrentY;
		while (true) {
			i--;
			if (i < 0) {
				break;
			}

			c = BoardManager.Instance.ChessPieces [CurrentX, i];
			if (c != null && (c.GetType () != typeof(King) || c.isWhite == isWhite)) {
				break;
			}
			if (c != null && c.GetType () == typeof(King) && c.isWhite != isWhite) {
				checkKing = true;
				return;
			}
		}

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
