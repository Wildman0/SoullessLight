using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProfiler : MonoBehaviour {
    
    //Profiler objects
    MemoryProfiler memoryProfiler = new MemoryProfiler();
    ProcessorProfiler processorProfiler = new ProcessorProfiler();

    //Profiler values
    private string[] memoryProfilerValues = new string[6];
    private string[] processorProfilerValues = new string[2];

    //Profiler default titles
    private readonly string[] memoryProfilerTextTitles =
    {
        "Used Heap Size (MB)",
        "Allocated Graphics Memory (MB)",
        "Mono Heap Size (MB)",
        "Mono Used Size (MB)",
        "Total Allocated Memory (MB)",
        "Total Reserved Memory (MB)"
    };

    private readonly string[] processorProfilerTextTitles =
    {
        "Last Frame Time (ms)",
        "Frames Per Second"
    };

    //UI objects
    public Text[] memoryProfilerTexts;
    public Text[] processorProfilerTexts;

    private const int megabyteDivider = 1048576;

    void Update()
    {
        memoryProfiler.Update();
        processorProfiler.Update();

        SetMemoryProfileValues();
        SetProcessorProfileValues();

        UpdateMemoryProfilerText();
        UpdateProcessorProfilerText();
    }

    void UpdateMemoryProfilerText()
    {
        for (int i = 0; i < memoryProfilerTexts.Length; i++)
        {
            memoryProfilerTexts[i].text = memoryProfilerTextTitles[i]
                                          + " :" + memoryProfilerValues[i];
        }
    }

    void UpdateProcessorProfilerText()
    {
        for (int i = 0; i < processorProfilerTexts.Length; i++)
        {
            processorProfilerTexts[i].text = processorProfilerTextTitles[i]
                                          + " :" + processorProfilerValues[i];
        }
    }

    void SetMemoryProfileValues()
    {
        memoryProfilerValues[0] = (memoryProfiler.usedHeapSize / megabyteDivider).ToString();
        memoryProfilerValues[1] = (memoryProfiler.allocatedGraphicsMemory / megabyteDivider).ToString();
        memoryProfilerValues[2] = (memoryProfiler.monoHeapSize / megabyteDivider).ToString();
        memoryProfilerValues[3] = (memoryProfiler.monoUsedSize / megabyteDivider).ToString();
        memoryProfilerValues[4] = (memoryProfiler.totalAllocatedMemory / megabyteDivider).ToString();
        memoryProfilerValues[5] = (memoryProfiler.totalReservedMemory / megabyteDivider).ToString();
    }

    void SetProcessorProfileValues()
    {
        processorProfilerValues[0] = processorProfiler.lastFrameTime.ToString();
        processorProfilerValues[1] = processorProfiler.framesPerSecond.ToString();
    }
}
