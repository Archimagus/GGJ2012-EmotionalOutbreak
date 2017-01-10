using UnityEngine;
using System.Collections;

public class Building : Unit
{
	public Color capColor0;
	public Color capColor1;
	public Color capColor2;
	public Color capColor3;
	public Color capColor4;

    public Texture[] buildingStart;
    public AudioClip[] HitSounds;
    private bool liberated = false;
    public int captureReward = 10;

    public int collisionCount;
    public int minZombieCollisions;

    GameObject attacker = null;

	// Use this for initialization
	public override void Start () 
	{
        base.Start();

        this.renderer.material.mainTexture = buildingStart[Random.Range(0, buildingStart.Length)];
    }

    public override void Update()
    {
        if (liberated)
        {
            return;
        }

        if (attacker == null)
        {
            collisionCount = 0;
        }

        if (collisionCount >= minZombieCollisions){
            if (Health < 1)
            {
                int index = Random.Range(0, 5);
                Material mat = renderer.material;
                switch (index)
                {
                    case 0:
                        mat.color = capColor0;
                        break;
                    case 1:
                        mat.color = capColor1;
                        break;
                    case 2:
                        mat.color = capColor2;
                        break;
                    case 3:
                        mat.color = capColor3;
                        break;
                    case 4:
                        mat.color = capColor4;
                        break;
                    default:
                        mat.color = capColor0;
                        break;
                }

                for (int i = 0; i < captureReward; i++)
                {
					Bounds b = new Bounds(transform.position, new Vector3(5, 0, 5));
					Civilian.Spawn(b);
                }

                collisionCount = 0;
                liberated = true;
                collider.enabled = false;
                return;
            }
            else if (Mathf.RoundToInt(Health) % 25 == 0)
            {

				Bounds b = new Bounds(transform.position, new Vector3(5, 0, 5));
				Civilian.Spawn(b);
                Health -= collisionCount / minZombieCollisions;

            }
            else
            {
                Health -= collisionCount / minZombieCollisions;
            }

        
        }
    }

    void OnCollisionEnter(Collision c)
    {
        if (liberated)
        {
            return;
        }

        if (c.gameObject.GetComponent<Zombie>())
        {
            attacker = c.gameObject.GetComponent<Zombie>().gameObject;
            ++collisionCount;
        }

    }

    void OnCollisionExit(Collision c)
    {
        if (liberated)
        {
            return;
        }

        if (c.gameObject.GetComponent<Zombie>())
        {
            --collisionCount;
        }
    }
}
