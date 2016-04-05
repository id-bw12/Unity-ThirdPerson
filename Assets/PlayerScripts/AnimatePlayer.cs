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
	
		//float move = Input.GetAxis ("Vertical");
		//animate.SetFloat("Speed", move);
	}

    public void SelectingAnimatation(Vector3 movement) {

        if (movement.x != 0.0f)
        {

            animate.SetFloat("Speed", 1.0f);

            if (movement.x < 0.0f)
            {
                animate.SetFloat("Forward", 0.4f);
                Debug.Log("good");
            }
            else
                if (movement.x > 0.0f)
                animate.SetFloat("Forward", 0.9f);

        }
        else
            if (movement.x == 0 && movement.z != 0){
            animate.SetFloat("Speed",1.0f);
            animate.SetFloat("Forward", 0.0f);
        }

        if (movement.magnitude == 0)
            animate.SetFloat("Speed", 0.0f);
       
    }
}
