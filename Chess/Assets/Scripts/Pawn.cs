using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece {

	public override void PossibleMove ()
	{
		moves.Clear ();
		ChessPiece c1;
		ChessPiece c2;
		BoardManager boardManager = BoardManager.Instance;
		int[] enPassant = boardManager.EnPassant;

		if (isWhite) {
			//White
			//Diagonal Left
			if (CurrentX != 0 && CurrentY != 7) {
				if (enPassant [0] == CurrentX - 1 && enPassant [1] == CurrentY + 1) {
					Move move = new Move (CurrentX, CurrentY, CurrentX - 1, CurrentY + 1, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
					}
				}

				c1 = boardManager.ChessPieces [CurrentX - 1, CurrentY + 1];
				if (c1 != null && !c1.isWhite) {
					Move move = new Move (CurrentX, CurrentY, CurrentX - 1, CurrentY + 1, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
					}
				}
			}

			//Diagonal Right
			if (CurrentX != 7 && CurrentY != 7) {
				if (enPassant [0] == CurrentX + 1 && enPassant [1] == CurrentY + 1) {
					Move move = new Move (CurrentX, CurrentY, CurrentX + 1, CurrentY + 1, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
					}
				}

				c1 = boardManager.ChessPieces [CurrentX + 1, CurrentY + 1];
				if (c1 != null && !c1.isWhite) {
					Move move = new Move (CurrentX, CurrentY, CurrentX + 1, CurrentY + 1, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
					}
				}
			}

			//One Move Forward
			if (CurrentY != 7) {
				c1 = boardManager.ChessPieces [CurrentX, CurrentY + 1];

				if (c1 == null) {
					Move move = new Move (CurrentX, CurrentY, CurrentX, CurrentY + 1, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
					}
				}
			}

			//Two Move Forward On First Move
			if (CurrentY == 1) {
				c1 = boardManager.ChessPieces [CurrentX, CurrentY + 1];
				c2 = boardManager.ChessPieces [CurrentX, CurrentY + 2];

				if (c1 == null && c2 == null) {
					Move move = new Move (CurrentX, CurrentY, CurrentX, CurrentY + 2, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
					}
				}
			}
		} else {
			//Black
			//Diagonal Left
			if (CurrentX != 7 && CurrentY != 0) {
				if (enPassant [0] == CurrentX + 1 && enPassant [1] == CurrentY - 1) {
					Move move = new Move (CurrentX, CurrentY, CurrentX + 1, CurrentY - 1, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
					}
				}

				c1 = boardManager.ChessPieces [CurrentX + 1, CurrentY - 1];
				if (c1 != null && c1.isWhite) {
					Move move = new Move (CurrentX, CurrentY, CurrentX + 1, CurrentY - 1, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
					}
				}
			}

			//Diagonal Right
			if (CurrentX != 0 && CurrentY != 0) {
				if (enPassant [0] == CurrentX - 1 && enPassant [1] == CurrentY - 1) {
					Move move = new Move (CurrentX, CurrentY, CurrentX - 1, CurrentY - 1, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
					}
				}

				c1 = boardManager.ChessPieces [CurrentX - 1, CurrentY - 1];
				if (c1 != null && c1.isWhite) {
					Move move = new Move (CurrentX, CurrentY, CurrentX - 1, CurrentY - 1, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
					}
				}
			}

			//One Move Forward
			if (CurrentY != 0) {
				c1 = boardManager.ChessPieces [CurrentX, CurrentY - 1];

				if (c1 == null) {
					Move move = new Move (CurrentX, CurrentY, CurrentX, CurrentY - 1, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
					}
				}
			}

			//Two Move Forward On First Move
			if (CurrentY == 6) {
				c1 = boardManager.ChessPieces [CurrentX, CurrentY - 1];
				c2 = boardManager.ChessPieces [CurrentX, CurrentY - 2];

				if (c1 == null && c2 == null) {
					Move move = new Move (CurrentX, CurrentY, CurrentX, CurrentY - 2, isWhite);
					if (move.isLegalMove) {
						moves.Add (move);
					}
				}
			}
		}

	}

	public override void CanCheckKing ()
	{
		ChessPiece c;
		checkKing = false;

		if (isWhite) {
			//Diagonal Left Check
			if (CurrentX != 0 && CurrentY != 7) {
				c = BoardManager.Instance.ChessPieces [CurrentX - 1, CurrentY + 1];
				if (c != null && c.GetType () == typeof(King) && !c.isWhite) {
					checkKing = true;
					return;
				}
			}
			//Diagonal Right Check
			if (CurrentX != 7 && CurrentY != 7) {
				c = BoardManager.Instance.ChessPieces [CurrentX + 1, CurrentY + 1];
				if (c != null && c.GetType () == typeof(King) && !c.isWhite) {
					checkKing = true;
					return;
				}
			}
		} else {
			//Diagonal Left Check
			if (CurrentX != 7 && CurrentY != 0) {
				c = BoardManager.Instance.ChessPieces [CurrentX + 1, CurrentY - 1];
				if (c != null && c.GetType () == typeof(King) && c.isWhite) {
					checkKing = true;
					return;
				}
			}
			//Diagonal Right Check
			if (CurrentX != 0 && CurrentY != 0) {
				c = BoardManager.Instance.ChessPieces [CurrentX - 1, CurrentY - 1];
				if (c != null && c.GetType () == typeof(King) && c.isWhite) {
					checkKing = true;
					return;
				}
			}
		}
	}
}