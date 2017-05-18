using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diamond : MonoBehaviour {

	private static float movement;
	private bool active;
	private float timeNotActive;

	void Start(){
		movement = Random.Range (0,2);
		timeNotActive = 10f;
		active = true;
	}

	void Update () {
		if(movement == 0){
			transform.Rotate (Vector3.up * Time.deltaTime * 6f);
		} else {
			transform.Rotate (Vector3.down * Time.deltaTime * 6f);
		}
			
		if(!active){
			timeNotActive -= Time.deltaTime;
			if(timeNotActive <= 0f){
				timeNotActive = 10f;
				active = true;
				for(int i = 0; i < gameObject.GetComponentsInChildren<Renderer> ().Length; i++){
					if(gameObject.GetComponentsInChildren<Renderer> ()[i].name == "Disc"){
						gameObject.GetComponentsInChildren<Renderer> ()[i].enabled = true;
					}
				}
			}
		}
		
	}

	void OnTriggerEnter(Collider col){
		
		if(active){
			if(col.gameObject.tag == "tank"){
				if(col.gameObject.GetComponent<TankMovement>().alive){
					if (col.gameObject.name == "Tanque") {
						if(GameObject.Find ("TankLife").GetComponent<Slider> ().value < 1.0f){
							GameObject.Find ("TankLife").GetComponent<Slider> ().value -= 0.15f;
						}
					} else {
						if (GameObject.Find ("EnemyTankLife").GetComponent<Slider> ().value < 1.0f) {
							GameObject.Find ("EnemyTankLife").GetComponent<Slider> ().value -= 0.15f;
						}
					}
					col.gameObject.GetComponent<TankMovement> ().life += 15f;
				}
			}
		}

		active = false;
		for(int i = 0; i < gameObject.GetComponentsInChildren<Renderer> ().Length; i++){
			if(gameObject.GetComponentsInChildren<Renderer> ()[i].name == "Disc"){
				gameObject.GetComponentsInChildren<Renderer> ()[i].enabled = false;
			}
		}
	}
}
