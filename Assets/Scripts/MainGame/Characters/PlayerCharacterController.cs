using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerCharacterController : MonoBehaviour
{
    static readonly int SpeedPropertyID = Animator.StringToHash("Speed");
    [SerializeField] UnityEvent<int> onTakeDamageEvent;
    [SerializeField] Camera playerCamera;
    [SerializeField] Animator animator;

    [Header("Navigation")]
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Transform waypoint;
    [SerializeField] Transform[] pathWaypoints;

    readonly bool hasBloodyBoots = true;
    bool isMoving = true;
    int startingHp;

    public int Hp { get; set; }
    public int CurrentWaypointIndex { get; set; }

    public event UnityAction<int> onTakeDamageEventAction;

    public void ToggleMoving(bool shouldMove)
    {
        isMoving = shouldMove;
        if (navMeshAgent) navMeshAgent.enabled = shouldMove;
    }

    public void SetDestination(Transform targetTransformWaypoint)
    {
        if (navMeshAgent)
            navMeshAgent.SetDestination(targetTransformWaypoint.position);
    }

    public void SetDestination(int waypointIndex)
    {
        SetDestination(pathWaypoints[waypointIndex]);
    }

    public void TakeDamage(int damageAmount)
    {
        Hp -= damageAmount;
        var hpPercentLeft = (float)Hp / startingHp;
        animator.SetLayerWeight(1, 1 - hpPercentLeft);
        onTakeDamageEvent.Invoke(Hp);
        onTakeDamageEventAction?.Invoke(Hp);
    }

    void Start()
    {
        Hp = 100;
        startingHp = Hp;
        SetMudAreaCost();
        ToggleMoving(true);
        SetDestination(pathWaypoints[0]);

        if (playerCamera)
        {
            StartCoroutine(TrackPlayerCamera());
        }
    }

    IEnumerator TrackPlayerCamera()
    {
        var waitFor100MS = new WaitForSeconds(0.1f);
        while (true)
        {
            //We want to know what the mouse is hovering now
            var ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out var hit, 100f))
                Debug.Log($"Hit: {hit.collider.name}");

            yield return waitFor100MS;
        }
    }

    void SetMudAreaCost()
    {
        if (hasBloodyBoots) navMeshAgent.SetAreaCost(3, 1);
    }

    [ContextMenu("Take Damage Test")]
    void TakeDamageTesting()
    {
        TakeDamage(10);
    }

    void Update()
    {
        if (isMoving && !navMeshAgent.isStopped && navMeshAgent.remainingDistance <= 0.1f)
        {
            CurrentWaypointIndex++;
            if (CurrentWaypointIndex >= pathWaypoints.Length)
                CurrentWaypointIndex = 0;

            SetDestination(pathWaypoints[CurrentWaypointIndex]);
        }

        if (animator)
            animator.SetFloat(SpeedPropertyID, navMeshAgent.velocity.magnitude);
    }
}