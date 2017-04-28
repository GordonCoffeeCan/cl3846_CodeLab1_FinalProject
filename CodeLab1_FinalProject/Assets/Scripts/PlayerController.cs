using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 8;
    public float jumpSpeed = 5;

    private Rigidbody2D _rig;
    private Animator _anim;

    private string _ctrMove;
    private string _ctrAim;
    private string _ctrJump;

    private bool _isGround = false;
    private float _fallingLayerWeight = 1;

    private void Awake() {
        _rig = this.GetComponent<Rigidbody2D>();
        _anim = this.GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
		if(this.tag == "Human") {
            _ctrMove = "JoyHorizontal";
            _ctrAim = "JoyVertical";
            _ctrJump = "JoyJump";
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (_isGround == true) {
            if (Input.GetAxis(_ctrMove) > 0) {
                _anim.SetFloat("Speed", 1);
            } else if (Input.GetAxis(_ctrMove) < 0) {
                _anim.SetFloat("Speed", -1);
            } else {
                _anim.SetFloat("Speed", 0);
            }

            if (Input.GetButtonDown(_ctrJump)) {
                _rig.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            }

            _fallingLayerWeight = 0;
        } else {
            if (Input.GetAxis(_ctrMove) >= 0) {
                _anim.SetFloat("JumpSpeed", 1);
            } else {
                _anim.SetFloat("JumpSpeed", -1);
            }

            if (_rig.velocity.y > 0.5f || _rig.velocity.y < -0.5f) {
                _anim.SetFloat("Speed", 0);
                _fallingLayerWeight = 1;
            }
        }
        _rig.velocity = new Vector2(Input.GetAxis(_ctrMove) * speed * Time.deltaTime * 10, _rig.velocity.y);
        _anim.SetLayerWeight(1, _fallingLayerWeight);
    }

    private void FixedUpdate() {

        //Get a raycast2D to detect is on the Ground or not;
        //0.6f is amount of offset on Y axis to avoid the ray collide with Character itself;
        //0.01f is the amount of distence for the ray. Character in the middle air, the ray will not collide with the Ground;
        //Give the _isGround value with both true or false to detect whether the character is on the ground or not. And will able to jump or not.
        RaycastHit2D _groudnHit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y - 0.6f), -Vector2.up, 0.01f);
        if (_groudnHit.collider != null) {
            if (_groudnHit.collider.gameObject.tag == "Ground") {
                _isGround = true;
            }
        } else if(_groudnHit.collider == null) {
            _isGround = false;
        }

        //Get a raycast2D to detect is on the Ground or not
    }
}
