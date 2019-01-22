using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Profiling;

public class ProcessorProfiler
{
    public float lastFrameTime;
    public float framesPerSecond;

	// Update is called once per frame
	public void Update ()
	{
	    SetLastFrameTime();
	    SetFramesPerSecond();
	}

    void SetLastFrameTime()
    {
        lastFrameTime = Time.deltaTime;
    }

    void SetFramesPerSecond()
    {
        framesPerSecond = Mathf.Round(1.0f / lastFrameTime);
    }
}
