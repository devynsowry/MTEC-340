using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public float animationSpeed = 1f;

    private void Awake()
    {
        // Search for MeshRenderer
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // Update background using deltaTime
        meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    }
}
