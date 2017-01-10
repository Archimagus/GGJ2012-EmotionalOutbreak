using UnityEngine;
using System.Collections;

public class NPC : Unit
{
    public Vector3 startPosition;
    public int updates, updatesToKillCheck = 150;
    public float killCheckDistance = 4;
    public bool killCheck = true;

	// Use this for initialization
	public override void Start() 
	{
		base.Start();
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	public override void Update ()
    {
        if (killCheck)
        {
            updates++;
            if (updates > updatesToKillCheck)
            {
                float d = (transform.position - startPosition).magnitude;
                if (d < killCheckDistance)
                {
					Relocate();
					return;
                }
                killCheck = false;
            }
        }	
	}
	private void Relocate()
	{
		var bounds = Map.Instance.renderer.bounds;
		Vector3 position = new Vector3(Random.Range(bounds.min.x, bounds.max.x), 0, Random.Range(bounds.min.z, bounds.max.z));
		transform.position = position;
		killCheck = true;
		updates = 0;
	}
}
