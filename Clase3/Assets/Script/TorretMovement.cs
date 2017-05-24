using UnityEngine;

public class TorretMovement : MonoBehaviour {

	public GameObject prefab;
	public Transform puntoSalida;

	public KeyCode leftMovement;
	public KeyCode rightMovement;
	public KeyCode shoot;

	private float time = 0;

	/* GET COMPONENT */
	private TankLife tnk;
	private AudioSource src;
	/* END COMPONENT */

	void Awake(){
		tnk = GetComponentInParent<TankLife> ();
		src = GameObject.Find ("Fire").GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(leftMovement)){
			transform.Rotate (Vector3.down * Time.deltaTime * 30f);
		}

		if(Input.GetKey(rightMovement)){
			transform.Rotate (Vector3.up * Time.deltaTime * 30f);
		}

		if(time >= 0){
			time -= Time.deltaTime;
		}

		if(Input.GetKeyDown(shoot) && time <= 0){
			src.Play ();
			if (tnk.getAlive()) {
				time = 1.5f;
				Instantiate (prefab, puntoSalida.position, puntoSalida.rotation);
			}
		}
	}
}
