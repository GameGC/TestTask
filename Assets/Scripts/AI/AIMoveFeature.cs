using ThirdPersonController.Core.DI;
using ThirdPersonController.MovementStateMachine.Features.Move;
using UnityEngine;
using UnityEngine.AI;

public class AIMoveFeature : BaseMoveFeature
{
    private NavMeshAgent m_NavMeshAgent;
    public override void CacheReferences(IStateMachineVariables variables, IReferenceResolver resolver)
    {
        base.CacheReferences(variables,resolver);
        m_NavMeshAgent = resolver.GetComponent<NavMeshAgent>();
    }

    public override void OnFixedUpdateState()
    {
        if (m_NavMeshAgent.isStopped) return;
      
        float dt = Time.fixedDeltaTime;


        var velocity = m_NavMeshAgent.velocity.normalized;
        var moveInput = new Vector2(velocity.x,velocity.z);
        // update position
        
        SetControllerMoveSpeed(Variables.MovementSmooth, in dt);

        // update rotation
        
        if (moveInput != Vector2.zero && !m_NavMeshAgent.isStopped)
        {
            var lookRotation = Quaternion.LookRotation(_transform.position - m_NavMeshAgent.destination);
            
            var direction =  lookRotation * new Vector3(moveInput.x,0,moveInput.y);
            RotateToDirection(direction, in dt);
        }
        
        //update animation
        UpdateAnimation(Variables.IsSlopeBadForMove,moveInput.magnitude);
    }
    
    public override void SetControllerMoveSpeed(float movementSmooth, in float dt)
    {
        Variables.MoveSpeed = Mathf.Lerp( Variables.MoveSpeed, runningSpeed, movementSmooth * dt);
    }

}