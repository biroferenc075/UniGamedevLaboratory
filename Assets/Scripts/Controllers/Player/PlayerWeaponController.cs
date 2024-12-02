using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class PlayerWeaponController : MonoBehaviour
    {
        [SerializeField] Weapon mainWeaponLvl1;
        [SerializeField] Weapon secondaryWeaponLvl1;

        [SerializeField] Weapon mainWeaponLvl2;
        [SerializeField] Weapon secondaryWeaponLvl2;

        [SerializeField] Weapon mainWeaponLvl3;
        [SerializeField] Weapon secondaryWeaponLvl3;

        [SerializeField] Transform firingPoint;

        private Weapon main;
        private Weapon secondary;

        private int level = 1;

        internal void UpgradeWeapon()
        {
            level++;

            if (level == 2)
            {
                main = mainWeaponLvl2;
                secondary = secondaryWeaponLvl2;
            }
            else if (level >= 3)
            {
                main = mainWeaponLvl3;
                secondary = secondaryWeaponLvl3;
            } else
            {
                main = mainWeaponLvl1;
                secondary = secondaryWeaponLvl1;
            }
            main.Init();
            secondary.Init();
        }

        void Start()
        {
            main = mainWeaponLvl1;
            secondary = secondaryWeaponLvl1;

            main.Init();
            secondary.Init();
        }

        void Update()
        {
            if(Input.GetKey(KeyCode.X)) {
                main.Fire(firingPoint, Quaternion.identity, true);
            }

            if(Input.GetKey(KeyCode.C)) { 
                secondary.Fire(firingPoint,Quaternion.identity, true); 
            }
            if (Input.GetKeyDown(KeyCode.U)) {
                UpgradeWeapon();
            }
        }
    }
}
