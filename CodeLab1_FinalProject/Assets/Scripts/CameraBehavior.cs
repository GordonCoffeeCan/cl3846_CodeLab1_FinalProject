using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

    public static bool playerShowed;

    private Camera _camera;
    private float _speed = 5;
    private bool _levelShowed = false;

    private float _minCamOrthographicSize = 5;
    private float _maxCamOrthographicSize = 0;
    private float _zoomCam;

    private void Awake() {
        _camera = Camera.main;
    }

    // Use this for initialization
    void Start () {
        _zoomCam = 0;
        playerShowed = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (_levelShowed == false) {
            Invoke("ShowLevel", 1); // Show Level
        } else if(_levelShowed == true) {
            Invoke("MoveToPlayer", 3); //Level showed, now show player;
        }

        _maxCamOrthographicSize = LevelLoader2D._offSetX / 4;
    }

    private void ShowLevel() {
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _maxCamOrthographicSize, _speed * Time.deltaTime);
        _levelShowed = true;
    }

    private void MoveToPlayer() {
        if (GameData.isHumanTurn == true) {
            ChoosePlayer(LevelLoader2D.playerHuman);
        } else {
            ChoosePlayer(LevelLoader2D.playerZombie);
        }

        if (playerShowed == false) {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _minCamOrthographicSize - 0.2f, _speed * Time.deltaTime);
            if (_camera.orthographicSize < _minCamOrthographicSize) {
                playerShowed = true;
            }
        }

        if (playerShowed == true) {
            //Choose Zoom Controll
            if (GameData.isHumanTurn == true) {
                ZoomCamera("JoyZoom");
                MoveToZeroAndPlayer(LevelLoader2D.playerHuman);
            } else {
                ZoomCamera("ZomZoom");
                MoveToZeroAndPlayer(LevelLoader2D.playerZombie);
            }
            //Choose Zoom Controll --- End
        }

        _zoomCam = _camera.orthographicSize;
    }

    private void ChoosePlayer(GameObject _player) {
        _camera.transform.position = new Vector3(Mathf.Lerp(_camera.transform.position.x, _player.transform.position.x, _speed * Time.deltaTime), Mathf.Lerp(_camera.transform.position.y, _player.transform.position.y, _speed * Time.deltaTime), _camera.transform.position.z);
    }

    private void MoveToZeroAndPlayer(GameObject _gameObject) {
        if (_zoomCam > (_maxCamOrthographicSize - 0.5f)) {
            _camera.transform.position = new Vector3(Mathf.Lerp(_camera.transform.position.x, 0, _speed * Time.deltaTime), Mathf.Lerp(_camera.transform.position.y, 0, _speed * Time.deltaTime), _camera.transform.position.z);
        } else {
            ChoosePlayer(_gameObject);
        }
    }

    private void ZoomCamera(string _axisName) {
        if (Mathf.Abs(Input.GetAxis(_axisName)) > 0.3f){
            _zoomCam = Mathf.Lerp(_zoomCam, _zoomCam += Input.GetAxis(_axisName), _speed * Time.deltaTime);
            _zoomCam = Mathf.Clamp(_zoomCam, _minCamOrthographicSize, _maxCamOrthographicSize);
            _camera.orthographicSize = _zoomCam;
        }
    }
}
