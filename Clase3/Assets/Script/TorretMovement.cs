using UnityEngine;

public class TorretMovement : MonoBehaviour {

	public GameObject prefab;
	public Transform puntoSalida;

	public KeyCode leftMovement;
	public KeyCode rightMovement;
	public KeyCode shoot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(leftMovement)){
			transform.Rotate (Vector3.down * Time.deltaTime * 30f);
		}

		if(Input.GetKey(rightMovement)){
			transform.Rotate (Vector3.up * Time.deltaTime * 30f);
		}

		if(Input.GetKeyDown(shoot)){
			if (GetComponentInParent<TankMovement> ().alive) {
				Instantiate (prefab, puntoSalida.position, puntoSalida.rotation);
			}

		}
	}
}
