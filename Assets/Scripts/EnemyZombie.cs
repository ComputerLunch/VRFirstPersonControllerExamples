using UnityEngine;
using System.Collections;

public class EnemyZombie : MonoBehaviour {

	Transform target; //the enemy's target
	float moveSpeed = 125f; //move speed
	float rotationSpeed = 2; //speed of turning
	bool isAttack = true;
	Transform myTransform; //current transform data of this enemy
	CharacterController controller;

	Animator anim;

	void Awake()
	{
		myTransform = transform; //cache transform data for easy access/preformance
		controller = GetComponent<CharacterController>();

		anim = GetComponent<Animator>();
	}

	void Start()
	{
		target = GameObject.FindWithTag("Player").transform; //target the player

	}

	// Update is called once per frame
	void Update () {

		if( Vector3.Distance(target.position ,myTransform.position) < 3.5){

			if(isAttack == true){
				anim.Play("atack01");
				isAttack = false;
			}


		} else if( Vector3.Distance(target.position ,myTransform.position) < 10){

			isAttack = true;
			anim.Play("run02");

		}else{

			isAttack = false;
			anim.Play("idle01");
		}


		if(isAttack){

			// find the target direction:
			Vector3 dir = target.position - myTransform.position;
			dir.y = 0; // ignore height differences to avoid enemy tilting
			//rotate to look at the player
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(dir), rotationSpeed*Time.deltaTime);
			//move towards the player:
			//controller.SimpleMove(myTransform.forward * moveSpeed);

		//	myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
			controller.SimpleMove( myTransform.forward * moveSpeed * Time.deltaTime);
		}
	}
}
