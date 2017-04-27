using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelLoader2D : MonoBehaviour {

    private string _filePath;
    private string _fileName = "Level01.txt";
    private GameObject _levelHolder;
    private StreamReader _streamReader;
    private string _docLine;
    private int _posY;
    private float _posScale;

    private float _offSetX;
    private float _offSetY;

	// Use this for initialization
	void Start () {
        _levelHolder = new GameObject("Level Holder");
        _filePath = Application.dataPath + "/" + _fileName;
        _streamReader = new StreamReader(_filePath);
        SetLevel();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SetLevel() {
        int _lineNumber = 0;
        while (!_streamReader.EndOfStream) {
            
            _docLine = _streamReader.ReadLine();

            for (int _posX = 0; _posX < _docLine.Length; _posX++) {
                if (_docLine[_posX] == 'x') {
                    SetObject("TestBox", _posX);
                } else if(_docLine[_posX] == 'c') {
                    SetObject("StoneCollum", _posX);
                }

                if (_lineNumber == 0) {
                    _offSetX += _posScale;
                }
            }
            _offSetY += _posScale;
            _posY--;
            _lineNumber++;
        }
        _streamReader.Close();

        _levelHolder.transform.position = new Vector3(-_offSetX / 2 + _posScale / 2, _offSetY / 2 - _posScale / 2, 0);
    }

    private void SetObject(string _name, int _posX) {
        GameObject _gameObject = Instantiate(Resources.Load("Prefabs/" + _name)) as GameObject;
        _posScale = _gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        _gameObject.transform.parent = _levelHolder.transform;
        _gameObject.transform.position = new Vector3(_posX * _posScale, _posY * _posScale, 0);
    }
}
