using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	#region Singleton

	public static BoardManager Instance{ set; get; }

	void Awake () {
		Instance = this;
	}

	#endregion

	public ChessPiece[,] ChessPieces{ get; set; }
	[HideInInspector]
	public List<ChessPiece> allWhitePieces;
	[HideInInspector]
	public List<ChessPiece> allBlackPieces;

	public List<GameObject> chessPiecePrefabs;

	public static bool isWhiteTurn = true;
	public bool isBlackChecked;
	public bool isWhiteChecked;

	public int[] EnPassant{ get; set; }

	List<GameObject> activeChessPieces;

	ChessPiece selectedChesspiece;

	bool[,] AllowedMoves{ set; get; }

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

		bool hasAtLeastOneMove = false;
		AllowedMoves = ChessPieces [x, y].PossibleMove ();
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				if (AllowedMoves [i, j]) {
					hasAtLeastOneMove = true;
					break;
				}
			}
		}

		if (!hasAtLeastOneMove)
			return;

		selectedChesspiece = ChessPieces [x, y];
		BoardHighlight.Instance.HighlightAllowedMoves (AllowedMoves);
	}

	void CapturePiece (ChessPiece c) {
		activeChessPieces.Remove (c.gameObject);
		if (c.isWhite) {
			allWhitePieces.Remove (c);
		} else {
			allBlackPieces.Remove (c);
		}
		Destroy (c.gameObject);
	}

	void MoveChessPiece (int x, int y) {
		if (AllowedMoves [x, y]) {
			Vector2 lastPosition = new Vector2 (selectedChesspiece.CurrentX, selectedChesspiece.CurrentY);
			ChessPiece previousPiece = ChessPieces [x, y];
			bool noCheck = false;
			MoveInGame (x, y);
			if (selectedChesspiece.isWhite) {
				CalculateWhiteCheck ();

				if (isWhiteChecked) {
					RevertInGame ((int)lastPosition.x, (int)lastPosition.y, x, y, previousPiece);
				} else {
					noCheck = true;
					RevertInGame ((int)lastPosition.x, (int)lastPosition.y, x, y, previousPiece);
				}
			} else {
				CalculateBlackCheck ();

				if (isBlackChecked) {
					RevertInGame ((int)lastPosition.x, (int)lastPosition.y, x, y, previousPiece);
				} else {
					noCheck = true;
					RevertInGame ((int)lastPosition.x, (int)lastPosition.y, x, y, previousPiece);
				}
			}

			if (noCheck) {
				if (previousPiece != null && previousPiece.isWhite != isWhiteTurn) {
					CapturePiece (previousPiece);
				}
				if (selectedChesspiece.GetType () == typeof(Pawn)) {
					if (y == 7) {
						// White Promotion
						CapturePiece (selectedChesspiece);
						SpawnChessPieces (1, x, y);
						selectedChesspiece = ChessPieces [x, y];
					} else if (y == 0) {
						// Black Promotion
						CapturePiece (selectedChesspiece);
						SpawnChessPieces (7, x, y);
						selectedChesspiece = ChessPieces [x, y];
					}

					if (x == EnPassant [0] && y == EnPassant [1]) {
						if (y == 5 && isWhiteTurn) {
							previousPiece = ChessPieces [x, y - 1];
						} else if (y == 2 && !isWhiteTurn) {
							previousPiece = ChessPieces [x, y + 1];
						}

						CapturePiece (previousPiece);
					}
				}

				EnPassant [0] = -1;
				EnPassant [1] = -1;

				if (selectedChesspiece.GetType () == typeof(Pawn)) {
					if (selectedChesspiece.CurrentY == 1 && y == 3) {
						//Black gets Enpassant
						EnPassant [0] = x;
						EnPassant [1] = y - 1;
					} else if (selectedChesspiece.CurrentY == 6 && y == 4) {
						//Black Gets EnPassant
						EnPassant [0] = x;
						EnPassant [1] = y + 1;
					}
				}

				MoveInGame (x, y);
				MovePhysical (x, y);
			}
		}

		BoardHighlight.Instance.HideHighlights ();
		selectedChesspiece = null;
	}

	void MoveInGame (int x, int y) {
		ChessPieces [selectedChesspiece.CurrentX, selectedChesspiece.CurrentY] = null;
		selectedChesspiece.SetPosition (x, y);
		ChessPieces [x, y] = selectedChesspiece;
	}

	void MovePhysical (int x, int y) {
		selectedChesspiece.transform.position = GetTileCentre (x, y);
		isWhiteTurn = !isWhiteTurn;
	}

	void RevertInGame (int lastX, int lastY, int x, int y, ChessPiece previousPiece) {
		ChessPieces [lastX, lastY] = selectedChesspiece;
		selectedChesspiece.SetPosition (lastX, lastY);
		ChessPieces [x, y] = previousPiece;
	}

	void CalculateBlackCheck () {
		isBlackChecked = false;
		foreach (ChessPiece c in allWhitePieces) {
			c.PossibleMove ();
			if (c.checkKing) {
				isBlackChecked = true;
				break;
			}
		}
	}

	void CalculateWhiteCheck () {
		isWhiteChecked = false;
		foreach (ChessPiece c in allBlackPieces) {
			c.PossibleMove ();
			if (c.checkKing) {
				isWhiteChecked = true;
				break;
			}
		}
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
		if (ChessPieces [x, y].isWhite) {
			allWhitePieces.Add (ChessPieces [x, y]);
		} else {
			allBlackPieces.Add (ChessPieces [x, y]);
		}
	}

	void SpawnAllChessPieces () {
		activeChessPieces = new List<GameObject> ();
		ChessPieces = new ChessPiece[8, 8];
		allWhitePieces = new List<ChessPiece> ();
		allBlackPieces = new List<ChessPiece> ();
		EnPassant = new int[2]{ -1, -1 };

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
