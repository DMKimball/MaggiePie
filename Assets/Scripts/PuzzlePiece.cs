﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour {
	public bool IsGrabbableByEnemy() {
		var rigidbody = GetComponent<Rigidbody2D>();
		var spriteRenderer = GetComponent<SpriteRenderer>();
		return spriteRenderer && spriteRenderer.enabled
			&& spriteRenderer.isVisible
			&& rigidbody && rigidbody.bodyType != RigidbodyType2D.Static;
	}

	void SetSolution() {
		var rigidbody = GetComponent<Rigidbody2D>();
		if (rigidbody)
			rigidbody.bodyType = RigidbodyType2D.Static;
		var collider = GetComponent<BoxCollider2D>();
		collider.isTrigger = true;
		collider.enabled = true;
		var spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.enabled = false;
		var grabbable = GetComponent<Grabbable>();
		if (grabbable)
			grabbable.enabled = false;
	}

	void SnapToSolution(Transform solution) {
		var rigidbody = GetComponent<Rigidbody2D>();
		if (rigidbody)
			rigidbody.bodyType = RigidbodyType2D.Static;
		var collider = GetComponent<BoxCollider2D>();
		collider.enabled = false;
		transform.position = solution.position;
		transform.parent = solution;

		var grabbable = GetComponent<Grabbable>();
		if (grabbable)
			grabbable.ReleaseSelf(0);

        transform.parent.parent.parent.SendMessage("OnSnapToSolution");
	}

	bool IsSolution(PuzzlePiece otherPiece) {
		if (!(transform.parent != null
			&& transform.parent.name == "Solution"
			&& transform.childCount == 0)) {
			return false;
		}

		var spriteRenderer = GetComponent<SpriteRenderer>();
		var otherSpriteRenderer = otherPiece.GetComponent<SpriteRenderer>();
		return spriteRenderer.sprite == otherSpriteRenderer.sprite;
	}

	void OnTriggerEnter2D(Collider2D other) {
		var grabbable = GetComponent<Grabbable>();
		if (grabbable && grabbable.IsGrabbedByEnemy())
			return;
		var otherPiece = other.GetComponent<PuzzlePiece>();
		if (otherPiece != null && otherPiece.IsSolution(this)) {
			SnapToSolution(other.transform);
		}
	}
}
