using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microlight.MicroBar;

public class CreatureStatusBar : MonoBehaviour
{
    [SerializeField] private Creature _creature;
    
    [SerializeField] private string name;

    [SerializeField] private MicroBar hp_bar;
    [SerializeField] private MicroBar hunger_bar;


    private void Awake()
    {
        hp_bar.Initialize(_creature.MaxHp);
        hunger_bar.Initialize(_creature.MaxStomach);

    }

    private void Update()
    {
        hp_bar.UpdateHealthBar(_creature.Hp);
        hunger_bar.UpdateHealthBar(_creature.CurrentStomach);

    }
}
