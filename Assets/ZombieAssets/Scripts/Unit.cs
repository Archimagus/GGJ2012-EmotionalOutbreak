using UnityEngine;
using System.Collections;

public class Unit : Entity
{
	public float noiseMagnitude = 1.0f;
	public float focus = 0.99f;
	public Vector3 influence = Vector3.zero;

	public float healthMin = 10;
	public float healthMax = 20;
	public int attack = 1;
	public float range = 1;
	public float RunSpeed = 30.0f;
	Unit target;

	public float health = 10;
	public float Health
	{
		get { return health; }
		set { health = value; }
	}
	
	// Use this for initialization
	public virtual void Start() 
	{
		Health = Random.Range(healthMin, healthMax);
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
		
	}

	public virtual void DoDamage(float damage)
	{
		Health -= damage * Time.deltaTime;
	}
	
}
