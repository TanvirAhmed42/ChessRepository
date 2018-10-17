using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessAI : MonoBehaviour {

	BoardManager boardManager;
	Move[] allPossibleWhiteMoves;

	List<int> whiteCount = new List<int>();
	List<int> blackCount = new List<int>();

	void Start () {
		boardManager = BoardManager.Instance;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)  && boardManager.isWhiteTurn) {
			allPossibleWhiteMoves = boardManager.allPossibleWhiteMoves.ToArray ();
			CalculateWhiteBestMove (allPossibleWhiteMoves);
		}
	}

	void CalculateWhiteBestMove (Move[] allPossibleWhiteMoves) {
		bool whiteKingMovement = boardManager.whiteKingHasMoved;
		ChessPiece rightWhiteRook = boardManager.ChessPieces [7, 0];
		ChessPiece leftWhiteRook = boardManager.ChessPieces [0, 0];
		whiteCount.Clear ();
		blackCount.Clear ();
		foreach (Move move in allPossibleWhiteMoves) {
			ChessPiece pieceToMove = move.pieceToMove;
			ChessPiece targetPiece = move.targetPiece;
			if (pieceToMove.GetType () == typeof(King) && pieceToMove.isWhite) {
				boardManager.whiteKingHasMoved = true;
				if (move.targetX == move.currentX + 2) {
					boardManager.MoveInGame (5, 0, rightWhiteRook);
				} else if (move.targetX == move.currentX - 2) {
					boardManager.MoveInGame (3, 0, leftWhiteRook);
				}
			}
			boardManager.MoveInGame (move.targetX, move.targetY, pieceToMove);
			boardManager.CalculateAllWhiteMoves ();
			boardManager.CalculateAllBlackMoves ();
			int maxWhiteMoveCount = boardManager.allPossibleWhiteMoves.Count;
			int minBlackMoveCount = boardManager.allPossibleBlackMoves.Count;
			whiteCount.Add (maxWhiteMoveCount);
			blackCount.Add (minBlackMoveCount);
			boardManager.RevertInGame (move.currentX, move.currentY, move.targetX, move.targetY, targetPiece, pieceToMove);
			if (pieceToMove.GetType () == typeof(King)) {
				boardManager.whiteKingHasMoved = whiteKingMovement;
				if (move.targetX == move.currentX + 2) {
					boardManager.RevertInGame (7, 0, 5, 0, null, rightWhiteRook);
				} else if (move.targetX == move.currentX - 2) {
					boardManager.RevertInGame (0, 0, 3, 0, null, leftWhiteRook);
				}
			}
			boardManager.CalculateAllWhiteMoves ();
			boardManager.CalculateAllBlackMoves ();
		}

		FindMoveWithHighestCount (allPossibleWhiteMoves, whiteCount);
		FindMoveWithLowestCount (allPossibleWhiteMoves, blackCount);
	}

	void FindMoveWithHighestCount (Move[] moves, List<int> count) {
		Debug.Log ("Max White Moves");
		int maxCount = int.MinValue;
		for (int i = 0; i < count.Count; i++) {
			if (count [i] > maxCount) {
				maxCount = count [i];
			}
		}

		for (int i = 0; i < count.Count; i++) {
			if (count [i] == maxCount) {
				Debug.Log (moves[i].pieceToMove);
				Debug.Log (moves[i].currentX + " " + moves[i].currentY + " Current Position");
				Debug.Log (moves[i].targetX + " " + moves[i].targetY + " Target Position");
				Debug.Log (count[i] + " White Moves");
			}
		}
	}

	void FindMoveWithLowestCount(Move[] moves, List<int> count) {
		Debug.Log ("Min Black Moves");
		int minCount = int.MaxValue;
		for (int i = 0; i < count.Count; i++) {
			if (count [i] < minCount) {
				minCount = count [i];
			}
		}

		for (int i = 0; i < count.Count; i++) {
			if (count [i] == minCount) {
				Debug.Log (moves[i].pieceToMove);
				Debug.Log (moves[i].currentX + " " + moves[i].currentY + " Current Position");
				Debug.Log (moves[i].targetX + " " + moves[i].targetY + " Target Position");
				Debug.Log (count[i] + " Black Moves");
			}
		}
	}
}
