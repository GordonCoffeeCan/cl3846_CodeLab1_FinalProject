using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour {

    private Transform currentWeapon;

    private void Awake() {
        if(this.tag == "Human") {
            currentWeapon = this.transform.Find("SpriteHolder/Human_body/Human_leftArm/Weapon_Stone");
        } else if (this.tag == "Zombie") {
            currentWeapon = this.transform.Find("SpriteHolder/Zombie_body/Zombie_leftArm/Weapon_Skull");
        }
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerController.isGround == false) {
            currentWeapon.gameObject.SetActive(false);
        } else {
            currentWeapon.gameObject.SetActive(true);
        }
	}
}
