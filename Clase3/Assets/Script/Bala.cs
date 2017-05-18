using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

	public float fuerza;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody> ().AddRelativeForce (Vector3.right * fuerza, ForceMode.Impulse);
		Destroy (gameObject,2.0f);
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.name == "Tanque" || col.gameObject.name == "TanqueEnemigo"){
			if(col.gameObject.GetComponent<TankMovement> ().life > 0){

				if (col.gameObject.name == "Tanque") {
					GameObject.Find ("TankLife").GetComponent<Slider> ().value += 0.20f;
				} else {
					GameObject.Find ("EnemyTankLife").GetComponent<Slider>().value += 0.20f;
				}

				col.gameObject.GetComponent<TankMovement> ().life -= 20f;
				Destroy (gameObject);
			}
		}
	}
}
