using UnityEngine;

public class TankMovement : MonoBehaviour {

	public float life;
	public bool alive;
	[SerializeField] private float speed;
	[SerializeField] private float rotateSpeed;

	public KeyCode[] keys;
	public GameObject[] checkpoints;

	private float velocityForward;
	private float valocityBack;
	private bool collisionTerrain;


	// Use this for initialization
	void Start () {
		life = 50;
		alive = true;
	}
	
	// Update is called once per frame
	void Update () {
		//GetComponent<Rigidbody>().AddForce(-transform.up * Time.deltaTime * 7500f);

		if(Input.GetKeyDown(keys[4])){
			transform.position = checkpoints[Random.Range(0,checkpoints.Length)].GetComponent<Transform>().position;
			transform.rotation = new Quaternion (0,0,0,0);
		}

		if(collisionTerrain){
			if(Input.GetKey(keys[0])){
				//transform.Translate (Vector3.right * Time.deltaTime * speed);
				GetComponent<Rigidbody>().AddForce(transform.right * Time.deltaTime * speed);
			}

			if(Input.GetKey(keys[1])){
				//transform.Translate (Vector3.left * Time.deltaTime * speed);
				GetComponent<Rigidbody>().AddForce(-transform.right * Time.deltaTime * speed);
			}

			if(Input.GetKey(keys[2])){
				transform.Rotate (Vector3.up * Time.deltaTime * rotateSpeed);
			}

			if(Input.GetKey(keys[3])){
				transform.Rotate (Vector3.down * Time.deltaTime * rotateSpeed);
			}
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

	void OnCollisionStay(Collision col){
		if(col.gameObject.name == "Terrain" || col.gameObject.tag == "Bridges"){
			collisionTerrain = true;
		}
	}
	void OnCollisionExit(Collision col){
		if(col.gameObject.name == "Terrain" || col.gameObject.tag == "Bridges"){
			collisionTerrain = false;
		}
	}
}
