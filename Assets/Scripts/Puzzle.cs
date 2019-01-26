using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour {
	void Start() {
		foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>()) {
			var color = spriteRenderer.color;
			color.a = .25f;
			spriteRenderer.color = color;
		}
	}
}
