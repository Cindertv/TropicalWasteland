using UnityEngine;

public class Waypoints : MonoBehaviour {
    /// <summary>
    /// Gets the refrence to the waypoints in the game scene and let the enemies follow the pre defined path layed out
    /// </summary>
	public static Transform[] wayPoints;

	void Awake ()
	{
		wayPoints = new Transform[transform.childCount];
		for (int i = 0; i < wayPoints.Length; i++)
		{
			wayPoints[i] = transform.GetChild(i);
		}
	}

}
