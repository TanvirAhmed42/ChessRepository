using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlight : MonoBehaviour {

	#region Singleton

	public static BoardHighlight Instance{ set; get; }

	void Awake () {
		Instance = this;
	}

	#endregion

	public GameObject highlightPrefab;

	List<GameObject> highlights = new List<GameObject> ();

	GameObject GetHighlightedObject () {
		GameObject go = highlights.Find (g => !g.activeSelf);

		if (go == null) {
			go = Instantiate (highlightPrefab);
			highlights.Add (go);
		}

		return go;
	}

	public void HighlightAllowedMoves (bool[,] moves) {
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				if (moves [i, j]) {
					GameObject go = GetHighlightedObject ();
					go.SetActive (true);
					go.transform.position = new Vector3 (i + .5f, 0f, j + .5f);
				}
			}
		}
	}

	public void HideHighlights () {
		foreach (GameObject go in highlights) {
			go.SetActive (false);
		}
	}
}
