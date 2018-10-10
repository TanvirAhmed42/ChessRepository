using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	public List<GameObject> chessPiecePrefabs;

	List<GameObject> activeChessPieces = new List<GameObject> ();

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

	void SpawnChessPieces (int index, Vector3 position) {
		GameObject chessPieceToSpawn = Instantiate (chessPiecePrefabs [index], position, Quaternion.Euler (Vector3.right * 90)) as GameObject;
		chessPieceToSpawn.transform.SetParent (transform);
		activeChessPieces.Add (chessPieceToSpawn);
	}

	void SpawnAllChessPieces () {
		//White

		//Rooks
		SpawnChessPieces(2, GetTileCentre(0, 0));
		SpawnChessPieces (2, GetTileCentre (7, 0));

		//Knights
		SpawnChessPieces(4, GetTileCentre(1, 0));
		SpawnChessPieces (4, GetTileCentre (6, 0));

		//Bishops
		SpawnChessPieces(3, GetTileCentre(2, 0));
		SpawnChessPieces (3, GetTileCentre (5, 0));

		//Queen
		SpawnChessPieces(1, GetTileCentre(3, 0));

		//King
		SpawnChessPieces(0, GetTileCentre(4, 0));

		//Pawns
		for (int i = 0; i < 8; i++) {
			SpawnChessPieces (5, GetTileCentre (i, 1));
		}

		//Black

		//Rooks
		SpawnChessPieces(8, GetTileCentre(0, 7));
		SpawnChessPieces (8, GetTileCentre (7, 7));

		//Knights
		SpawnChessPieces(10, GetTileCentre(1, 7));
		SpawnChessPieces (10, GetTileCentre (6, 7));

		//Bishops
		SpawnChessPieces(9, GetTileCentre(2, 7));
		SpawnChessPieces (9, GetTileCentre (5, 7));

		//Queen
		SpawnChessPieces(7, GetTileCentre(3, 7));

		//King
		SpawnChessPieces(6, GetTileCentre(4, 7));

		//Pawns
		for (int i = 0; i < 8; i++) {
			SpawnChessPieces (11, GetTileCentre (i, 6));
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
