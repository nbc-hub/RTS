using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Building : MonoBehaviour, IUnitDamageable {

    public event EventHandler OnDead;


    [SerializeField] private int healthAmountMax;
    [SerializeField] private bool isEnemy;
    [SerializeField] private float healthBarOffsetY;
    [SerializeField] private float attackDistanceOffset;

    private HealthSystem healthSystem;
    private World_Bar healthBar;

    private void Awake() {
        healthSystem = new HealthSystem(healthAmountMax);

        healthBar = World_Bar.Create(transform, new Vector3(0, healthBarOffsetY, 0), new Vector3(2, .3f), Color.grey, Color.red, 1f, 0, new World_Bar.Outline { color = Color.black, size = .1f });
        LookAtCamera lookAtCamera = healthBar.GetGameObject().AddComponent<LookAtCamera>();
        lookAtCamera.SetInvert(true);

        healthBar.Hide();

        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        healthSystem.OnDead += HealthSystem_OnDead;
    }

    private void HealthSystem_OnDead(object sender, System.EventArgs e) {
        Destroy(gameObject);
        OnDead?.Invoke(this, EventArgs.Empty);
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e) {
        if (healthSystem.GetHealthPercent() < 1f) {
            healthBar.Show();
            healthBar.SetSize(healthSystem.GetHealthPercent());
        } else {
            healthBar.Hide();
        }
    }

    public bool IsEnemy() {
        return isEnemy;
    }

    public bool IsDead() {
        return healthSystem.IsDead();
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void Damage(int damageAmount) {
        healthSystem.Damage(damageAmount);
    }

    public Transform GetTransform() {
        return transform;
    }

    public float GetAttackDistanceOffset() {
        return attackDistanceOffset;
    }

}
