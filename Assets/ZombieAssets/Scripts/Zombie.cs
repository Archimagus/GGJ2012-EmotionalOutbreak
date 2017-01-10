using UnityEngine;
using System.Collections;

public class Zombie : Unit
{
	public float zombieAttraction = 100.0f;
	public int atrophyRate = 10;

	public AudioClip[] Screams;
	public AudioClip[] BodyAttacks;
	public AudioClip[] HitSounds;

	public static float lastScream = 0;

	public Texture[] deadZombieTextures;
	public AudioClip[] DeathSounds;
	public static float lastDeathScream = 0;

	private int frameCount;
	private float maxInfluenceStrength;
	public GameObject deadZombieTemplate;
	private Vector3 currentVelocity;
	private int minRad = 3;
	private int maxRad = 5;
	private float baseRad;
	private float baseRunSpeed;
	public float nerfDistance;
	private float nerfDistanceSquare;
	public float cursorTolerance;
	private float cursorToleranceSquare;
	Vector3 civLocation = Vector3.zero;
	Civilian lover = null;
	private GameObject map;
	private Texture2D mapTex;

	private void OnTriggerEnter(Collider t)
	{
		if (!t.CompareTag("Civilian"))
		{
			return;
		}

		lover = t.GetComponent<Civilian>();
		civLocation = lover.transform.position;
	}

	// Use this for initialization
	public override void Start()
	{
		base.Start();

		baseRunSpeed = RunSpeed;
		map = Map.Instance.gameObject;
		mapTex = map.renderer.sharedMaterial.mainTexture as Texture2D;

		var sphereCollide = GetComponent<SphereCollider>();
		baseRad = Random.Range(minRad, maxRad);
		sphereCollide.radius = baseRad;
		nerfDistanceSquare = nerfDistance * nerfDistance;
		cursorToleranceSquare = cursorTolerance * cursorTolerance;
		if (Time.realtimeSinceStartup - lastScream > 3)
		{
			lastScream = Time.realtimeSinceStartup;

			int index = Random.Range(0, Screams.Length);
			audio.volume = 0.1f;
			audio.PlayOneShot(Screams[index]);

			index = Random.Range(0, BodyAttacks.Length);
			audio.volume = 0.2f;
			audio.PlayOneShot(BodyAttacks[index]);

			index = Random.Range(0, HitSounds.Length);
			audio.volume = 0.2f;
			audio.PlayOneShot(HitSounds[index]);
		}
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();

		if (Vector3.SqrMagnitude(transform.position-Map.Instance.Cursor.transform.position) < nerfDistanceSquare)
		{
			RunSpeed = 0;
		}
		else
		{
			RunSpeed = baseRunSpeed;
		}

		var distanceCiv = Vector3.SqrMagnitude(civLocation-transform.position);
		var distanceCursor = Vector3.SqrMagnitude(Map.Instance.Cursor.transform.position-transform.position);

		if (distanceCursor > cursorToleranceSquare || distanceCiv > 100*100)
		{
			lover = null;
		}

		if (Health <= 0)
		{
			Splat();

			if (Time.realtimeSinceStartup - lastDeathScream > 3)
			{
				lastDeathScream = Time.realtimeSinceStartup;
				int index = Random.Range(0, DeathSounds.Length);
				audio.volume = 0.3f;
				audio.PlayOneShot(DeathSounds[index]);
			}

			collider.enabled = false;
			Map.Instance.aliveZombies--;
			GameObject.Destroy(gameObject);
			return;
		}

		var inf = Map.Instance.Cursor.transform.position;
		if (lover != null)
		{
			inf = civLocation;
		}
		inf.y = 0;
		AddInfluence(inf, zombieAttraction);
		maxInfluenceStrength *= focus;

		Vector3 normalizedVelocity = (influence - rigidbody.position).normalized;

		rigidbody.velocity = normalizedVelocity * RunSpeed;
		transform.forward = normalizedVelocity;

		Atrophy();
	}

	private void Splat()
	{
		var k = transform.position;
		k.y = -2;
		Texture2D dead = deadZombieTextures[Random.Range(0, deadZombieTextures.Length)] as Texture2D;
		var scale = map.transform.localScale;
		scale *= 10;
		var x = 1 - ((transform.position.x + scale.x / 2) / scale.x);
		var z = 1 - ((transform.position.z + scale.z / 2) / scale.z);
		int pixelX = (int)(mapTex.width * x);
		int pixelY = (int)(mapTex.height * z);
		for (int px = 0; px < dead.width; px++)
		{
			for (int py = 0; py < dead.height; py++)
			{
				var pix = dead.GetPixel(px, (dead.width - py) - 1);
				if (pix.r > 0.8f && pix.g > 0.8f && pix.b > 0.8f)
					continue;
				mapTex.SetPixel(pixelX + (px - (dead.width / 2)), pixelY + (py - (dead.height / 2)), pix);
			}

		}
		mapTex.Apply();
	}

	public void AddInfluence(Vector3 position, float strength)
	{
		if (strength > maxInfluenceStrength)
		{
			influence = position;
			maxInfluenceStrength = strength;
		}
	}

	private void Atrophy()
	{
		Health -= atrophyRate*Time.deltaTime;
	}
}
