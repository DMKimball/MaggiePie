﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puzzle : MonoBehaviour {
	private Dictionary<int, PuzzlePiece> _enemyGrabbablePieces;
    private int _numPieces = 0;
    private int _numPiecesSolved = 0;

    [SerializeField] private string _nextScene;
    [SerializeField] private float _fadeInTime = 1.0f;
    [SerializeField] private float _nextSceneDelayTime = 10.0f;
    [SerializeField] private GameObject _victoryUI = null;

    void Start() {
        _numPiecesSolved = 0;
		_enemyGrabbablePieces = new Dictionary<int, PuzzlePiece>();
		var solution = transform.Find("Solution");
        _numPieces = solution.transform.childCount;
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

    public void OnSnapToSolution()
    {
        _numPiecesSolved++;
        if (_numPiecesSolved == _numPieces)
        {
            StartCoroutine(LevelComplete());
        }
    }

    public IEnumerator LevelComplete()
    {
        GameObject player = GameObject.Find("MagpiePC");
        if (player)
        {
            player.SendMessage("PlayVictoryJingle");
        }

        if (_victoryUI)
        {
            _victoryUI.SetActive(true);
        }
        yield return null;

        MenuScript menuController = _victoryUI.GetComponent<MenuScript>();

        if (menuController)
        {
            menuController.SetFade(0.0f);
            for (float fTimeSoFar = 0.0f; fTimeSoFar <= _fadeInTime; fTimeSoFar += Time.deltaTime)
            {
                menuController.SetFade(fTimeSoFar / _fadeInTime);
                yield return null;
            }
            menuController.SetFade(1.0f);

            yield return new WaitForSeconds(_nextSceneDelayTime - _fadeInTime);
        }
        else
        {
            yield return new WaitForSeconds(_nextSceneDelayTime);
        }

        SceneManager.LoadScene(_nextScene);
    }
}
