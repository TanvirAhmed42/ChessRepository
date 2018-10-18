using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Move {

	public ChessPiece pieceToMove;
	public ChessPiece targetPiece;
	public int currentX;
	public int currentY;
	public int targetX;
	public int targetY;
	public bool isWhite;
	public bool isLegalMove = true;

	public Move (int _currentX, int _currentY, int _targetX, int _targetY, bool _isWhite) {
		currentX = _currentX;
		currentY = _currentY;
		targetX = _targetX;
		targetY = _targetY;
		isWhite = _isWhite;
		pieceToMove = BoardManager.Instance.ChessPieces [currentX, currentY];
		targetPiece = BoardManager.Instance.ChessPieces [targetX, targetY];

		if (targetPiece != null) {
			BoardManager.Instance.CaptureSimulation (targetPiece);
		}

		BoardManager.Instance.MoveInGame (targetX, targetY, pieceToMove);
		bool legal;
		if (isWhite) {
			legal = BoardManager.Instance.CalculateWhiteCheck ();
		} else {
			legal = BoardManager.Instance.CalculateBlackCheck ();
		}

		isLegalMove = !legal;
		BoardManager.Instance.RevertInGame (currentX, currentY, targetX, targetY, targetPiece, pieceToMove);

		if (targetPiece != null) {
			BoardManager.Instance.AddPiece (targetPiece, targetX, targetY);
		}
	}

	public void PrintMove () {
		char currectXChar = (char)(currentX + 48);
		char targetXChar = (char)(targetX + 48);

		Debug.Log (currectXChar.ToString () + currentY + " to " + targetXChar.ToString () + targetY);
	}
}
