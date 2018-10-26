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
	//[HideInInspector]
	public List<ChessPiece> allWhitePieces;
	//[HideInInspector]
	public List<ChessPiece> allBlackPieces;
	//[HideInInspector]
	public List<Move> allPossibleWhiteMoves = new List<Move> ();
	//[HideInInspector]
	public List<Move> allPossibleBlackMoves = new List<Move> ();

	public List<GameObject> chessPiecePrefabs;


	public bool isWhiteTurn = true;
	public bool twoPlayer = true;
	public bool isBlackChecked;
	public bool isWhiteChecked;

	public bool whiteKingHasMoved;
	public bool blackKingHasMoved;
	public int[] EnPassant{ get; set; }

	List<GameObject> activeChessPieces;

	ChessPiece selectedChesspiece;

	bool[,] AllowedMoves{ set; get; }
	bool aiMoving;

	const float tile_Size = 1f;
	const float tile_Offset = .5f;

	int selectionX = -1;
	int selectionY = -1;

	void Start () {
		SpawnAllChessPieces ();
		CalculateAllWhiteMoves ();
	}

	void Update () {
		UpdateSelection ();
		DrawChessBoard ();

		if (!twoPlayer) {
			if (isWhiteTurn) {
				aiMoving = false;
				if (Input.GetMouseButtonDown (0)) {
					if (selectionX >= 0 && selectionY >= 0) {
						if (selectedChesspiece == null) {
							SelectChessPiece (selectionX, selectionY);
						} else {
							MoveChessPiece (selectionX, selectionY);
						}
					}
				}
			} else {
				if (!aiMoving) {
					MoveChessPieceByAI ();
				}
			}
		} else {
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
	}

	void SelectChessPiece (int x, int y) {
		if (ChessPieces [x, y] == null) {
			return;
		}

		if (ChessPieces [x, y].isWhite != isWhiteTurn) {
			return;
		}

		selectedChesspiece = ChessPieces [x, y];
		AllowedMoves = selectedChesspiece.MovesListToBoolArray (selectedChesspiece.moves);

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

	public void AddPiece (ChessPiece c, int x, int y) {
		activeChessPieces.Add (c.gameObject);
		if (c.isWhite) {
			allWhitePieces.Add (c);
		} else {
			allBlackPieces.Add (c);
		}
		c.SetPosition (x, y);
	}

	public void CaptureSimulation (ChessPiece c) {
		activeChessPieces.Remove (c.gameObject);
		if (c.isWhite) {
			allWhitePieces.Remove (c);
		} else {
			allBlackPieces.Remove (c);
		}
		ChessPieces [c.CurrentX, c.CurrentY] = null;
	}

	void MoveChessPieceByAI () {
		aiMoving = true;
		Move[] bestMoves = ChessAI.Instance.CalculateBlackBestMove ();
		if (bestMoves.Length == 0) {
			if (CalculateBlackCheck ()) {
				Debug.Log ("You Win");
			} else {
				Debug.Log ("Match Draw");
			}
			return;
		}
		int randomBestMove;
		if (bestMoves.Length == 1) {
			randomBestMove = 0;
		} else {
			randomBestMove = Random.Range (0, bestMoves.Length);
		}
		bestMoves [randomBestMove].PrintMove ();
		selectedChesspiece = bestMoves [randomBestMove].pieceToMove;
		AllowedMoves = selectedChesspiece.MovesListToBoolArray (selectedChesspiece.moves);
		MoveChessPiece (bestMoves [randomBestMove].targetX, bestMoves [randomBestMove].targetY);
	}

	void MoveChessPiece (int x, int y) {
		if (AllowedMoves [x, y] || (!isWhiteTurn && !twoPlayer)) {
			ChessPiece previousPiece = ChessPieces [x, y];
			if (previousPiece != null && previousPiece.isWhite != isWhiteTurn) {
				CapturePiece (previousPiece);
				Debug.Log ("After Capturing");
			}
			if (selectedChesspiece.GetType () == typeof(Pawn)) {
				if (y == 7) {
					// White Promotion
					CapturePiece (selectedChesspiece);
					SpawnChessPieces (1, x, y);
					selectedChesspiece = ChessPieces [x, y];
					selectedChesspiece.PossibleMove ();
				} else if (y == 0) {
					// Black Promotion
					CapturePiece (selectedChesspiece);
					SpawnChessPieces (7, x, y);
					selectedChesspiece = ChessPieces [x, y];
					selectedChesspiece.PossibleMove ();
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

			Debug.Log ("Before Moving In Game");
			MoveInGame (x, y, selectedChesspiece);
			Debug.Log ("After In Game Before Physical");
			MovePhysical (x, y);

			Debug.Log ("Moved ChessPiece Physically");

			if (selectedChesspiece.GetType () == typeof(King)) {
				if (!isWhiteTurn) {
					if (x == 2 && !whiteKingHasMoved) {
						ChessPiece rookPiece = ChessPieces [0, 0];
						MoveInGame (3, 0, rookPiece);
						rookPiece.transform.position = GetTileCentre (3, 0);
					} else if (x == 6 && !whiteKingHasMoved) {
						ChessPiece rookPiece = ChessPieces [7, 0];
						MoveInGame (5, 0, rookPiece);
						rookPiece.transform.position = GetTileCentre (5, 0);
					}

					whiteKingHasMoved = true;
				} else {
					if (x == 2 && !blackKingHasMoved) {
						ChessPiece rookPiece = ChessPieces [0, 7];
						MoveInGame (3, 7, rookPiece);
						rookPiece.transform.position = GetTileCentre (3, 7);
					} else if (x == 6 && !blackKingHasMoved) {
						ChessPiece rookPiece = ChessPieces [7, 7];
						MoveInGame (5, 7, rookPiece);
						rookPiece.transform.position = GetTileCentre (5, 7);
					}

					blackKingHasMoved = true;
				}
			}

			if (isWhiteTurn) {
				CalculateAllWhiteMoves ();
			} else {
				CalculateAllBlackMoves ();
			}

			if (isWhiteTurn) {
				isBlackChecked = false;
			} else {
				isWhiteChecked = false;
			}

			if (allPossibleWhiteMoves.Count == 0) {
				if (CalculateWhiteCheck ()) {
					Debug.Log ("Black Won");
				} else {
					Debug.Log ("Match Draw");
				}
			} else if (allPossibleBlackMoves.Count == 0) {
				if (CalculateBlackCheck ()) {
					Debug.Log ("White Won");
				} else {
					Debug.Log ("Match Draw");
				}
			}
		}

		BoardHighlight.Instance.HideHighlights ();
		selectedChesspiece = null;
	}

	public void MoveInGame (int x, int y, ChessPiece pieceToMove) {
		ChessPieces [pieceToMove.CurrentX, pieceToMove.CurrentY] = null;
		pieceToMove.SetPosition (x, y);
		ChessPieces [x, y] = pieceToMove;
	}

	public void MovePhysical (int x, int y) {
		selectedChesspiece.transform.position = GetTileCentre (x, y);
		isWhiteTurn = !isWhiteTurn;
	}

	public void RevertInGame (int lastX, int lastY, int x, int y, ChessPiece previousPiece, ChessPiece pieceToRevert) {
		ChessPieces [lastX, lastY] = pieceToRevert;
		pieceToRevert.SetPosition (lastX, lastY);
		ChessPieces [x, y] = previousPiece;
	}

	public bool CalculateBlackCheck () {
		isBlackChecked = false;
		foreach (ChessPiece c in allWhitePieces) {
			if (c != null) {
				c.CanCheckKing ();
				if (c.checkKing) {
					isBlackChecked = true;
					//Debug.Log (c);
					break;
				}
			}
		}

		return isBlackChecked;
	}

	public bool CalculateWhiteCheck () {
		isWhiteChecked = false;
		foreach (ChessPiece c in allBlackPieces) {
			if (c != null) {
				c.CanCheckKing ();
				if (c.checkKing) {
					isWhiteChecked = true;
					break;
				}
			}
		}

		return isWhiteChecked;
	}

	public void CalculateAllWhiteMoves () {
		allPossibleWhiteMoves.Clear ();
		foreach (ChessPiece whitePiece in allWhitePieces) {
			if (whitePiece != null) {
				whitePiece.PossibleMove ();
				allPossibleWhiteMoves.AddRange (whitePiece.moves);
			}
		}
	}

	public Move[] CalculateNextWhiteMoves () {
		List<Move> whiteMoves = new List<Move> ();
		for (int i = allWhitePieces.Count - 1; i >= 0; i--) {
			if (allWhitePieces [i] != null) {
				allWhitePieces [i].PossibleMove ();
				//Move[] whitePieceMoves = allWhitePieces [i].CopyMoveList ();
				whiteMoves.AddRange (allWhitePieces[i].moves);
			}
		}

		return whiteMoves.ToArray ();
	}

	public Move[] CalculateNextBlackMoves () {
		List<Move> blackMoves = new List<Move> ();
		for (int i = allBlackPieces.Count - 1; i >= 0; i--) {
			if (allBlackPieces [i] != null) {
				allBlackPieces [i].PossibleMove ();
				//Move[] blackPieceMoves = allBlackPieces [i].CopyMoveList ();
				blackMoves.AddRange (allBlackPieces[i].moves);
			}
		}

		return blackMoves.ToArray ();
	}

	public void CalculateAllBlackMoves () {
		allPossibleBlackMoves.Clear ();
		foreach (ChessPiece blackPiece in allBlackPieces) {
			if (blackPiece != null) {
				blackPiece.PossibleMove ();
				allPossibleBlackMoves.AddRange (blackPiece.moves);
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
		//Debug.Log (ChessPieces [x, y] + " Before setting to queen");
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
