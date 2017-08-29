using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
	Patrol,
	Pursue,
	Change
}

public class EnemyAI : MonoBehaviour
{
	private NavMeshAgent navComponent;
	private WolfManager wolfManager;

	private Vector3 lastKnownLocation;
	private bool locationKnown = false;         // if the last player location is known

	private Vector3 target;
	private float playerDistance;
	public Transform assignmentArea;

	public float viewDistance = 7;
	public float sideViewDistance = 5;
	public float areaRange = 10;
	private bool playerIsVisible = false;       // whether or not the player is currently visible to this wolf

	public float walkingSpeed = 1;
	public float chasingSpeed = 5;

	EnemyState currentState;

	void Start()
	{
		navComponent = GetComponent<NavMeshAgent>();
		wolfManager = GameObject.FindGameObjectWithTag("WolfManager").GetComponent<WolfManager>();
		MoveToRandom(areaRange, assignmentArea.position);
	}

	void FixedUpdate()
	{
		DetectPlayer();

		// start of AI decision tree
		StartDecisionTree();
	}

	private void StartDecisionTree()
	{
		if (playerIsVisible)
		{
			currentState = EnemyState.Pursue;
		}
		else
		{
			if (locationKnown)
			{
				currentState = EnemyState.Change;
			}
			else
			{
				currentState = EnemyState.Patrol;
			}
		}
		ProcessState();
	}

	// tells the wolf what to do based on the current state
	private void ProcessState()
	{
		switch (currentState)
		{
			case EnemyState.Pursue:
				if (playerIsVisible)
				{
					navComponent.SetDestination(target);
				}
				break;
			case EnemyState.Patrol:
				if (!playerIsVisible)
				{
					Patrol(areaRange, assignmentArea.position);
				}
				break;
			case EnemyState.Change:
				Debug.Log("Changing assignment");
				locationKnown = false;
				wolfManager.ChangeAssignment(ref assignmentArea, lastKnownLocation);
				Patrol(areaRange, lastKnownLocation);
				break;
		}
	}

	// patrols a given area within the given range
	private void Patrol(float range, Vector3 area)
	{
	    if (!navComponent.pathPending && navComponent.remainingDistance < navComponent.stoppingDistance)
		{
			MoveToRandom(range, area);
		}
	}

	// moves to a random point on the navmesh, within a given range
	private void MoveToRandom(float range, Vector3 area)
	{
		Vector3 randomPoint = Random.insideUnitSphere * range;
		randomPoint = new Vector3(area.x + randomPoint.x, transform.position.y, area.z + randomPoint.y);

		NavMeshHit navHit;
		NavMesh.SamplePosition(randomPoint, out navHit, range, -1);

		target = navHit.position;
		navComponent.velocity = Vector3.zero;
		navComponent.SetDestination(target);
	}
	
	// casts rays and detects if the player has been spotted
	private void DetectPlayer()
	{
		// only draw rays if the player is within a reasonable range
		playerDistance = Vector3.Distance(wolfManager.playerTransform.position, transform.position);

		// check if the player is in the visibility range and player is not hidden
		if (playerDistance <= viewDistance && wolfManager.IsPlayerVisible)
		{
			target = wolfManager.playerTransform.position;
			RaycastHit hit;

			// ray debugging
			Vector3 forward = transform.TransformDirection(Vector3.forward) * viewDistance;
			Vector3 right = transform.TransformDirection(Vector3.forward) * sideViewDistance;
			Vector3 left = transform.TransformDirection(Vector3.forward) * sideViewDistance;
			right = Quaternion.Euler(0, 45, 0) * right;
			left = Quaternion.Euler(0, -45, 0) * left;
			Debug.DrawRay(transform.position, forward, Color.green);
			Debug.DrawRay(transform.position, right, Color.magenta);
			Debug.DrawRay(transform.position, left, Color.magenta);

			// actual ray casting, see if a feeler has hit the player
			if (Physics.Raycast(transform.position, forward, out hit) ||
			Physics.Raycast(transform.position, right, out hit) ||
			Physics.Raycast(transform.position, left, out hit))
			{
				if (hit.collider.gameObject.tag == "Player")
				{
					playerIsVisible = true;
					navComponent.speed = chasingSpeed;          
					
					// record the location of the player
					locationKnown = true;
					lastKnownLocation = wolfManager.playerTransform.position;
				}
			}
		}
		// if the player has left the visibility range then stop pursuing and remember where player was
		else
		{
			playerIsVisible = false;
			navComponent.speed = walkingSpeed;
		}
	}
}
