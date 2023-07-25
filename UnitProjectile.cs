using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProjectile : MonoBehaviour {


    public static void Create(Vector3 spawnPosition, IUnitDamageable targetUnit, int damageAmount) {
        Transform unitProjectileTransform = Instantiate(Assets.Instance.pfUnitProjectile, spawnPosition, Quaternion.identity);

        UnitProjectile unitProjectile  = unitProjectileTransform.GetComponent<UnitProjectile>();
        unitProjectile.Setup(targetUnit, damageAmount);
    }



    private IUnitDamageable targetUnit;
    private int damageAmount;

    private void Setup(IUnitDamageable targetUnit, int damageAmount) {
        this.targetUnit = targetUnit;
        this.damageAmount = damageAmount;
    }

    private void Update() {
        if (targetUnit.IsDead()) {
            // Target is already dead
            DestroySelf();
            return;
        }

        Vector3 moveDir = (targetUnit.GetPosition() - transform.position).normalized;
        float moveSpeed = 10f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        transform.LookAt(targetUnit.GetTransform());

        float damageDistance = .5f;
        if (Vector3.Distance(transform.position, targetUnit.GetPosition()) < damageDistance) {
            targetUnit.Damage(damageAmount);
            DestroySelf();
        }
    }

    private void DestroySelf() {
        Destroy(gameObject);
    }

}

