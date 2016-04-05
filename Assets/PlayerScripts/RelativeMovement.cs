using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour {
	[SerializeField] private Transform target;

	private float moveSpeed = 6.0f, vertSpeed;
	private CharacterController charController;
	private ControllerColliderHit contact;
	private Animator animate;
	private PlayerControl control;

	// Use this for initialization
	void Start () {

		target = GameObject.Find ("Main Camera").transform;

		charController = GetComponent<CharacterController> ();

		charController.center = new Vector3 (0, 1, 0);
		charController.radius = 0.4f;

		control = gameObject.GetComponent<PlayerControl> ();

		vertSpeed = control.MinFall;

		animate = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 movement = Vector3.zero;

		float horInput = Input.GetAxis ("Horizontal");
		float vertInput = Input.GetAxis ("Vertical");

		bool hitGround = false;

		RaycastHit hit;

		movement.x = horInput * moveSpeed;
		movement.z = vertInput * moveSpeed;

        //Debug.Log(movement.x);

        this.gameObject.GetComponent<AnimatePlayer>().SelectingAnimatation(movement);

		movement = Vector3.ClampMagnitude (movement, moveSpeed);

		if (horInput != 0 || vertInput != 0) {

			movement.x = horInput;
			movement.y = vertInput;

			Quaternion tmp = target.rotation;
			target.eulerAngles = new Vector3 (0, target.eulerAngles.y, 0);
			movement = target.TransformDirection (movement);

			target.rotation = tmp;

			transform.rotation = Quaternion.LookRotation (movement);
		}

		if (vertSpeed < 0 && Physics.Raycast (transform.position, Vector3.down, out hit)) {

			float check = (charController.height + charController.radius) / 1.9f;

			hitGround = hit.distance <= check;
		}

		if (hitGround) {
			if (Input.GetButton ("Jump"))
				vertSpeed = control.JumpSpeed;
			else {
				
				vertSpeed = -0.1f;
				animate.SetBool ("Jumping", false);
			}
		} else {
			vertSpeed += control.Gravity * 5 * Time.deltaTime;

			if (vertSpeed < control.TerminalVelocity)
				vertSpeed = control.TerminalVelocity;

			if (contact != null)
				animate.SetBool ("Jumping", true);

			if (charController.isGrounded) {
				if (Vector3.Dot (movement, contact.normal) < 0)
					movement = contact.normal * moveSpeed;
				else
					movement += contact.normal * moveSpeed;
			}

		}

		movement.y = vertSpeed;

		movement *= Time.deltaTime;
		charController.Move (movement);
	}

	void OnControllerColliderHit(ControllerColliderHit hit){

		contact = hit;
	}
}
