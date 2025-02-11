using UnityEngine;
using UnityEngine.Animations;

public class FallAndRiseBehaviour : StateMachineBehaviour
{
    PlayerCharacterController playerCharacterController;

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineEnter(animator, stateMachinePathHash);
        
        if(!playerCharacterController)
            playerCharacterController = animator.GetComponent<PlayerCharacterController>();
        
        playerCharacterController.ToggleMoving(false);
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineExit(animator, stateMachinePathHash);
        
        playerCharacterController.ToggleMoving(true);
    }
}
