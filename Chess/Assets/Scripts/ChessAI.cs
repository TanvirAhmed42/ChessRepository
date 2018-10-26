using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessAI : MonoBehaviour {

	#region Singleton

	public static ChessAI Instance{ set; get; }

	void Awake () {
		Instance = this;
	}

	#endregion

	public int numberOfMovesToLookAhead = 4;

	BoardManager boardManager;

	public List<Evaluation> evaluations = new List<Evaluation> ();

	void Start () {
		boardManager = BoardManager.Instance;
	}

	int MiniMax (Move[] allPossibleWhiteMoves, Move[] allPossibleBlackMoves, int depth, bool maximizingPlayer) {
		int currentState;
		if (maximizingPlayer) {
			if (boardManager.CalculateWhiteCheck () && allPossibleWhiteMoves.Length == 0) {
				currentState = int.MinValue;
				return currentState;
			} else if (allPossibleWhiteMoves.Length == 0) {
				currentState = 0;
				return currentState;
			}
		} else {
			if (boardManager.CalculateBlackCheck () && allPossibleBlackMoves.Length == 0) {
				currentState = int.MaxValue;
				return currentState;
			} else if (allPossibleBlackMoves.Length == 0) {
				currentState = 0;
				return currentState;
			}
		}
		if (depth == 0) {
			currentState = CalculateGameState (boardManager.allWhitePieces.ToArray (), boardManager.allBlackPieces.ToArray ());
			return currentState;
		}
		if (maximizingPlayer) {
			bool kingMovement = boardManager.whiteKingHasMoved;
			ChessPiece leftWhiteRook = boardManager.ChessPieces [0, 0];
			ChessPiece rightWhiteRook = boardManager.ChessPieces [7, 0];
			int maxState = int.MinValue;
			for (int i = allPossibleWhiteMoves.Length - 1; i >= 0; i--) {
				ChessPiece pieceToMove = allPossibleWhiteMoves [i].pieceToMove;
				ChessPiece targetPiece = allPossibleWhiteMoves [i].targetPiece;
				if (targetPiece != null) {
					boardManager.CaptureSimulation (targetPiece);
				}
				if (pieceToMove.GetType () == typeof(King)) {
					boardManager.whiteKingHasMoved = true;
					if (allPossibleWhiteMoves [i].targetX == allPossibleWhiteMoves [i].currentX - 2) {
						boardManager.MoveInGame (3, 0, leftWhiteRook);
					} else if (allPossibleWhiteMoves [i].targetX == allPossibleWhiteMoves [i].currentX + 2) {
						boardManager.MoveInGame (5, 0, rightWhiteRook);
					}
				}
				boardManager.MoveInGame (allPossibleWhiteMoves [i].targetX, allPossibleWhiteMoves [i].targetY, pieceToMove);
				currentState = MiniMax (null, boardManager.CalculateNextBlackMoves (), depth - 1, false);
				Evaluation evaluation = new Evaluation (allPossibleWhiteMoves [i], currentState);
				if (depth == numberOfMovesToLookAhead) {
					evaluations.Add (evaluation);
				}
				if (currentState > maxState) {
					maxState = currentState;
				}
				boardManager.RevertInGame (allPossibleWhiteMoves [i].currentX, allPossibleWhiteMoves [i].currentY, allPossibleWhiteMoves [i].targetX, allPossibleWhiteMoves [i].targetY, targetPiece, pieceToMove);
				if (pieceToMove.GetType () == typeof(King)) {
					boardManager.whiteKingHasMoved = kingMovement;
					if (allPossibleWhiteMoves [i].targetX == allPossibleWhiteMoves [i].currentX - 2) {
						boardManager.RevertInGame (0, 0, 3, 0, null, leftWhiteRook);
					} else if (allPossibleWhiteMoves [i].targetX == allPossibleWhiteMoves [i].currentX + 2) {
						boardManager.RevertInGame (7, 0, 5, 0, null, rightWhiteRook);
					}
				}
				if (targetPiece != null) {
					boardManager.AddPiece (targetPiece, allPossibleWhiteMoves [i].targetX, allPossibleWhiteMoves [i].targetY);
				}
			}
			return maxState;
		} else {
			bool kingMovement = boardManager.blackKingHasMoved;
			ChessPiece leftBlackRook = boardManager.ChessPieces [0, 7];
			ChessPiece rightBlackRook = boardManager.ChessPieces [7, 7];
			int minState = int.MaxValue;
			for (int i = allPossibleBlackMoves.Length - 1; i >= 0; i--) {
				ChessPiece pieceToMove = allPossibleBlackMoves [i].pieceToMove;
				ChessPiece targetPiece = allPossibleBlackMoves [i].targetPiece;
				if (targetPiece != null) {
					boardManager.CaptureSimulation (targetPiece);
				}
				if (pieceToMove.GetType () == typeof(King)) {
					boardManager.blackKingHasMoved = true;
					if (allPossibleBlackMoves [i].targetX == allPossibleBlackMoves [i].currentX - 2) {
						boardManager.MoveInGame (3, 7, leftBlackRook);
					} else if (allPossibleBlackMoves [i].targetX == allPossibleBlackMoves [i].currentX + 2) {
						boardManager.MoveInGame (5, 7, rightBlackRook);
					}
				}
				boardManager.MoveInGame (allPossibleBlackMoves [i].targetX, allPossibleBlackMoves [i].targetY, pieceToMove);
				currentState = MiniMax (boardManager.CalculateNextWhiteMoves (), null, depth - 1, true);
				Evaluation evaluation = new Evaluation (allPossibleBlackMoves [i], currentState);
				if (depth == numberOfMovesToLookAhead) {
					evaluations.Add (evaluation);
				}
				if (currentState < minState) {
					minState = currentState;
				}
				boardManager.RevertInGame (allPossibleBlackMoves [i].currentX, allPossibleBlackMoves [i].currentY, allPossibleBlackMoves [i].targetX, allPossibleBlackMoves [i].targetY, targetPiece, pieceToMove);
				if (pieceToMove.GetType () == typeof(King)) {
					boardManager.blackKingHasMoved = kingMovement;
					if (allPossibleBlackMoves [i].targetX == allPossibleBlackMoves [i].currentX - 2) {
						boardManager.RevertInGame (0, 7, 3, 7, null, leftBlackRook);
					} else if (allPossibleBlackMoves [i].targetX == allPossibleBlackMoves [i].currentX + 2) {
						boardManager.RevertInGame (7, 7, 5, 7, null, rightBlackRook);
					}
				}
				if (targetPiece != null) {
					boardManager.AddPiece (targetPiece, allPossibleBlackMoves [i].targetX, allPossibleBlackMoves [i].targetY);
				}
			}
			return minState;
		}
	}

	/*public Move[] CalculateBlackBestMove (Move[] allPossibleBlackMoves) {
		List<Evaluation> evaluations = new List<Evaluation> ();

		int minState = int.MaxValue;
		foreach (Move blackMove in allPossibleBlackMoves) {
			ChessPiece blackPieceToMove = blackMove.pieceToMove;
			ChessPiece blackTargetPiece = blackMove.targetPiece;
			if (blackTargetPiece != null) {
				boardManager.CaptureSimulation (blackTargetPiece);
			}
			boardManager.MoveInGame (blackMove.targetX, blackMove.targetY, blackPieceToMove);
			boardManager.CalculateAllWhiteMoves ();
			int maxState = int.MinValue;
			foreach (Move whiteMove in boardManager.allPossibleWhiteMoves) {
				ChessPiece whitePieceToMove = whiteMove.pieceToMove;
				ChessPiece whiteTargetPiece = whiteMove.targetPiece;
				if (whiteTargetPiece != null) {
					boardManager.CaptureSimulation (whiteTargetPiece);
				}
				boardManager.MoveInGame (whiteMove.targetX, whiteMove.targetY, whitePieceToMove);
				int currentState = CalculateGameState (boardManager.allWhitePieces.ToArray (), boardManager.allBlackPieces.ToArray ());
				if (currentState > maxState) {
					maxState = currentState;
				}
				boardManager.RevertInGame (whiteMove.currentX, whiteMove.currentY, whiteMove.targetX, whiteMove.targetY, whiteTargetPiece, whitePieceToMove);
				if (whiteTargetPiece != null) {
					boardManager.AddPiece (whiteTargetPiece, whiteMove.targetX, whiteMove.targetY);
				}
			}
			Evaluation evaluation = new Evaluation (blackMove, maxState);
			evaluations.Add (evaluation);
			if (maxState < minState) {
				minState = maxState;
			}
			boardManager.RevertInGame (blackMove.currentX, blackMove.currentY, blackMove.targetX, blackMove.targetY, blackTargetPiece, blackPieceToMove);
			if (blackTargetPiece != null) {
				boardManager.AddPiece (blackTargetPiece, blackMove.targetX, blackMove.targetY);
			}
		}

		List<Move> bestMoves = new List<Move> ();

		foreach (Evaluation evaluation in evaluations) {
			if (evaluation.evaluatedValue == minState) {
				bestMoves.Add (evaluation.moveToGetEvaluation);
			}
		}

		return bestMoves.ToArray ();
	}*/

	public Move[] CalculateBlackBestMove () {
		evaluations.Clear ();
		int minState = MiniMax (null, boardManager.CalculateNextBlackMoves (), numberOfMovesToLookAhead, false);

		List<Move> bestMoves = new List<Move> ();
		foreach (Evaluation evaluation in evaluations) {
			if (evaluation.evaluatedValue == minState) {
				bestMoves.Add (evaluation.moveToGetEvaluation);
			}
		}

		return bestMoves.ToArray ();
	}

	int CalculateGameState (ChessPiece[] allWhitePieces, ChessPiece[] allBlackPieces) {
		int whiteState = 0;
		int blackState = 0;

		foreach (ChessPiece whitePiece in allWhitePieces) {
			whiteState += whitePiece.pieceValue;
		}

		foreach (ChessPiece blackPiece in allBlackPieces) {
			blackState += blackPiece.pieceValue;
		}

		int state = whiteState - blackState;

		return state;
	}
}

[System.Serializable]
public struct Evaluation {
	public Move moveToGetEvaluation;
	public int evaluatedValue;

	public Evaluation (Move _moveToGetEvaluation, int _evaluatedValue) {
		moveToGetEvaluation = _moveToGetEvaluation;
		evaluatedValue = _evaluatedValue;
	}
}