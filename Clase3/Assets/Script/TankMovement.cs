using UnityEngine;
using UnityEngine.UI;

public class TankMovement : MonoBehaviour {

	public KeyCode[] keys;
	public GameObject[] checkpoints;

	private float velocityForward;
	private float valocityBack;
	private bool collisionTerrain;
	public GameObject slider;

	[SerializeField] private float deadXPos;

	[SerializeField] private float speed;
	[SerializeField] private float rotateSpeed;

	private string buttonPress = "";

	/* GET COMPONENTS */
	private RectTransform dPos;
	private Text dText;
	private Rigidbody rb;
	private BoxCollider bcl;
	private Slider sld;
	private AudioSource srcMusic;
	private AudioSource srcGameEnd;
	private TankLife tnk;
	/* END GET COMPONENTS */

	void Awake(){
		dPos = GameObject.Find ("DeadText").GetComponent<RectTransform> ();
		dText = GameObject.Find ("DeadText").GetComponent<Text> ();
		rb = GetComponent<Rigidbody> ();
		bcl = GetComponent<BoxCollider> ();
		sld = slider.GetComponent<Slider> ();
		srcMusic = GameObject.Find ("Music").GetComponent<AudioSource> ();
		srcGameEnd = GameObject.Find ("GameEnd").GetComponent<AudioSource> ();
		tnk = GetComponent<TankLife> ();
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


		if(tnk.getLife() == 0 && tnk.getAlive()){
			tnk.decreaseNumberOfLifes();
			if (tnk.getNumberOfLifes() <= 0) {
				foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
					r.enabled = false;
				}
				bcl.enabled = false;
				rb.isKinematic = true;
				tnk.setAlive (false);
				dPos.localPosition = new Vector3 (deadXPos,0,0);
				dText.text = "YOU ARE \n  DEAD";
				srcMusic.Stop ();
				srcGameEnd.Play ();
			} else {
				tnk.setLife(100f);
				sld.value = 0f;

				transform.position = checkpoints[Random.Range(0,checkpoints.Length)].transform.position;
				transform.rotation = new Quaternion (0,0,0,0);
			}
		} else if(tnk.getLife() > 100f){
			tnk.setLife(100f);
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
