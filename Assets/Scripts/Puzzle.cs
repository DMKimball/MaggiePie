using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour {
	void Start() {
		var solution = transform.Find("Solution");
		for (var i = 0; i < solution.transform.childCount; ++i) {
			var solutionPiece = solution.transform.GetChild(i);
			solutionPiece.SendMessage("SetSolution");
		}
	}

	public PuzzlePiece PickRandomPiece() {
		var pieces = transform.Find("Pieces");
		if (pieces.childCount == 0)
			return null;
		var puzzlePiece = pieces.GetChild(Random.Range(0, pieces.childCount - 1));
		return puzzlePiece.GetComponent<PuzzlePiece>();
	}
}
