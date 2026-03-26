using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layerTransform;
        [Range(0f, 1f)] public float parallaxFactor;
    }

    public ParallaxLayer[] layers;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Vector3 lastCameraPosition;

    void Start()
    {
        lastCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        Vector3 cameraDelta = cameraTransform.position - lastCameraPosition;

        foreach(ParallaxLayer layer in layers)
        {
            float parallaxX = cameraDelta.x * layer.parallaxFactor;
            float parallaxY = cameraDelta.y * layer.parallaxFactor;

            layer.layerTransform.position += new Vector3(parallaxX, parallaxY, 0);
        }
        lastCameraPosition = cameraTransform.position;
    }
}