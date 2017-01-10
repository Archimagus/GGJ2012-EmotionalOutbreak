using UnityEngine;
using System.Collections;

public class Civilian : NPC
{

	public float runDistance = 100;
	public float baseRunSpeed;
	private bool isColliding = false;
	private bool alive = true;
	Vector3 currentVelocity = Vector3.zero;
	Vector3 zombieLocation = Vector3.zero;
	GameObject terror = null;

	private void OnTriggerStart(Collider t)
	{
		if (t.collider.GetComponent<Zombie>() != null)
		{
			return;
		}

		terror = t.collider.GetComponent<Zombie>().gameObject;
		zombieLocation = terror.transform.position;
	}

	private void OnCollisionEnter(Collision c)
	{
		isColliding = true;
		var aggressor = c.collider.GetComponent<Zombie>();
		if (aggressor != null)
		{
			Vector3 position = transform.position;
			Instantiate(Map.Instance.ZombieTemplate, position, Quaternion.identity);
			Map.Instance.aliveZombies++;

			Kill();
		}
		//else
		//{
		//    var building = c.collider.GetComponent<Building>();
		//    if (building != null)
		//    {
		//        building.captureReward++;
		//        Kill();
		//    }
		//}
	}

	private void OnCollisionExit(Collision c)
	{
		isColliding = false;
		if (!collider.enabled)
		{
			collider.enabled = true;
		}
	}

	// Use this for initialization
	public override void Start()
	{
		base.Start();
		Map.Instance.aliveCivilians++;
		baseRunSpeed = RunSpeed;
		if (isColliding)
		{
			collider.enabled = false;
		}

	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();

		var distanceZom = Vector3.Distance(zombieLocation, transform.position);
		if (distanceZom > 50)
		{
			terror = null;
		}

		RunSpeed = baseRunSpeed / 2;
		var randdirection = new Vector3(Random.value - 0.5f, 0, Random.value - 0.5f);
		var runDirection = randdirection;
		if (Vector3.Distance(transform.position, Map.Instance.Cursor.transform.position) < runDistance + (Map.Instance.aliveZombies / 10))
		{
			runDirection = (transform.position - Map.Instance.Cursor.transform.position) + randdirection;
			runDirection.y = 0;
			RunSpeed = baseRunSpeed;
		}
		if (terror)
		{
			runDirection = (transform.position - zombieLocation) + randdirection;
			runDirection.y = 0;
			RunSpeed = baseRunSpeed;
		}

		runDirection.Normalize();

		rigidbody.velocity = Vector3.RotateTowards(rigidbody.velocity, runDirection, 1, 1);
		Vector3 normalizedVelocity = rigidbody.velocity.normalized;
		rigidbody.velocity = normalizedVelocity * RunSpeed;
		transform.forward = Vector3.SmoothDamp(transform.forward, normalizedVelocity, ref currentVelocity, 0.1f);
	}
	public static void Spawn(Bounds bounds)
	{
		Vector3 position = new Vector3(Random.Range(bounds.min.x, bounds.max.x), 0, Random.Range(bounds.min.z, bounds.max.z));
		Instantiate(Map.Instance.CivilianTemplate, position, Quaternion.identity);
	}
	public static void Spawn()
	{
		var bounds = Map.Instance.renderer.bounds;
		Spawn(bounds);
	}

	public void Kill()
	{
		if (alive)
		{
			alive = false;
			Map.Instance.aliveCivilians--;
			if (Map.Instance.civiliansToRespawn > 0)
			{
				Map.Instance.civiliansToRespawn--;
				Spawn();
			}
			GameObject.Destroy(gameObject);
		}
	}
}
