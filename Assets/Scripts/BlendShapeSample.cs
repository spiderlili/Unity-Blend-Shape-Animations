using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapeSample : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer CubeSkinnedMeshRenderer;
    private Mesh _mesh;
    [SerializeField] private KeyCode blendShapeKey0 = KeyCode.A;
    [SerializeField] private KeyCode blendShapeKey1 = KeyCode.S;
    
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

        if (Input.GetKeyDown(blendShapeKey0)) {
            CubeSkinnedMeshRenderer.SetBlendShapeWeight(0, 100);
            if (CubeSkinnedMeshRenderer.GetBlendShapeWeight(1) != 0) {
                CubeSkinnedMeshRenderer.SetBlendShapeWeight(1, 0);
            }
        }

        if (Input.GetKeyDown(blendShapeKey1)) {
            CubeSkinnedMeshRenderer.SetBlendShapeWeight(1, 100);
        }
    }
}
