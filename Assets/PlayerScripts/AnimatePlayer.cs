using UnityEngine;
using System.Collections;

public class AnimatePlayer : MonoBehaviour {

	private Animator animate;

	// Use this for initialization
	void Start () {
	
		animate = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		float move = Input.GetAxis ("Vertical");
		animate.SetFloat("Speed", move);
	}
}
