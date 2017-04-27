using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LevelLoader2D : MonoBehaviour {

    private float offSetX = 12;
    private float offSetY = 4;

    public string[] fileNames;
    public static int levelNum;

    public static bool isLevelSet = false;

    public static GameObject _levelHolder;

    private List<GameObject> _levelObjects;

    // Use this for initialization
    void Start () {
        _levelHolder = new GameObject("Level Holder");

        _levelObjects = new List<GameObject>();
        SettLevel();
    }
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetKeyDown(KeyCode.P)) {
            //levelNum++;
            SceneManager.LoadScene("Main");
        }*/
	}

    private void SettLevel() {
        
        levelNum = 0;
        

        for (int i = 0; i < 1; i++) {
            string fileName = fileNames[levelNum];
            string filePath = Application.dataPath + "/" + fileName;
            StreamReader sr = new StreamReader(filePath);
            int yPos = 0;
            while (!sr.EndOfStream) {
                string line = sr.ReadLine();

                for (int xPos = 0; xPos < line.Length; xPos++) {
                    if (line[xPos] == 'x') {
                        GameObject _platform = Instantiate(Resources.Load("Prefabs/TestBox") as GameObject);
                        _levelObjects.Add(_platform);
                        Vector2 _platformSize = new Vector2(_platform.GetComponent<SpriteRenderer>().bounds.size.x, _platform.GetComponent<SpriteRenderer>().bounds.size.y);
                        _platform.transform.position = new Vector2(xPos * _platformSize.x - offSetX * _platformSize.x, yPos * _platformSize.y - offSetY * _platformSize.y);
                        //offSetX += _platformSize.x;
                        //offSetY += _platformSize.y;
                        _platform.transform.SetParent(_levelHolder.transform);
                    } else if (line[xPos] == 't' && i == 0) {
                        //GameObject _target = Instantiate(Resources.Load("Prefabs/Target") as GameObject);
                        //Rigidbody _targetRig = _target.GetComponent<Rigidbody>();
                        
                        //_target.transform.position = new Vector3(xPos + offSetX, 0.5f, yPos + offSetY);
                    }
                }
                yPos--;

            }
            sr.Close();
        }

        /*for (int i = 0; i < _levelObjects.Count; i++) {
            _levelObjects[i].transform.position -= new Vector3(offSetX, offSetY, 0);
        }*/
    }
}
