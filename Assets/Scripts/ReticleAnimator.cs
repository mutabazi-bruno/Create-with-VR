using UnityEngine;

// Makes a teleport reticle spin and pulse (scale up and down).
// The XR line visual sets this object's position and rotation every frame, so the animation
// runs on a child copy of the mesh instead of on this object itself.
public class ReticleAnimator : MonoBehaviour
{
    public float spinSpeed = 90f;      // degrees per second
    public float pulseSpeed = 4f;      // how fast it grows and shrinks
    public float pulseAmount = 0.15f;  // 0.15 = grows and shrinks by 15%

    private Transform visual;
    private Vector3 spinAxis;

    void Awake()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshFilter == null || meshRenderer == null)
        {
            enabled = false;
            return;
        }

        // Copy the reticle mesh onto a child and hide the original
        GameObject child = new GameObject("Animated Visual");
        child.layer = gameObject.layer;
        child.transform.SetParent(transform, false);
        child.AddComponent<MeshFilter>().sharedMesh = meshFilter.sharedMesh;
        MeshRenderer childRenderer = child.AddComponent<MeshRenderer>();
        childRenderer.sharedMaterials = meshRenderer.sharedMaterials;
        childRenderer.shadowCastingMode = meshRenderer.shadowCastingMode;
        childRenderer.receiveShadows = meshRenderer.receiveShadows;
        meshRenderer.enabled = false;
        visual = child.transform;

        // Spin around the axis the flat reticle is thinnest along (the axis pointing out of it)
        Vector3 size = meshFilter.sharedMesh.bounds.size;
        if (size.x <= size.y && size.x <= size.z) spinAxis = Vector3.right;
        else if (size.y <= size.z) spinAxis = Vector3.up;
        else spinAxis = Vector3.forward;
    }

    void Update()
    {
        visual.localRotation = Quaternion.AngleAxis(Time.time * spinSpeed, spinAxis);
        visual.localScale = Vector3.one * (1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount);
    }
}
