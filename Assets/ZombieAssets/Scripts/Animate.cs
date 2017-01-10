using UnityEngine;
using System.Collections;

public class Animate : MonoBehaviour {

    public Texture[] animatedFramesIdle;
    public Texture[] animatedFramesForward;
    private int frameCount = 0;
    public double lungeThreshold = 1;
    private double baseLungeThreshold;
    private float lastFrame = 0;
    private float lastPosX = 0;
    private float lastPosZ = 0;
    private float curPosX;
    private float curPosZ;
    public double frameSpeed = 0.5;
    private bool lungeForward = false;
    private int count;

    // Use this for initialization
    void Start()
    {
        this.renderer.material.mainTexture = animatedFramesIdle[Random.Range(0, animatedFramesIdle.Length)];
        baseLungeThreshold = lungeThreshold;
        count = 0;
    }

    void Update()
    {
        //var curVel = rigidbody.velocity;
        //float zomSpeed = curVel.magnitude;
        curPosX = Mathf.Abs(rigidbody.position.x);
        curPosZ = Mathf.Abs(rigidbody.position.z);

        if (curPosX - lastPosX > lungeThreshold || curPosX - lastPosX < -lungeThreshold)
        {
            lungeForward = true;
            if (count == 0)
            {
                lungeThreshold = 0;
            }
            else
            {
                lungeThreshold = lungeThreshold + 0.01;
            }
            count++;
        }
        else if (curPosZ - lastPosZ > lungeThreshold || curPosZ - lastPosZ < -lungeThreshold)
        {
            lungeForward = true;
            if (count == 0)
            {
                lungeThreshold = 0;
            }
            else
            {
                lungeThreshold = lungeThreshold + 0.01;
            }
            count++;
        }
        else
        {
            lungeForward = false;
            lungeThreshold = baseLungeThreshold;
            count = 0;
        }

        lastPosX = curPosX;
        lastPosZ = curPosZ;

        if (Time.realtimeSinceStartup - lastFrame > frameSpeed)
        {
            frameCount++;
            if (frameCount >= animatedFramesForward.Length)
            {
                frameCount = 0;
            }
            lastFrame = Time.realtimeSinceStartup;

            if (lungeForward)
            {
                this.renderer.material.mainTexture = animatedFramesForward[frameCount];
            } 
			else
            {
                this.renderer.material.mainTexture = animatedFramesIdle[frameCount];
            }
        }
    }

}
