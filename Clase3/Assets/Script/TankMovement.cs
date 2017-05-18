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

	private string buttonPress = "";


	// Use this for initialization
	void Start () {
		life = 100;
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
				buttonPress = "up";
			}

			if(Input.GetKey(keys[1])){
				//transform.Translate (Vector3.left * Time.deltaTime * speed);
				GetComponent<Rigidbody>().AddForce(-transform.right * Time.deltaTime * speed);
				buttonPress = "down";
			}

			if(Input.GetKey(keys[2])){
				transform.Rotate (Vector3.up * Time.deltaTime * rotateSpeed);
				buttonPress = "right";
			}

			if(Input.GetKey(keys[3])){
				transform.Rotate (Vector3.down * Time.deltaTime * rotateSpeed);
				buttonPress = "left";
			}
		}


		if(life == 0){
			foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
				r.enabled = false;
			}
			GetComponent<BoxCollider> ().enabled = false;
			GetComponent<Rigidbody> ().isKinematic = true;
			alive = false;
		} else if(life > 100f){
			life = 100f;
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
			if(buttonPress == "up"){
				GetComponent<Rigidbody>().AddForce(transform.right * Time.deltaTime * speed / 4,ForceMode.Impulse);
			} else if(buttonPress == "down") {
				GetComponent<Rigidbody>().AddForce(-transform.right * Time.deltaTime * speed / 4, ForceMode.Impulse);
			}
		}
	}
}
