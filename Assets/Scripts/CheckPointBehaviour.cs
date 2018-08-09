using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBehaviour : MonoBehaviour {

	public float checkPointX, checkPointY, checkPointZ; // Where the player's new checkpoint will be

	// Returns a vector associated with the checkpoint
	public Vector3 getCheckPointData () {
		return new Vector3 (checkPointX,checkPointY,checkPointZ);
	}
}