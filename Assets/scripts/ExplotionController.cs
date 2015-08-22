using UnityEngine;
using System.Collections;

// ExplotionController

// He controls explosions. He controls fire.
// Fire is a dangerous thing, but we know he will use it wisely
// We are all mad around here

public class ExplotionController : MonoBehaviour {

	public Animator animator;
	
	public void explode () {
		animator.Play (0);
	}
}
