using UnityEngine;
using UnityEngine.AI;

public class AILense : MonoBehaviour
{
    public float radius = 5;
    public float minAngle = -35;
    public float maxAngle = 35;
    
    public float minRandomMoveDistance = 35;
    public float maxRandomMoveDistance = 35;
    
    private Transform m_Target;
    private NavMeshAgent m_Agent;

    private RaycastHit[] m_Results;
    private void Awake() => m_Agent = GetComponent<NavMeshAgent>();

    private void Update()
    {
        m_Target = null;
        //Physics.SphereCastNonAlloc not works on unity 2021.3...
        m_Results = Physics.SphereCastAll(new Ray(transform.position, transform.forward), radius, radius, LayerMask.GetMask("Default"));
        for (int i = 0; i < m_Results.Length; i++)
        {
            ref var raycastHit = ref m_Results[i];
            if(!raycastHit.collider.CompareTag("Player")) continue;
            
            float angle = Vector3.Angle(transform.position + transform.forward,raycastHit.transform.position);
            
            
            if (minAngle < angle && angle < maxAngle)
            {
                m_Target = raycastHit.transform;
                break;
            }
        }

        if (m_Target)
        {
            m_Agent.SetDestination(m_Target.position);
            m_Agent.stoppingDistance = 0;
        }
        else if(m_Agent.isStopped || m_Agent.remainingDistance <= m_Agent.stoppingDistance)
        {
            m_Agent.SetDestination(GetRandomPoint(transform.position,minRandomMoveDistance, maxRandomMoveDistance));
            m_Agent.stoppingDistance = 0.3f;

        }
    }
    
    private static Vector3 GetRandomPoint(Vector3 center, float minDistance, float maxDistance)
    {
        
        var randomPos = Random.insideUnitSphere * Random.Range(minDistance,maxDistance) + center;
        NavMesh.SamplePosition(randomPos, out var hit, maxDistance, NavMesh.AllAreas);

        return hit.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,radius);
    }
}