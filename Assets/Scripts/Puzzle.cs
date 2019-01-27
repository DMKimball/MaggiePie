using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour {
	private Dictionary<int, PuzzlePiece> _enemyGrabbablePieces;

	void Start() {
		_enemyGrabbablePieces = new Dictionary<int, PuzzlePiece>();
		var solution = transform.Find("Solution");
		for (var i = 0; i < solution.transform.childCount; ++i) {
			var solutionPiece = solution.transform.GetChild(i);
			solutionPiece.SendMessage("SetSolution");
		}
	}

	void Update() {
		var pieces = GetComponentsInChildren<PuzzlePiece>();
		foreach (PuzzlePiece piece in pieces) {
			var id = piece.GetInstanceID();
			var containsKey = _enemyGrabbablePieces.ContainsKey(id);
			if (piece.IsGrabbableByEnemy()) {
				if (!containsKey) {
					_enemyGrabbablePieces.Add(id, piece);
				}
			} else if (containsKey) {
				_enemyGrabbablePieces.Remove(id);
			}
		}
	}

	public PuzzlePiece PickRandomPiece() {
		if (_enemyGrabbablePieces.Count == 0)
			return null;

		var enumerator = _enemyGrabbablePieces.GetEnumerator();
		var n = Random.Range(1, _enemyGrabbablePieces.Count);
		for (int i = 0; i < n; ++i)
			enumerator.MoveNext();
		return enumerator.Current.Value;
	}
}
