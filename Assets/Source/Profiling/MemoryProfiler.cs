using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Profiling;

public class MemoryProfiler
{
    public ulong usedHeapSize;
    public ulong allocatedGraphicsMemory;
    public ulong monoHeapSize;
    public ulong monoUsedSize;
    public ulong totalAllocatedMemory;
    public ulong totalReservedMemory;

    // Update is called once per frame
    public void Update ()
	{
	    SetUsedHeapSize();
        SetAllocatedGraphicsMemory();
        SetMonoHeapSize();
        SetMonoUsedSize();
        SetTotalAllocatedMemory();
	    SetTotalReservedMemory();
	}

    void SetUsedHeapSize()
    {
        usedHeapSize = (ulong) Profiler.usedHeapSizeLong;
        
    }

    void SetAllocatedGraphicsMemory()
    {
        allocatedGraphicsMemory = (ulong) Profiler.GetAllocatedMemoryForGraphicsDriver();
    }

    void SetMonoHeapSize()
    {
        monoHeapSize = (ulong) Profiler.GetMonoHeapSizeLong();
    }

    void SetMonoUsedSize()
    {
        monoUsedSize = (ulong) Profiler.GetMonoUsedSizeLong();
    }

    void SetTotalAllocatedMemory()
    {
        totalAllocatedMemory = (ulong) Profiler.GetTotalAllocatedMemoryLong();
    }

    void SetTotalReservedMemory()
    {
        totalReservedMemory = (ulong) Profiler.GetTotalReservedMemoryLong();
    }
}
