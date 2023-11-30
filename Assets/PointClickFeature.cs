using System.Collections;
using System.Collections.Generic;
using ThirdPersonController.Core;
using ThirdPersonController.Core.DI;
using ThirdPersonController.MovementStateMachine.Features.Move;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PointClickFeature : BaseMoveFeature
{
    private Camera m_Camera;
    private NavMeshAgent m_NavMeshAgent;
    public override void CacheReferences(IStateMachineVariables variables, IReferenceResolver resolver)
    {
        base.CacheReferences(variables,resolver);
        m_Camera = resolver.GetCamera().GetComponent<Camera>();
        m_NavMeshAgent = resolver.GetComponent<NavMeshAgent>();
    }

    public override void OnUpdateState()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (Physics.Raycast(m_Camera.ScreenPointToRay(Mouse.current.position.value),out  var hit))
            {
                m_NavMeshAgent.SetDestination(hit.point);
            }
        }
    }
  
    public override void OnFixedUpdateState()
    {
        if (m_NavMeshAgent.isStopped) return;
      
        float dt = Time.fixedDeltaTime;


        var velocity = m_NavMeshAgent.velocity.normalized;
        var moveDirection = new Vector2(velocity.x,velocity.z);
        // update position
        
        SetControllerMoveSpeed(Variables.MovementSmooth, in dt);
        MoveCharacter(Variables.IsSlopeBadForMove, moveDirection);

        // update rotation
        
        if (moveDirection != Vector2.zero) 
            RotateToDirection(moveDirection, in dt);
        
        //update animation
        UpdateAnimation(Variables.IsSlopeBadForMove,moveDirection.magnitude);
    }
    
    public override void SetControllerMoveSpeed(float movementSmooth, in float dt)
    {
        Variables.MoveSpeed = Mathf.Lerp( Variables.MoveSpeed, runningSpeed, movementSmooth * dt);
    }

}
