
using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDown.Movement{

    public class PlayerRotation : Rotator
    {
        [Header("Torso & Legs")]
        [SerializeField] private Transform torso;
        [SerializeField] private Transform legs;
        [SerializeField] private Transform weapon;

        [Header("Mover Reference")]
        [SerializeField] private Mover playerMover;

        private Vector3 initialWeaponOffset; // Offset between the torso center and weapon tip
        private void Start()
        {
            // Calculate the initial offset of the weapon from the torso center
            if (torso != null && weapon != null)
            {
                initialWeaponOffset = torso.InverseTransformPoint(weapon.position);
            }
        }

        //Determine mouse position and look that way
        private void OnLook(InputValue value)
        {
            Vector2 mousePosition =  Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
            LookAt(torso, mousePosition);

            // Sync Weapon's rotation with the Torso
            if (weapon != null)
            {
                // Update Weapon rotation
                weapon.rotation = torso.rotation;

                // Update Weapon position based on the torso's center and initial offset
                weapon.position = torso.TransformPoint(initialWeaponOffset);
            }
        }

        private void Update()
        {
            //Rotate legs to face the movement direction
            Vector3 legsLookPoint = transform.position + new Vector3(playerMover.CurrentInput.x, playerMover.CurrentInput.y, 0f);
            LookAt(legs, legsLookPoint);
        }
        
    }
}

