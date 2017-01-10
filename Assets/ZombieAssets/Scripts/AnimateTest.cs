using UnityEngine;
using System.Collections;

public class AnimateTest : MonoBehaviour {

    public Texture[] animatedFramesIdle;
    public Texture[] animatedFramesForward;
    public double frameSpeed = 0.5;
    public double lungeThreshold = 1;
    private float lastFrame = 0;
    private int frameCount = 0;

    // Use this for initialization
    void Start()
    {
        frameCount = Random.Range(0, animatedFramesIdle.Length);
        this.renderer.material.mainTexture = animatedFramesIdle[frameCount];
    }

    void Update()
    {
        var curVel = rigidbody.velocity;
        float zomSpeed = curVel.magnitude;

        if (Time.realtimeSinceStartup - lastFrame > frameSpeed)
        {
            frameCount++;
            lastFrame = Time.realtimeSinceStartup;
            if (frameCount >= animatedFramesIdle.Length)
            {
                frameCount = 0;
            }
            if (zomSpeed > lungeThreshold)
            {
                this.renderer.material.mainTexture = animatedFramesForward[frameCount];
            } else {
                this.renderer.material.mainTexture = animatedFramesIdle[frameCount];
            }
        }
    }

}
