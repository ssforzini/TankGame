using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankLife : MonoBehaviour {

	private float life;
	[SerializeField]private int numberOfLifes;
	private bool alive;
	public GameObject text;

	/* GET COMPONENT */
	private Text tText;
	/* END GET COMPONENT */

	void Awake(){
		tText = text.GetComponent<Text> ();
	}

	// Use this for initialization
	void Start () {
		life = 100;
		alive = true;
		tText.text = numberOfLifes.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setLife(float _life){ life = _life; }
	public float getLife(){ return life; }
	public void setAlive(bool _alive){ alive = _alive; }
	public bool getAlive(){ return alive; }
	public void setNumberOfLifes(int _numberOfLifes){ numberOfLifes = _numberOfLifes; }
	public int getNumberOfLifes(){ return numberOfLifes; }

	public void decreaseNumberOfLifes(){ 
		numberOfLifes--; 
		tText.text = numberOfLifes.ToString ();
	}
}
