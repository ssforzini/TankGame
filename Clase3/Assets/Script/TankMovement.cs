using UnityEngine;
using UnityEngine.UI;

public class TankMovement : MonoBehaviour {

	[HideInInspector] public float life;
	[HideInInspector] public int numberOfLifes;
	[HideInInspector] public bool alive;

	public KeyCode[] keys;
	public GameObject[] checkpoints;

	private float velocityForward;
	private float valocityBack;
	private bool collisionTerrain;
	public GameObject slider;
	public GameObject text;
	private GameObject dead;

	[SerializeField] private float deadXPos;

	[SerializeField] private float speed;
	[SerializeField] private float rotateSpeed;

	private string buttonPress = "";

	// Use this for initialization
	void Start () {
		life = 100;
		numberOfLifes = 1;
		text.GetComponent<Text> ().text = numberOfLifes.ToString ();
		alive = true;
		dead = GameObject.Find ("DeadText");
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(keys[4])){
			transform.position = checkpoints[Random.Range(0,checkpoints.Length)].GetComponent<Transform>().position;
			transform.rotation = new Quaternion (0,0,0,0);
		}

		if(collisionTerrain){
			if(Input.GetKey(keys[0])){
				GetComponent<Rigidbody>().AddForce(transform.right * Time.deltaTime * speed);
				buttonPress = "up";
			}

			if(Input.GetKey(keys[1])){
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


		if(life == 0 && alive){
			numberOfLifes--;
			text.GetComponent<Text> ().text = numberOfLifes.ToString ();
			if (numberOfLifes <= 0) {
				foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
					r.enabled = false;
				}
				GetComponent<BoxCollider> ().enabled = false;
				GetComponent<Rigidbody> ().isKinematic = true;
				alive = false;
				dead.GetComponent<RectTransform> ().localPosition = new Vector3 (deadXPos,0,0);
				dead.GetComponent<Text> ().text = "YOU ARE \n  DEAD";
			} else {
				life = 100f;
				slider.GetComponent<Slider> ().value = 0f;

				transform.position = checkpoints[Random.Range(0,checkpoints.Length)].GetComponent<Transform>().position;
				transform.rotation = new Quaternion (0,0,0,0);
			}
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
