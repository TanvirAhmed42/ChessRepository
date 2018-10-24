using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece {

	public override void PossibleMove ()
	{
		moves.Clear ();
		ChessPiece c;
		int i, j;

		ChessPiece c1, c2, c3, c4, c5, c6, c7;
		//White Castling
		if (isWhite) {
			if (!BoardManager.Instance.whiteKingHasMoved && !BoardManager.Instance.CalculateWhiteCheck ()) {
				c1 = BoardManager.Instance.ChessPieces [0, 0];
				c2 = BoardManager.Instance.ChessPieces [1, 0];
				c3 = BoardManager.Instance.ChessPieces [2, 0];
				c4 = BoardManager.Instance.ChessPieces [3, 0];
				c5 = BoardManager.Instance.ChessPieces [5, 0];
				c6 = BoardManager.Instance.ChessPieces [6, 0];
				c7 = BoardManager.Instance.ChessPieces [7, 0];

				//Left Side
				if (c1 != null && c1.GetType () == typeof(Rook) && c1.isWhite) {
					if (c2 == null && c3 == null && c4 == null) {
						bool check = false;
						ChessPiece kingPiece = BoardManager.Instance.ChessPieces [4, 0];
						BoardManager.Instance.MoveInGame (2, 0, kingPiece);
						if (BoardManager.Instance.CalculateWhiteCheck ()) {
							check = true;
						}
						BoardManager.Instance.RevertInGame (4, 0, 2, 0, c3, kingPiece);

						BoardManager.Instance.MoveInGame (3, 0, kingPiece);
						if (BoardManager.Instance.CalculateWhiteCheck ()) {
							check = true;
						}
						BoardManager.Instance.RevertInGame (4, 0, 3, 0, c4, kingPiece);

						if (!check) {
							Move move = new Move (CurrentX, CurrentY, CurrentX - 2, CurrentY, isWhite);
							moves.Add (move);
						}

					}
				}

				//Right Side
				if (c7 != null && c7.GetType () == typeof(Rook) && c7.isWhite) {
					if (c5 == null && c6 == null) {
						bool check = false;
						ChessPiece kingPiece = BoardManager.Instance.ChessPieces [4, 0];
						BoardManager.Instance.MoveInGame (5, 0, kingPiece);
						if (BoardManager.Instance.CalculateWhiteCheck ()) {
							check = true;
						}
						BoardManager.Instance.RevertInGame (4, 0, 5, 0, c5, kingPiece);

						BoardManager.Instance.MoveInGame (6, 0, kingPiece);
						if (BoardManager.Instance.CalculateWhiteCheck ()) {
							check = true;
						}
						BoardManager.Instance.RevertInGame (4, 0, 6, 0, c6, kingPiece);

						if (!check) {
							Move move = new Move (CurrentX, CurrentY, CurrentX + 2, CurrentY, isWhite);
							moves.Add (move);
						}
					}
				}
			}
		} else {
			if (!BoardManager.Instance.blackKingHasMoved && !BoardManager.Instance.CalculateBlackCheck ()) {
				c1 = BoardManager.Instance.ChessPieces [0, 7];
				c2 = BoardManager.Instance.ChessPieces [1, 7];
				c3 = BoardManager.Instance.ChessPieces [2, 7];
				c4 = BoardManager.Instance.ChessPieces [3, 7];
				c5 = BoardManager.Instance.ChessPieces [5, 7];
				c6 = BoardManager.Instance.ChessPieces [6, 7];
				c7 = BoardManager.Instance.ChessPieces [7, 7];

				//Left Side
				if (c1 != null && c1.GetType () == typeof(Rook) && !c1.isWhite) {
					if (c2 == null && c3 == null && c4 == null) {
						bool check = false;
						ChessPiece kingPiece = BoardManager.Instance.ChessPieces [4, 7];
						BoardManager.Instance.MoveInGame (2, 7, kingPiece);
						if (BoardManager.Instance.CalculateBlackCheck ()) {
							check = true;
						}
						BoardManager.Instance.RevertInGame (4, 7, 2, 7, c3, kingPiece);

						BoardManager.Instance.MoveInGame (3, 7, kingPiece);
						if (BoardManager.Instance.CalculateBlackCheck ()) {
							check = true;
						}
						BoardManager.Instance.RevertInGame (4, 7, 3, 7, c4, kingPiece);

						if (!check) {
							Move move = new Move (CurrentX, CurrentY, CurrentX - 2, CurrentY, isWhite);
							moves.Add (move);
						}
					}
				}

				//Right Side
				if (c7 != null && c7.GetType () == typeof(Rook) && !c7.isWhite) {
					if (c5 == null && c6 == null) {
						bool check = false;
						ChessPiece kingPiece = BoardManager.Instance.ChessPieces [4, 7];
						BoardManager.Instance.MoveInGame (5, 7, kingPiece);
						if (BoardManager.Instance.CalculateBlackCheck ()) {
							check = true;
						}
						BoardManager.Instance.RevertInGame (4, 7, 5, 7, c5, kingPiece);

						BoardManager.Instance.MoveInGame (6, 7, kingPiece);
						if (BoardManager.Instance.CalculateBlackCheck ()) {
							check = true;
						}
						BoardManager.Instance.RevertInGame (4, 7, 6, 7, c6, kingPiece);

						if (!check) {
							Move move = new Move (CurrentX, CurrentY, CurrentX + 2, CurrentY, isWhite);
							moves.Add (move);
						}
					}
				}
			}
		}

		//Top Side
		i = CurrentX - 1;
		j = CurrentY + 1;
		if (CurrentY != 7) {
			for (int k = 0; k < 3; k++) {
				if (i >= 0 && i < 8) {
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
					}
				}

				i++;
			}
		}

		//Middle Left
		if (CurrentX != 0) {
			c = BoardManager.Instance.ChessPieces [CurrentX - 1, CurrentY];
			if (c == null) {
				Move move = new Move (CurrentX, CurrentY, CurrentX - 1, CurrentY, isWhite);
				if (move.isLegalMove) {
					moves.Add (move);
				}
			} else {
				if (c.isWhite != isWhite) {
					Move move = new Move (CurrentX, CurrentY, CurrentX - 1, CurrentY, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
					}
				}
			}
		}

		//Middle Right
		if (CurrentX != 7) {
			c = BoardManager.Instance.ChessPieces [CurrentX + 1, CurrentY];
			if (c == null) {
				Move move = new Move (CurrentX, CurrentY, CurrentX + 1, CurrentY, isWhite);
				if (move.isLegalMove) {
					moves.Add (move);
				}
			} else {
				if (c.isWhite != isWhite) {
					Move move = new Move (CurrentX, CurrentY, CurrentX + 1, CurrentY, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
					}
				}
			}
		}
	}

	public override void CanCheckKing ()
	{
		checkKing = false;
		ChessPiece c;
		int i, j;

		//Top Side
		i = CurrentX - 1;
		j = CurrentY + 1;
		if (CurrentY != 7) {
			for (int k = 0; k < 3; k++) {
				if (i >= 0 && i < 8) {
					c = BoardManager.Instance.ChessPieces [i, j];
					//Debug.Log (c + " " + i + " " + j);
					if (c != null && c.GetType () == typeof(King) && c.isWhite != isWhite) {
						checkKing = true;
						return;
					}
				}

				i++;
			}
		}

		//Bottom Side
		i = CurrentX - 1;
		j = CurrentY - 1;
		if (CurrentY != 0) {
			for (int k = 0; k < 3; k++) {
				if (i >= 0 && i < 8) {
					c = BoardManager.Instance.ChessPieces [i, j];
					//Debug.Log (c + " " + i + " " + j);
					if (c != null && c.GetType () == typeof(King) && c.isWhite != isWhite) {
						checkKing = true;
						return;
					}
				}

				i++;
			}
		}

		//Middle Left
		if (CurrentX != 0) {
			c = BoardManager.Instance.ChessPieces [CurrentX - 1, CurrentY];
			//Debug.Log (c + " " + (CurrentX - 1).ToString () + " " + CurrentY);
			if (c != null && c.GetType () == typeof(King) && c.isWhite != isWhite) {
				checkKing = true;
				return;
			}
		}

		//Middle Right
		if (CurrentX != 7) {
			c = BoardManager.Instance.ChessPieces [CurrentX + 1, CurrentY];
			//Debug.Log (c + " " + (CurrentX + 1).ToString () + " " + CurrentY);
			if (c != null && c.GetType () == typeof(King) && c.isWhite != isWhite) {
				checkKing = true;
				return;
			}
		}
	}
}
