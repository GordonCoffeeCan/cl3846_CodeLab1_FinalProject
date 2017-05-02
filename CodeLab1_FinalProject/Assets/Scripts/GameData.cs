using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {
    public bool isHumanTrun = true;

    public static bool _isHumanTurn = true;


	// Use this for initialization
	/*void Start () {
		
	}*/
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.Alpha1)) {
            isHumanTrun = true;
        }else if (Input.GetKeyUp(KeyCode.Alpha2)) {
            isHumanTrun = false;
        }

        _isHumanTurn = isHumanTrun;
    }
}
