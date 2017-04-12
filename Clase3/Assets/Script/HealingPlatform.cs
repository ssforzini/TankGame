using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealingPlatform : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionStay(Collision col){
		if(col.gameObject.name == "Tanque" || col.gameObject.name == "TanqueEnemigo"){
			if(col.gameObject.GetComponent<TankMovement> ().life > 0){

				if (col.gameObject.name == "Tanque") {
					if(GameObject.Find ("TankLife").GetComponent<Slider> ().value < 1.0f){
						GameObject.Find ("TankLife").GetComponent<Slider> ().value -= 0.001f;
					}
				} else {
					if (GameObject.Find ("EnemyTankLife").GetComponent<Slider> ().value < 1.0f) {
						GameObject.Find ("EnemyTankLife").GetComponent<Slider> ().value -= 0.001f;
					}
				}
				if(col.gameObject.GetComponent<TankMovement> ().life < 50f){
					col.gameObject.GetComponent<TankMovement> ().life += 0.05f;
				}

				print (col.gameObject.GetComponent<TankMovement> ().life);
			}
		}
	}
}
