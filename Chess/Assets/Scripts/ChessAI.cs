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

	BoardManager boardManager;

	void Start () {
		boardManager = BoardManager.Instance;
	}

	public Move[] CalculateBlackBestMove (Move[] allPossibleBlackMoves) {
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
			Evaluation e = new Evaluation (blackMove, maxState);
			evaluations.Add (e);
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

public struct Evaluation {
	public Move moveToGetEvaluation;
	public int evaluatedValue;

	public Evaluation (Move _moveToGetEvaluation, int _evaluatedValue) {
		moveToGetEvaluation = _moveToGetEvaluation;
		evaluatedValue = _evaluatedValue;
	}
}