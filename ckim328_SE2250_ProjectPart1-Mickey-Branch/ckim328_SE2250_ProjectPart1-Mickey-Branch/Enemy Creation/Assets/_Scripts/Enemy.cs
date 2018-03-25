using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour {
	[Header("Set in Inspector: Enemy")]
	public float speed=10f;
	public float fireRate = 0.3f;
	public float health= 10;
	public int score=100; 
	System.Random randomizer = new System.Random ();


	private BoundsCheck bndCheck;

	void Awake(){
		bndCheck = GetComponent<BoundsCheck> ();
	}
	//this is a property, a method that acts like a field
	public Vector3 pos {
		get {
			return(this.transform.position);
		}
		set{ this.transform.position = value; }
	}
	// Use this for initialization
	void Start () {
		 
	}
	
	// Update is called once per frame
	void Update () {
		int movetype = randomizer.Next(2);
		if (gameObject.CompareTag("EN0"))
			{
			MoveDown ();
			}
			if (gameObject.CompareTag ("EN1")){
				switch(movetype){
				case 0:
					MoveDiag1();
					break;
				case 1:
					MoveDiag2();
				break;
				}
			}

		if (bndCheck!= null&& !bndCheck.offDown){
			// we have gone off the bottom of the screen so we need to destroy the object 
				Destroy (gameObject);
		}

	}
	public virtual void MoveDown(){
		Vector3 tempPos = pos;
		tempPos.y -= speed * Time.deltaTime;
		pos = tempPos;
	}

	public virtual void MoveDiag1(){
		Vector3 tempPos = pos;
		tempPos.y -= speed * Time.deltaTime;
		tempPos.x -= speed * Time.deltaTime;
		pos = tempPos;
	}

	public virtual void MoveDiag2(){
		Vector3 tempPos = pos;
		tempPos.y -= speed * Time.deltaTime;
		tempPos.x += speed * Time.deltaTime;
		pos = tempPos;
	}
}
