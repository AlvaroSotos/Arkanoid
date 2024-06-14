using AimClicker.Scripts;
using Scripts.Arkanoid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManagerDoh : WorldManagerTop
{
    DohController dohController;
    DamageController damageController;

    protected override void Awake()
    {
        base.Awake();
        dohController = GetComponentInChildren<DohController>();
        dohController.OnDohDestroyedEvent += OnDohDestroyedCallback;
        dohController.OnDohDestroyedBeforeEvent += OnDohDestroyedBeforeCallback;
        damageController = GetComponentInChildren<DamageController>();
    }

    private void OnDohDestroyedBeforeCallback()
    {
        ballsManager.Freeze();
        
    }

    private void OnDohDestroyedCallback()
    {
        dohController.OnDohDestroyedEvent -= OnDohDestroyedCallback;
        levelManager.NextLevel();
        playerManager.AddScore(damageController.GetScore());
    }
    void ResetBullets()
    {
        
    }


}
