using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfManager : MonoBehaviour
{
	public Transform playerTransform;
	private bool isPlayerVisible = true;
	public List<Transform> nodeList = new List<Transform>();
	public List<GameObject> wolfList = new List<GameObject>();

	public bool IsPlayerVisible
	{
		get { return isPlayerVisible; }
		set { isPlayerVisible = value; }
	}

	// checks the closest node to the location given and sets it as the assignment
	public void ChangeAssignment(ref Transform assignment, Vector3 location)
	{
		foreach(Transform node in nodeList)
		{
			float minDistance = Vector3.Distance(assignment.position, location);
			if (Vector3.Distance(node.position, location) < minDistance)
			{
				assignment = node;
			}
		}
	}
}
