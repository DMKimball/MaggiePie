using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBird : MonoBehaviour
{
	public float _speed = 5;
	private Vector2 _direction;

	void Start() {
		var puzzle = GameObject.FindObjectOfType<Puzzle>();
		var puzzlePiece = puzzle.PickRandomPiece();
		if (puzzlePiece) {
			var position = puzzlePiece.transform.position;
			transform.position = position + new Vector3(-2*position.x, -position.y);
			_direction = (position - transform.position).normalized;
		}
	}

	void Update() {
		var rigidbody = GetComponent<Rigidbody2D>();
		rigidbody.velocity = _direction * _speed;
	}
}
