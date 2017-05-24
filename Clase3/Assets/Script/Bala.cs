using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

	public float fuerza;

	/* GET COMPONENTS */
	private Rigidbody rb;
	private Slider enemySld;
	private Slider tankSld;
	private AudioSource src;
	/* END GET COMPONENTS */

	void Awake(){
		src = GameObject.Find ("Explosion").GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody> ();
		enemySld = GameObject.Find ("EnemyTankLife").GetComponent<Slider> ();
		tankSld = GameObject.Find ("TankLife").GetComponent<Slider> ();
	}
	void Start () {
		rb.AddRelativeForce (Vector3.right * fuerza, ForceMode.Impulse);
		Destroy (gameObject,2.0f);
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.name == "Tanque" || col.gameObject.name == "TanqueEnemigo"){
			if(col.gameObject.GetComponent<TankLife> ().getLife() > 0){

				if (col.gameObject.name == "Tanque") {
					tankSld.value += 0.20f;
				} else {
					enemySld.value += 0.20f;
				}

				col.gameObject.GetComponent<TankLife> ().setLife(col.gameObject.GetComponent<TankLife> ().getLife() - 20f);
			}
		}
		src.Play ();
		Destroy (gameObject);
	}
}
