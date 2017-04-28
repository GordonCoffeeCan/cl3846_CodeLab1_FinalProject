using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

    private Transform _camera;

    private float _speed = 5;

    private void Awake() {
        _camera = Camera.main.transform;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Invoke("MoveToPlayer", 0.8f);
	}

    private void MoveToPlayer() {
        _camera.position = new Vector3(Mathf.Lerp(_camera.position.x, LevelLoader2D.playerHuman.transform.position.x, _speed * Time.deltaTime), Mathf.Lerp(_camera.position.y, LevelLoader2D.playerHuman.transform.position.y, _speed * Time.deltaTime), _camera.position.z);
    }
}
