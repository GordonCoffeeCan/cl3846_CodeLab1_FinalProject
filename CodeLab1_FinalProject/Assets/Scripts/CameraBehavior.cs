using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {
    private Camera _camera;
    private float _speed = 5;
    private bool _levelShowed = false;
    private bool _playerShowed = false;

    private void Awake() {
        _camera = Camera.main;
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (_levelShowed == false) {
            Invoke("ShowLevel", 1);
        } else {
            Invoke("MoveToPlayer", 3);
        }

        if(_playerShowed == true) {
            if (GameData.isHumanTurn == true) {
                _camera.orthographicSize += Input.GetAxis("JoyZoom");
            } else {
                _camera.orthographicSize += Input.GetAxis("ZomZoom");
            }
        }

        Debug.Log(_playerShowed);

        if (_camera.orthographicSize < 5) {
            _playerShowed = true;
        }
    }

    private void ShowLevel() {
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, LevelLoader2D._offSetX / 4, _speed * Time.deltaTime);
        _levelShowed = true;
    }

    private void MoveToPlayer() {
        if (GameData.isHumanTurn == true) {
            ChoosePlayer(LevelLoader2D.playerHuman);
        } else {
            ChoosePlayer(LevelLoader2D.playerZombie);
        }

        if (_playerShowed == false) {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, 4.8f, _speed * Time.deltaTime);
        }
    }

    private void ChoosePlayer(GameObject _player) {
        _camera.transform.position = new Vector3(Mathf.Lerp(_camera.transform.position.x, _player.transform.position.x, _speed * Time.deltaTime), Mathf.Lerp(_camera.transform.position.y, _player.transform.position.y, _speed * Time.deltaTime), _camera.transform.position.z);
    }
}
