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

	[SerializeField] private float deadXPos;

	[SerializeField] private float speed;
	[SerializeField] private float rotateSpeed;

	private string buttonPress = "";

	/* GET COMPONENTS */
	private RectTransform dPos;
	private Text dText;
	private Rigidbody rb;
	private Text tText;
	private BoxCollider bcl;
	private Slider sld;
	private AudioSource srcMusic;
	private AudioSource srcGameEnd;
	/* END GET COMPONENTS */

	void Awake(){
		dPos = GameObject.Find ("DeadText").GetComponent<RectTransform> ();
		dText = GameObject.Find ("DeadText").GetComponent<Text> ();
		rb = GetComponent<Rigidbody> ();
		tText = text.GetComponent<Text> ();
		bcl = GetComponent<BoxCollider> ();
		sld = slider.GetComponent<Slider> ();
		srcMusic = GameObject.Find ("Music").GetComponent<AudioSource> ();
		srcGameEnd = GameObject.Find ("GameEnd").GetComponent<AudioSource> ();
	}

	void Start () {
		life = 100;
		numberOfLifes = 1;
		tText.text = numberOfLifes.ToString ();
		alive = true;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(keys[4]) && transform.rotation.z > 150f){
			transform.position = checkpoints[Random.Range(0,checkpoints.Length)].transform.position;
			transform.rotation = new Quaternion (0,0,0,0);
		}

		if(collisionTerrain){
			if(Input.GetKey(keys[0])){
				rb.AddForce(transform.right * Time.deltaTime * speed);
				buttonPress = "up";
			}

			if(Input.GetKey(keys[1])){
				rb.AddForce(-transform.right * Time.deltaTime * speed);
				buttonPress = "down";
			}

			if(Input.GetKey(keys[2])){
				rb.AddTorque (Vector3.up * Time.deltaTime * rotateSpeed);
				buttonPress = "right";
			}

			if(Input.GetKey(keys[3])){
				rb.AddTorque (Vector3.down * Time.deltaTime * rotateSpeed);
				buttonPress = "left";
			}
		}


		if(life == 0 && alive){
			numberOfLifes--;
			tText.text = numberOfLifes.ToString ();
			if (numberOfLifes <= 0) {
				foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
					r.enabled = false;
				}
				bcl.enabled = false;
				rb.isKinematic = true;
				alive = false;
				dPos.localPosition = new Vector3 (deadXPos,0,0);
				dText.text = "YOU ARE \n  DEAD";
				srcMusic.Stop ();
				srcGameEnd.Play ();
			} else {
				life = 100f;
				sld.value = 0f;

				transform.position = checkpoints[Random.Range(0,checkpoints.Length)].transform.position;
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
				rb.AddForce(transform.right * Time.deltaTime * speed / 4,ForceMode.Impulse);
			} else if(buttonPress == "down") {
				rb.AddForce(-transform.right * Time.deltaTime * speed / 4, ForceMode.Impulse);
			}
		}
	}
}
