using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 8;
    public float jumpSpeed = 5;
    public float aimSpeed = 30;
    public float reticleFadeSpeed = 5;

    private float _reticleTimer;
    private float _reticleTimerRecord = 1.2f;

    private Rigidbody2D _rig;
    private Animator _anim;
    private Transform _headPivot;
    private Transform _reticlePivot;
    private SpriteRenderer _reticle;
    private float _reticleCounterAngle;
    private Transform _spriteHolder;

    private float _speed;
    private bool _directionChanged;

    private string _ctrMove;
    private string _ctrAim;
    private string _ctrJump;

    private bool _isGround = false;

    private void Awake() {
        _rig = this.GetComponent<Rigidbody2D>();
        _anim = this.GetComponent<Animator>();
        _reticlePivot = this.transform.Find("ReticlePivot");
        _reticle = _reticlePivot.Find("Reticle").GetComponent<SpriteRenderer>();
        _spriteHolder = this.transform.Find("SpriteHolder");
        _speed = speed;
        _reticleTimer = _reticleTimerRecord;
    }

    // Use this for initialization
    void Start () {
		if(this.tag == "Human") {
            _ctrMove = "JoyHorizontal";
            _ctrAim = "JoyVertical";
            _ctrJump = "JoyJump";
            _headPivot = this.transform.Find("Human_body/HeadPivot");
        } else if (this.tag == "Zombie") {
            _ctrMove = "ZomHorizontal";
            _ctrAim = "ZomVertical";
            _ctrJump = "ZomJump";
            _headPivot = this.transform.Find("Zombie_body/HeadPivot");
        }
        _reticle.color = new Color(1, 1, 1, 0);
        _reticleCounterAngle = 0;
        _directionChanged = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (_isGround == true) {
            if (Mathf.Abs(Input.GetAxis(_ctrAim)) > 0.75f) {
                if (_spriteHolder.localScale.x > 0) {
                    if (_directionChanged == true) {
                        _reticlePivot.rotation = Quaternion.Euler(new Vector3(0, 0, _reticleCounterAngle));
                        _directionChanged = false;
                    }
                    
                    if (_reticlePivot.rotation.eulerAngles.z >= 0 && _reticlePivot.rotation.eulerAngles.z < 110) {
                        if (_reticlePivot.rotation.eulerAngles.z > 90) {
                            _reticlePivot.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                        }
                        _reticlePivot.Rotate(new Vector3(0, 0, Input.GetAxis(_ctrAim) * Time.deltaTime * 100));
                        _reticleCounterAngle = (90 - _reticlePivot.rotation.eulerAngles.z) * 2 + _reticlePivot.rotation.eulerAngles.z;
                    } else if (_reticlePivot.rotation.eulerAngles.z >= 260 && _reticlePivot.rotation.eulerAngles.z < 360) {
                        if (_reticlePivot.rotation.eulerAngles.z < 270) {
                            _reticlePivot.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
                        }
                        _reticlePivot.Rotate(new Vector3(0, 0, Input.GetAxis(_ctrAim) * Time.deltaTime * 100));
                        _reticleCounterAngle = _reticlePivot.rotation.eulerAngles.z - (_reticlePivot.rotation.eulerAngles.z - 270) * 2;
                    }
                } else if(_spriteHolder.localScale.x < 0) {
                    if (_directionChanged == false) {
                        _reticlePivot.rotation = Quaternion.Euler(new Vector3(0, 0, _reticleCounterAngle));
                        _directionChanged = true;
                    }
                    if (_reticlePivot.rotation.eulerAngles.z > 87 && _reticlePivot.rotation.eulerAngles.z < 273) {
                        if (_reticlePivot.rotation.eulerAngles.z < 90) {
                            _reticlePivot.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                            _reticleCounterAngle = _reticlePivot.rotation.eulerAngles.z - (_reticlePivot.rotation.eulerAngles.z - 90) * 2;
                        } else if (_reticlePivot.rotation.eulerAngles.z > 273) {
                            Debug.Log("Yes");
                            _reticlePivot.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
                            _reticleCounterAngle = (270 - _reticlePivot.rotation.eulerAngles.z) * 2 + _reticlePivot.rotation.eulerAngles.z;
                        }

                        _reticlePivot.Rotate(new Vector3(0, 0, -Input.GetAxis(_ctrAim) * Time.deltaTime * 100));
                    }

                    Debug.Log(_reticlePivot.rotation.eulerAngles.z);
                }


                /*if (_headPivot.rotation.eulerAngles.z >= 0 && _headPivot.rotation.eulerAngles.z < 90) {
                    if (_headPivot.rotation.eulerAngles.z > 30) {
                        _headPivot.rotation = Quaternion.Euler(new Vector3(0, 0, 30));
                    }
                } else if (_headPivot.rotation.eulerAngles.z >= 270 && _headPivot.rotation.eulerAngles.z < 360) {
                    if (_headPivot.rotation.eulerAngles.z < 315) {
                        _headPivot.rotation = Quaternion.Euler(new Vector3(0, 0, -45));
                    }
                }
                _headPivot.Rotate(new Vector3(0, 0, Input.GetAxis(_ctrAim) * Time.deltaTime * 100));*/


                _reticleTimer = _reticleTimerRecord;
                _reticle.color = new Color(1, 1, 1, Mathf.Lerp(_reticle.color.a, 1, reticleFadeSpeed * Time.deltaTime));
                
                _anim.SetFloat("Speed", 0);
                speed = 0;
            } else{
                if (Input.GetAxis(_ctrMove) > 0) {
                    _anim.SetFloat("Speed", 1);
                    _anim.SetLayerWeight(1, 0);
                } else if (Input.GetAxis(_ctrMove) < 0) {
                    _anim.SetFloat("Speed", 1);
                    _anim.SetLayerWeight(1, 1);
                } else {
                    _anim.SetFloat("Speed", 0);
                }
                speed = _speed;

                _reticleTimer -= Time.deltaTime;
                if(_reticleTimer <= 0 || Mathf.Abs(Input.GetAxis(_ctrMove)) > 0) {
                    _reticleTimer = 0;
                    _reticle.color = new Color(1, 1, 1, Mathf.Lerp(_reticle.color.a, 0, reticleFadeSpeed * Time.deltaTime));
                }
            }

            if (Input.GetButtonDown(_ctrJump)) {
                _rig.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            }
        } else {
            if (Input.GetAxis(_ctrMove) > 0) {
                _anim.SetFloat("Speed", 1);
                _anim.SetLayerWeight(1, 0);
            } else if (Input.GetAxis(_ctrMove) < 0) {
                _anim.SetFloat("Speed", 1);
                _anim.SetLayerWeight(1, 1);
            } else {
                _anim.SetFloat("Speed", 0);
            }

            if (_rig.velocity.y > 0.5f || _rig.velocity.y < -0.5f) {
                _anim.SetFloat("Speed", 0);
            }
        }
        _rig.velocity = new Vector2(Input.GetAxis(_ctrMove) * speed * Time.deltaTime * 10, _rig.velocity.y);
        _anim.SetBool("OnGround", _isGround);
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
        //Get a raycast2D to detect is on the Ground or not ------End
    }
}
