using UnityEngine;
using System.Collections;

public class Cop : NPC
{
	private void OnCollisionStay(Collision c)
	{
		var aggressor = c.collider.GetComponent<Zombie>();

		if (aggressor != null)
		{
			aggressor.Health -= attack*Time.deltaTime;
			Health -= aggressor.attack*Time.deltaTime;
		}
	}
	Vector3 currentVelocity = Vector3.zero;
	// Use this for initialization
	public override void Start()
	{
		base.Start();
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
		if (Health <= 0)
		{
			Kill();
		}

		var runDirection = (Map.Instance.Cursor.transform.position - transform.position);
		runDirection.y = 0;
		runDirection.Normalize();
		

		rigidbody.velocity = Vector3.RotateTowards(rigidbody.velocity, runDirection, 1, 1);
		Vector3 normalizedVelocity = rigidbody.velocity.normalized;
		rigidbody.velocity = normalizedVelocity * RunSpeed;
		transform.forward = Vector3.SmoothDamp(transform.forward, normalizedVelocity, ref currentVelocity, 0.2f);

	}
	public void Kill()
	{
		Map.Instance.aliveCops--;
		GameObject.Destroy(gameObject);
	}
	public static void Spawn()
	{
		var bounds = Map.Instance.renderer.bounds;
		Vector3 position = new Vector3(Random.Range(bounds.min.x, bounds.max.x), 0, Random.Range(bounds.min.z, bounds.max.z));
		Instantiate(Map.Instance.CopTemplate, position, Quaternion.identity);
		Map.Instance.aliveCops++;
	}
}
