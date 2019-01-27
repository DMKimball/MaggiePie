using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBird : MonoBehaviour
{
	public float _baseSpeedMin = 6;
	public float _baseSpeedInc = 1;
	public float _baseSpeedMax = 11;
	public AudioClip[] _sounds;

	private float _speed;
	private float _baseSpeed;
	private Vector2 _direction;
	private bool _readyToLaunch;

	public void Start() {
		StartCoroutine("SoundCoroutine");
		_baseSpeed = _baseSpeedMin;
		_readyToLaunch = true;
		_speed = _baseSpeed;
	}

	IEnumerator SoundCoroutine() {
		while (true) {
			var audioSource = GetComponent<AudioSource>();
			var clip = _sounds[Random.Range(0, _sounds.Length - 1)];
			if (clip)
				audioSource.clip = clip;
			audioSource.Play();
			yield return new WaitForSeconds(1.25f);
		}
	}

	void Update() {
		var rigidbody = GetComponent<Rigidbody2D>();
		var spriteRenderer = GetComponent<SpriteRenderer>();

		var legalToLaunch = rigidbody.velocity.sqrMagnitude == 0
			|| (spriteRenderer && !spriteRenderer.isVisible);
		if (_readyToLaunch && legalToLaunch) {
			var puzzle = GameObject.FindObjectOfType<Puzzle>();
			var puzzlePiece = puzzle.PickRandomPiece();
			if (puzzlePiece) {
				Launch(puzzlePiece);
			}
		}
		rigidbody.velocity = _direction * _speed;

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
		_speed = _baseSpeed/2;
		_baseSpeed = Mathf.Max(_baseSpeedMin, _baseSpeed - 2*_baseSpeedInc);
		_direction.y = -_direction.y;
	}

	public void OnReleaseObject(float disableGrabTime) {
		if (disableGrabTime > 0) {
			_speed = _baseSpeedMax*1.5f;
			_baseSpeed = Mathf.Min(_baseSpeedMax, _baseSpeed + _baseSpeedInc);
		}
	}

	public void OnRespawn() {
		_baseSpeed = Mathf.Min(_baseSpeedMax, _baseSpeed + _baseSpeedInc);
		_readyToLaunch = true;
		_speed = _baseSpeed;
	}
}
