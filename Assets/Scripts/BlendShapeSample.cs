using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapeSample : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer CubeSkinnedMeshRenderer;
    private Mesh _mesh;
    [SerializeField] private KeyCode blendShape0TriggerKey = KeyCode.A;
    [SerializeField] private KeyCode blendShape1TriggerKey = KeyCode.S;
    [SerializeField] private KeyCode blendShapeOriginalTriggerKey = KeyCode.D;
    
    void Start()
    {
        if (CubeSkinnedMeshRenderer == null) {
            CubeSkinnedMeshRenderer = this.GetComponent<SkinnedMeshRenderer>();
        }
        _mesh = CubeSkinnedMeshRenderer.sharedMesh;
    }
    
    void Update()
    {
        int shapeCount = _mesh.blendShapeCount;
        string[] blendShapeName = new string[shapeCount];
        int[] blendShapeIndex = new int[shapeCount];

        for (int i = 0; i < shapeCount; i++) {
            blendShapeName[i] = _mesh.GetBlendShapeName(i);
            blendShapeIndex[i] = _mesh.GetBlendShapeIndex(_mesh.GetBlendShapeName(i));
            Debug.Log(blendShapeName[i] + ": " + blendShapeIndex[i]);
        }

        if (Input.GetKeyDown(blendShape0TriggerKey)) {
            CubeSkinnedMeshRenderer.SetBlendShapeWeight(0, 100);
            if (CubeSkinnedMeshRenderer.GetBlendShapeWeight(1) != 0) {
                CubeSkinnedMeshRenderer.SetBlendShapeWeight(1, 0);
            }
        }

        if (Input.GetKeyDown(blendShape1TriggerKey)) {
            CubeSkinnedMeshRenderer.SetBlendShapeWeight(1, 100);
        }

        ResetBlendShapeToOriginalOnTrigger();
    }
    
    // TODO: Create function to reset blendshape
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
}
