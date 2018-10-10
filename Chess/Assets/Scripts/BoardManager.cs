using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	public ChessPiece[,] ChessPieces{ get; set; }

	public List<GameObject> chessPiecePrefabs;

	public bool isWhiteTurn = true;

	List<GameObject> activeChessPieces;

	ChessPiece selectedChesspiece;

	const float tile_Size = 1f;
	const float tile_Offset = .5f;

	int selectionX = -1;
	int selectionY = -1;

	void Start () {
		SpawnAllChessPieces ();
	}

	void Update () {
		UpdateSelection ();
		DrawChessBoard ();

		if (Input.GetMouseButtonDown (0)) {
			if (selectionX >= 0 && selectionY >= 0) {
				if (selectedChesspiece == null) {
					SelectChessPiece (selectionX, selectionY);
				} else {
					MoveChessPiece (selectionX, selectionY);
				}
			}
		}
	}

	void SelectChessPiece (int x, int y) {
		if (ChessPieces [x, y] == null) {
			return;
		}

		if (ChessPieces [x, y].isWhite != isWhiteTurn) {
			return;
		}

		selectedChesspiece = ChessPieces [x, y];
	}

	void MoveChessPiece (int x, int y) {
		if (selectedChesspiece.PossibleMove (x, y)) {
			ChessPieces [selectedChesspiece.CurrentX, selectedChesspiece.CurrentY] = null;
			selectedChesspiece.transform.position = GetTileCentre (x, y);
			ChessPieces [x, y] = selectedChesspiece;
			isWhiteTurn = !isWhiteTurn;
		}

		selectedChesspiece = null;
	}

	void UpdateSelection () {
		if (!Camera.main)
			return;

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray, out hit, 25f, LayerMask.GetMask ("ChessPlane"))) {
			selectionX = (int)hit.point.x;
			selectionY = (int)hit.point.z;
		} else {
			selectionX = -1;
			selectionY = -1;
		}

		
	}

	void SpawnChessPieces (int index, int x, int y) {
		GameObject chessPieceToSpawn = Instantiate (chessPiecePrefabs [index], GetTileCentre (x, y), Quaternion.Euler (Vector3.right * 90)) as GameObject;
		chessPieceToSpawn.transform.SetParent (transform);
		ChessPieces [x, y] = chessPieceToSpawn.GetComponent<ChessPiece> ();
		ChessPieces [x, y].SetPosition (x, y);
		activeChessPieces.Add (chessPieceToSpawn);
	}

	void SpawnAllChessPieces () {
		activeChessPieces = new List<GameObject> ();
		ChessPieces = new ChessPiece[8, 8];

		//White

		//Rooks
		SpawnChessPieces(2, 0, 0);
		SpawnChessPieces (2, 7, 0);

		//Knights
		SpawnChessPieces(4, 1, 0);
		SpawnChessPieces (4, 6, 0);

		//Bishops
		SpawnChessPieces(3, 2, 0);
		SpawnChessPieces (3, 5, 0);

		//Queen
		SpawnChessPieces(1, 3, 0);

		//King
		SpawnChessPieces(0, 4, 0);

		//Pawns
		for (int i = 0; i < 8; i++) {
			SpawnChessPieces (5, i, 1);
		}

		//Black

		//Rooks
		SpawnChessPieces(8, 0, 7);
		SpawnChessPieces (8, 7, 7);

		//Knights
		SpawnChessPieces(10, 1, 7);
		SpawnChessPieces (10, 6, 7);

		//Bishops
		SpawnChessPieces(9, 2, 7);
		SpawnChessPieces (9, 5, 7);

		//Queen
		SpawnChessPieces(7, 3, 7);

		//King
		SpawnChessPieces(6, 4, 7);

		//Pawns
		for (int i = 0; i < 8; i++) {
			SpawnChessPieces (11, i, 6);
		}
	}

	Vector3 GetTileCentre (int x, int y) {
		Vector3 origin = Vector3.zero;
		origin.x += (tile_Size * x) + tile_Offset;
		origin.z += (tile_Size * y) + tile_Offset;

		return origin;
	}

	void DrawChessBoard () {
		Vector3 widthLine = Vector3.right * 8;
		Vector3 heightLine = Vector3.forward * 8;

		for (int i = 0; i <= 8; i++) {
			Vector3 start = Vector3.forward * i;
			Debug.DrawLine (start, start + widthLine);
			for (int j = 0; j <= 8; j++) {
				start = Vector3.right * j;
				Debug.DrawLine (start, start + heightLine);
			}
		}

		if (selectionX >= 0 && selectionY >= 0) {
			Debug.DrawLine (Vector3.forward * selectionY + Vector3.right * selectionX, Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));
			Debug.DrawLine (Vector3.forward * selectionY + Vector3.right * (selectionX + 1), Vector3.forward * (selectionY + 1) + Vector3.right * selectionX);
		}
	}
}
