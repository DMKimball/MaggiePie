﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBird : MonoBehaviour
{
	public float _speed = 5;
	private Vector2 _direction;

	public void Start() {
		var puzzle = GameObject.FindObjectOfType<Puzzle>();
		var puzzlePiece = puzzle.PickRandomPiece();
		if (puzzlePiece) {
			var position = puzzlePiece.transform.position;
			transform.position = position + new Vector3(-2*position.x, -position.y);
			_direction = (position - transform.position).normalized;
		}
		var respawn = GetComponent<RespawnScript>();
		if (respawn)
			respawn.SetRespawnPoint(transform);
	}

	void Update() {
		var rigidbody = GetComponent<Rigidbody2D>();
		rigidbody.velocity = _direction * _speed;
	}

	public void OnGrabObject(Grabbable grabbedObject) {
		_speed *= .5f;
		_direction.y = -_direction.y;
	}

	public void OnReleaseObject(Grabbable releasedObject) {
		_speed *= 2.5f;
	}
}
