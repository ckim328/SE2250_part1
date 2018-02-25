using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy {

	public override void Move(){
		Vector3 tempPos = pos;
		int movetype = Random.Range (0, 1);
		switch (movetype) {
		case 0:
			tempPos.x -= speed * Time.deltaTime;
			break;
		case 1: 
			tempPos.x += speed * Time.deltaTime;
			break;
		}
		pos = tempPos;
		base.Move ();
	}

}
