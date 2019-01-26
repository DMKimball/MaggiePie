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
}
