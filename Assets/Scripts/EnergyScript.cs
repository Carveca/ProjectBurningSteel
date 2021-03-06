﻿using UnityEngine;
using System.Collections;

public class EnergyScript : MonoBehaviour 
{
    public float maxEnergy = 100.0f;
    public float currentEnergy;

    public float boostCostModifier = 5.0f;
    public float boostRechargeModifier = 20.0f;
    public float attritionModifier = 10.0f;
    public float immunitDuration;

    private float immunityTimer = 0.0f;

    void Start()
    {
        currentEnergy = maxEnergy;
    }
    void Update()
    {
        if(immunityTimer > 0.0f)
        {
            immunityTimer -= Time.deltaTime;
        }

        if (currentEnergy < 0.0f)
        {
            EnergyDeath();
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Wall")
        {
            if(immunityTimer <= 0.0f)
            {
                currentEnergy -= attritionModifier;
            }            
        }
    }
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Wall")
        {
            if (immunityTimer <= 0.0f)
            {
                currentEnergy -= attritionModifier * Time.deltaTime;
            }
        }
    }


    public void BoostCost()
    {
        currentEnergy -= Time.deltaTime * boostCostModifier;
    }
    public void Recharge()
    {
        currentEnergy += Time.deltaTime * boostRechargeModifier;
    }
    public void AttritionDamage()
    {
        currentEnergy -= Time.deltaTime * attritionModifier;
    }

    public void Immunity()
    {
        if(immunityTimer <= 0.0f)
        {
            immunityTimer = 0.5f;
        }
    }

    void EnergyDeath()
    {
        GetComponent<PlayerRespawnScript>().Respawn();
    }
}
