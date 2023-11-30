using System;
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
                m_NavMeshAgent.isStopped = false;
                m_NavMeshAgent.SetDestination(hit.point + hit.normal *0.01f);
            }
        }

        if(m_NavMeshAgent.isStopped) return;
        var dest = m_NavMeshAgent.destination;
        var pos = _transform.position;
        if (Vector2.Distance(new Vector2(dest.x, dest.z), new Vector2(pos.x, pos.z)) < m_NavMeshAgent.stoppingDistance)
        {
            m_NavMeshAgent.isStopped = true;
        }
    }
  
    public override void OnFixedUpdateState()
    {
        float dt = Time.fixedDeltaTime;


        var velocity = m_NavMeshAgent.velocity.normalized;
        var moveInput = new Vector2(velocity.x,velocity.z);

        //just for ignoring stupid logs
        var lookRotation = m_NavMeshAgent.isStopped ? Quaternion.identity
            : Quaternion.LookRotation(_transform.position - m_NavMeshAgent.destination);
        
        var direction =  lookRotation * new Vector3(moveInput.x,0,moveInput.y);
        // update position
        
        SetControllerMoveSpeed(Variables.MovementSmooth, in dt);
        MoveCharacter(Variables.IsSlopeBadForMove, direction);

        // update rotation

        if (moveInput != Vector2.zero && !m_NavMeshAgent.isStopped) 
            RotateToDirection(direction, in dt);
        
        //update animation
        UpdateAnimation(Variables.IsSlopeBadForMove,moveInput.magnitude);
    }
    
    public override void SetControllerMoveSpeed(float movementSmooth, in float dt)
    {
        Variables.MoveSpeed = Mathf.Lerp( Variables.MoveSpeed, runningSpeed, movementSmooth * dt);
    }

}