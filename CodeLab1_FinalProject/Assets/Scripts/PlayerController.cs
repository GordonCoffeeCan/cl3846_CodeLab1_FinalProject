using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float speed = 1f;
    private float jumpSpeed = 3;

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
                _rig.AddForce(new Vector2(0, jumpSpeed * Time.deltaTime * 100), ForceMode2D.Impulse);
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
        _rig.velocity = new Vector2(Input.GetAxis(_ctrMove) * speed * Time.deltaTime * 100, _rig.velocity.y);
        _anim.SetLayerWeight(1, _fallingLayerWeight);
    }

    private void FixedUpdate() {
        RaycastHit2D _groudnHit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y - 0.6f), -Vector2.up, 0.01f);
        if (_groudnHit.collider != null) {
            if (_groudnHit.collider.gameObject.tag == "Ground") {
                _isGround = true;
            }
        } else if(_groudnHit.collider == null) {
            _isGround = false;
        }
    }

    /*private void OnCollisionStay2D(Collision2D _col) {
        if (_col.gameObject.tag == "Ground") {
            _isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D _col) {
        if (_col.gameObject.tag == "Ground") {
            _isGround = false;
        }
    }*/
}
