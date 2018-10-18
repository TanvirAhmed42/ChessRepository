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
		foreach (Move move in allPossibleBlackMoves) {
			ChessPiece pieceToMove = move.pieceToMove;
			ChessPiece targetPiece = move.targetPiece;
			if (targetPiece != null) {
				boardManager.CaptureSimulation (targetPiece);
			}
			boardManager.MoveInGame (move.targetX, move.targetY, pieceToMove);
			int currentState = CalculateGameState (boardManager.allWhitePieces.ToArray (), boardManager.allBlackPieces.ToArray ());
			Evaluation e = new Evaluation (move, currentState);
			evaluations.Add (e);
			if (currentState < minState) {
				minState = currentState;
			}
			boardManager.RevertInGame (move.currentX, move.currentY, move.targetX, move.targetY, targetPiece, pieceToMove);
			if (targetPiece != null) {
				boardManager.AddPiece (targetPiece, move.targetX, move.targetY);
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