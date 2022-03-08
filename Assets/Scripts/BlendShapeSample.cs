using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Advanced BlendShape coroutine example: https://github.com/spiderlili/URP-2020-Shaders-Library/blob/main/Assets/Scripts/CrackController.cs

public class BlendShapeSample : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer CubeSkinnedMeshRenderer;
    private Mesh _mesh;
    [SerializeField] private KeyCode blendShape0TriggerKey = KeyCode.A;
    [SerializeField] private KeyCode blendShape1TriggerKey = KeyCode.S;
    [SerializeField] private KeyCode blendShapeOriginalTriggerKey = KeyCode.D;
    [SerializeField] private KeyCode blendShape1CoroutineTriggerKey = KeyCode.X;
    public float CoroutineSpeed = 1.0f;
    
    void Start()
    {
        if (CubeSkinnedMeshRenderer == null) {
            CubeSkinnedMeshRenderer = this.GetComponent<SkinnedMeshRenderer>();
        }
        _mesh = CubeSkinnedMeshRenderer.sharedMesh;
    }
    
    void Update()
    {
        DebugBlendShapes();
        BlendShape0OnTrigger();
        BlendShape1OnTrigger();
        BlendShape01CoroutineOnTrigger();
        ResetBlendShapeToOriginalOnTrigger();
    }
    
    void ResetBlendShapeToOriginalOnTrigger()
    {
        int shapeCount = _mesh.blendShapeCount;
        string[] blendShapeName = new string[shapeCount];
        int[] blendShapeIndex = new int[shapeCount];
        
        if (Input.GetKeyDown(blendShapeOriginalTriggerKey)) {
            for (int i = 0; i < shapeCount; i++) {
                CubeSkinnedMeshRenderer.SetBlendShapeWeight(i, 0);
            }
        }
    }

    void DebugBlendShapes()
    {
        int shapeCount = _mesh.blendShapeCount;
        string[] blendShapeName = new string[shapeCount];
        int[] blendShapeIndex = new int[shapeCount];

        for (int i = 0; i < shapeCount; i++) {
            blendShapeName[i] = _mesh.GetBlendShapeName(i);
            blendShapeIndex[i] = _mesh.GetBlendShapeIndex(_mesh.GetBlendShapeName(i));
            Debug.Log(blendShapeName[i] + ": " + blendShapeIndex[i]);
        }
    }

    void BlendShape0OnTrigger()
    {
        if (Input.GetKeyDown(blendShape0TriggerKey)) {
            CubeSkinnedMeshRenderer.SetBlendShapeWeight(0, 100);
            if (CubeSkinnedMeshRenderer.GetBlendShapeWeight(1) != 0) {
                CubeSkinnedMeshRenderer.SetBlendShapeWeight(1, 0);
            }
        }
    }
    void BlendShape1OnTrigger()
    {
        if (Input.GetKeyDown(blendShape1TriggerKey)) {
            CubeSkinnedMeshRenderer.SetBlendShapeWeight(1, 100);
        }
    }

    
    void BlendShape01CoroutineOnTrigger()
    {
        if (Input.GetKeyDown(blendShape1CoroutineTriggerKey)) {
            StopAllCoroutines();
            StartCoroutine(BlendShape01Coroutine());
        }
    }
    
    IEnumerator BlendShape01Coroutine()
    {
        float lerp = 0;
        CubeSkinnedMeshRenderer.SetBlendShapeWeight(0, 0);
        CubeSkinnedMeshRenderer.SetBlendShapeWeight(1, 0);
        while (lerp < 1) {
            CubeSkinnedMeshRenderer.SetBlendShapeWeight(1, 100 * lerp);
            lerp += Time.deltaTime * CoroutineSpeed;
            Debug.Log("Lerp: " + lerp);
            yield return null;
        }
    }

    public void SetBlendShape(int index, float value)
    {
        CubeSkinnedMeshRenderer.SetBlendShapeWeight(index, 100 - value); // Blend shape is open when the value is 0 & closed when the value is 100
    }
}
