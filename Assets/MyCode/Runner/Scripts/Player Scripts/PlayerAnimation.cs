using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

	private Animator animator;
	public static bool comeDown;
	private bool comeDownStop;


	// move to the left (negative side)
	private float noteSpeed = -1f;
	private Rigidbody2D rigidBody;
  //[SerializeField] private AudioClip gotCoinSound;


	void Awake ()
	{
		// get the rigidbody of the obstacle
		rigidBody = GetComponent<Rigidbody2D>();
	}
	// Use this for initialization
	void Start () {

		// get animation controller
		animator = GetComponent<Animator>();
		comeDown =false;
		comeDownStop =false;


	}

	// Update is called once per frame
	void Update () {


	}

	// if we collide with something, stop the Run animation
	void OnCollisionEnter2D (Collision2D collider)
	{

		if (collider.gameObject.tag == "BreathingIsland") {

			comeDownStop = true;
			comeDown = false;



		}


	}


	// if we leave the collider, resume running animation
	void OnCollisionExit2D (Collision2D collider)
	{

		if (collider.gameObject.tag == "CloudIsland") {
			comeDown = true;
		}


	}

}
