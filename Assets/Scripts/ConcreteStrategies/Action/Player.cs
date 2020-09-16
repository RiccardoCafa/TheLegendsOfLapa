﻿using UnityEngine;

namespace Assets.Scripts.ConcreteStrategies.Action
{
    public class Player : ActionStrategy
    {
        private bool TryBasicAttack => Input.GetKeyDown(KeyCode.Z);
        private bool TryHeavyAttack => Input.GetKeyDown(KeyCode.X);
        private bool TryJump => Input.GetKeyDown(KeyCode.UpArrow);
        private bool FirstInvocation => Input.GetKeyDown(KeyCode.Alpha1);
        private bool SecondInvocation => Input.GetKeyDown(KeyCode.Alpha2);
        private bool ThirdInvocation => Input.GetKeyDown(KeyCode.Alpha3);
        private bool HookKey => Input.GetKey(KeyCode.R);
        private bool HookKeyUp => Input.GetKeyUp(KeyCode.R);

        private float HorizontalValue => Input.GetAxisRaw("Horizontal");

        public Summoner summoner;
        public bool IsControlledPlayer;

        private Hook hook;

        private void Awake()
        {
            if (IsControlledPlayer)
            {
                summoner.InvocationFactory = new AllyFactory();
            }
            else
            {
                summoner.InvocationFactory = new EnemyFactory();
            }

            hook = GetComponent<Hook>();
        }

        protected override void VerifyActionsOnUpdate()
        {
            if(HorizontalValue == 0)
            {
                Idle();
            }
            else if(HorizontalValue < 0)
            {
                RunLeft();
            }
            else
            {
                RunRight();
            }

            if (TryJump)
            {
                Jump();
            }

            if (TryBasicAttack)
            {
                BasicAttack();
            }

            if (TryHeavyAttack)
            {
                HeavyAttack();
            }

            if(FirstInvocation)
            {
                summoner.Summon(transform.position + (Vector3.right * transform.localScale.x), EInovcationType.Melee);
            }

            if(SecondInvocation)
            {
                summoner.Summon(transform.position + (Vector3.right * transform.localScale.x), EInovcationType.Magic);
            }

            if(ThirdInvocation)
            {
                summoner.Summon(transform.position + (Vector3.right * transform.localScale.x), EInovcationType.Ranged);
            }

            if(HookKey)
            {
                hook.Hooked();
            }

            if(HookKeyUp)
            {
                hook.Unhook();
            }

            Fall();
        }
    }
}