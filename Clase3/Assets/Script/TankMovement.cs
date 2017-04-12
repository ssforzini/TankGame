using UnityEngine;

public class TankMovement : MonoBehaviour {

	public KeyCode upMovement;
	public KeyCode downMovement;
	public KeyCode rightMovement;
	public KeyCode leftMovement;
	public float life;
	public bool alive;

	public KeyCode[] array;

	// Use this for initialization
	void Start () {
		life = 50;
		alive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(upMovement)){
			transform.Translate (Vector3.right * Time.deltaTime * 10f);
		}

		if(Input.GetKey(downMovement)){
			transform.Translate (Vector3.left * Time.deltaTime * 10f);
		}

		if(Input.GetKey(rightMovement)){
			transform.Rotate (Vector3.up * Time.deltaTime * 25f);
		}

		if(Input.GetKey(leftMovement)){
			transform.Rotate (Vector3.down * Time.deltaTime * 25f);
		}

		if(life == 0){
			foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
				r.enabled = false;
			}
			GetComponent<BoxCollider> ().enabled = false;
			GetComponent<Rigidbody> ().isKinematic = true;
			alive = false;
		} else if(life > 50f){
			life = 50f;
		}
	}
}
