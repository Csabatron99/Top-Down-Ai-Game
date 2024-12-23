using UnityEngine;

namespace TopDown.CameraController{
    
    public class CameraController : MonoBehaviour
    {
        [SerializeField]private Transform playerTransform;
        [SerializeField]private float displacementMiltiplier = 0.15f;
        private float zPosition = -10;

        private void Update()
        {
            //Calculate mouse position in world coordinates then calculate displacement depending on difference between mouse and player pos.
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 cameraDisplacement = (mousePosition - playerTransform.position) * displacementMiltiplier;
            
            //Determine final camera position and assign it.
            Vector3 finalCameraPosition = playerTransform.position + cameraDisplacement;
            finalCameraPosition.z = zPosition;
            transform.position = finalCameraPosition;
        }
    }

}
