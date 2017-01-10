using UnityEngine;
using System.Collections;

public class Cursor : Unit
{
    public float RunSpeedMax = 120;
    
    // Use this for initialization
	public override void Start()
	{
		base.Start();
	}

	// Update is called once per frame
	public override void Update()
	{
		float testRunSpeed;
		if (!Map.Instance.gameOver)
		{
			testRunSpeed = RunSpeed + (Map.Instance.aliveZombies / 10);
            if (testRunSpeed > RunSpeedMax)
            {
                testRunSpeed = RunSpeedMax;
            }
			Vector3 dir = Vector3.zero;
			dir.z += Input.GetAxis("Vertical");
			dir.x += Input.GetAxis("Horizontal");
			var pos = Vector3.MoveTowards(transform.position, transform.position + dir.normalized * testRunSpeed * Time.deltaTime, testRunSpeed * Time.deltaTime);
			var mapBoudns = Map.Instance.renderer.bounds;
			if (pos.x < mapBoudns.min.x)
			{
				pos.x = mapBoudns.min.x;
			}
			if (pos.x > mapBoudns.max.x)
			{
				pos.x = mapBoudns.max.x;
			}
			if (pos.z < mapBoudns.min.z)
			{
				pos.z = mapBoudns.min.z;
			}
			if (pos.z > mapBoudns.max.z)
			{
				pos.z = mapBoudns.max.z;
			}
			transform.position = pos;

            if (Map.Instance.aliveZombies < 20)
            {
                collider.enabled = true;
            }
            else
            {
                collider.enabled = false;
            }
		}
	}
}
