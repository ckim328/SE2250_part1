using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
	[Header("Set in Inspector: Enemy")]
	public float speed=10f;
	public float fireRate = 0.3f;
	public float health= 10;
	public int score=100;
    public Text scoreValue;
    public Text highscoreValue;

	private BoundsCheck bndCheck;
    private int highscore = 0;
    private static int totalScore;

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
        if (scoreValue.text == "XX")
        {
            scoreValue.text = "0";
        }
        if (highscoreValue.text == "XX")
        {
            int inbetween = PlayerPrefs.GetInt("HighScore", 0);
            highscoreValue.text = inbetween.ToString();
        }
	}

	// Update is called once per frame  
	void Update () {
		Move();

		if (bndCheck!= null&& bndCheck.offDown){
			// we have gone off the bottom of the screen so we need to destroy the object 
			Destroy (gameObject);
		}

	}
	public virtual void Move(){
		Vector3 tempPos = pos;
		tempPos.y -= speed * Time.deltaTime;
		pos = tempPos;
	}
	void OnCollisionEnter(Collision coll){
		GameObject	otherGO = coll.gameObject;
        switch (otherGO.tag){
            case "ProjectileHero":
                Projectile p = otherGO.GetComponent<Projectile>();
                if (!bndCheck.isOnScreen)
                {
                    Destroy(otherGO);
                    break;
                }
                health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                if (health <= 0)
                {
                    totalScore += this.score;
                    if (totalScore > highscore)
                    {
                        PlayerPrefs.SetInt("HighScore", totalScore);
                        highscoreValue.text = totalScore.ToString();
                    }
                    scoreValue.text = totalScore.ToString();
                    Destroy(this.gameObject);
                }
                Destroy(otherGO);
                break;
            default:
                print("Enemy Hit by non-ProjectileHero: " + otherGO.name);
                break;

        }
		/*if (otherGO.tag == "ProjectileHero") {
			Destroy (otherGO);
			Destroy (gameObject);

		} else {
			print ("Enemy hit by non-projectilehero" + otherGO.name);
		}*/
	}
}