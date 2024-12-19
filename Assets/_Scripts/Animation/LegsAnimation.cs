using TopDown.Movement;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class LegsAnimation : MonoBehaviour
{
    [SerializeField]private Mover playerMover;
    private Animator legsAnimator;
    
    
    private void Awake()
    {
        legsAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        legsAnimator.SetBool("moving", playerMover.CurrentInput != Vector3.zero);
    }

}

