using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CodeMonkey.Utils;

public class RTSUnit : MonoBehaviour, IUnitDamageable {


    public event EventHandler OnIsSelectedChanged;
    public event EventHandler OnStartedMoving;
    public event EventHandler OnStoppedMoving;
    public event EventHandler OnDead;


    [SerializeField] private bool isEnemy;
    [SerializeField] private IUnitBehaviour unitBehaviour;
    [SerializeField] private int healthAmountMax;
    [SerializeField] private float attackDistanceOffset;

    private HealthSystem healthSystem;
    private World_Bar healthBar;
    private NavMeshAgent navMeshAgent;
    private bool isSelected;


    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();

        unitBehaviour = GetComponent<NormalUnitBehaviour>();

        healthSystem = new HealthSystem(healthAmountMax);
        healthBar = World_Bar.Create(transform, new Vector3(0, 2, 0), new Vector3(1, .2f), Color.grey, Color.red, 1f, 0, new World_Bar.Outline { color = Color.black, size = .1f });
        LookAtCamera lookAtCamera = healthBar.GetGameObject().AddComponent<LookAtCamera>();
        lookAtCamera.SetInvert(true);
        lookAtCamera.SetZeroY(true);

        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        healthSystem.OnDead += HealthSystem_OnDead;

        SetIsSelected(false);
    }

    private void HealthSystem_OnDead(object sender, System.EventArgs e) {
        Destroy(gameObject);
        OnDead?.Invoke(this, EventArgs.Empty);
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e) {
        healthBar.SetSize(healthSystem.GetHealthPercent());
    }

    private void Update() {
        unitBehaviour.UpdateBehaviour();
    }

    public NavMeshAgent GetNavMeshAgent() {
        return navMeshAgent;
    }

    public void SetDestination(Vector3 destinationPosition, float stoppingDistance = .5f) {
        navMeshAgent.SetDestination(destinationPosition);
        navMeshAgent.stoppingDistance = stoppingDistance;
        navMeshAgent.isStopped = false;
        OnStartedMoving?.Invoke(this, EventArgs.Empty);
    }

    public void SetActiveBehaviour(IUnitBehaviour unitBehaviour) {
        this.unitBehaviour = unitBehaviour;
    }

    public IUnitBehaviour GetActiveBehaviour() {
        return unitBehaviour;
    }

    public bool GetIsSelected() => isSelected;

    public void SetIsSelected(bool isSelected) {
        this.isSelected = isSelected;
        OnIsSelectedChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool IsEnemy() {
        return isEnemy;
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void Damage(int damageAmount) {
        if (IsDead()) return; // Already dead

        healthSystem.Damage(damageAmount);
    }

    public bool IsDead() {
        return healthSystem.IsDead();
    }

    public void SetStateNormal() {
        GetComponent<NormalUnitBehaviour>().MoveTo(GetPosition());
    }

    public void NormalMoveTo(Vector3 destinationPosition) {
        GetComponent<NormalUnitBehaviour>().MoveTo(destinationPosition);
    }

    public void StopMoving() {
        navMeshAgent.isStopped = true;
        OnStoppedMoving?.Invoke(this, EventArgs.Empty);
    }

    public bool IsStopped() {
        return navMeshAgent.isStopped;
    }

    public Transform GetTransform() {
        return transform;
    }

    public float GetAttackDistanceOffset() {
        return attackDistanceOffset;
    }

}
