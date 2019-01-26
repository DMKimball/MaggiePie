using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other) {
		var isSolution = other.transform.parent.name == "Solution";
		if (isSolution && other.transform.childCount == 0) {
			var rigidbody = GetComponent<Rigidbody2D>();
			rigidbody.bodyType = RigidbodyType2D.Static;
			var collider = GetComponent<BoxCollider2D>();
			collider.enabled = false;
			transform.position = other.transform.position;
			transform.parent = other.transform;
		}
	}
}
