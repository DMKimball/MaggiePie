using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBird : MonoBehaviour
{
	public float _speed = 5;
	private Vector2 _direction;
	private bool _readyToLaunch;

	public void Start() {
		_readyToLaunch = true;
	}

	void Update() {
		if (_readyToLaunch) {
			var puzzle = GameObject.FindObjectOfType<Puzzle>();
			var puzzlePiece = puzzle.PickRandomPiece();
			if (puzzlePiece) {
				Launch(puzzlePiece);
			}
		}
		var rigidbody = GetComponent<Rigidbody2D>();
		rigidbody.velocity = _direction * _speed;

		var spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer) {
			spriteRenderer.flipX = _direction.x > 0;
		}
	}

	void Launch(PuzzlePiece puzzlePiece) {
		_readyToLaunch = false;
		var position = puzzlePiece.transform.position;
		var vpPos = Camera.main.WorldToViewportPoint(position);
		vpPos.x = vpPos.x < .5 ? 1 : 0;
		vpPos.y = 1;
		transform.position = Camera.main.ViewportToWorldPoint(vpPos);
		_direction = (position - transform.position).normalized;
		var respawn = GetComponent<RespawnScript>();
		if (respawn)
			respawn.SetRespawnPoint(transform);
	}

	public void OnGrabObject(Grabbable grabbedObject) {
		_speed *= .5f;
		_direction.y = -_direction.y;
	}

	public void OnReleaseObject(Grabbable releasedObject) {
		_speed *= 2.5f;
	}
}
