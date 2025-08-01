using UnityEngine;

namespace Level
{
    [RequireComponent(typeof(Transform))]
    public class ParallaxLayer : MonoBehaviour
    {
        public float parallaxFactor = 0.5f; 

        private Transform cam;
        private Vector3 lastCamPos;

        private void Start()
        {
            cam = Camera.main.transform; 
            lastCamPos = cam.position;
        }

        private void LateUpdate()
        {
            Vector3 delta = cam.position - lastCamPos;
            transform.position += new Vector3(delta.x * parallaxFactor, delta.y * parallaxFactor, 0);
            lastCamPos = cam.position;
        }
    }
}
