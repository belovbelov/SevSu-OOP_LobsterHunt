using Assets.Scripts;
using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class HeadBob
    {
        protected Player player; 
        protected float TAimAdjust = 1f;
        protected float Intensity = 0.015f;

        public abstract void doHeadBob();
    }

    public class HeadBobIdle : HeadBob
    {
        public HeadBobIdle(Player p)
        {
            player = p;
        }

        public override void doHeadBob()
        {
            player.TargetWeaponBobPosition = player.WeaponParentCurrentPos + new Vector3(Mathf.Cos(player.MovementCounter) * Intensity * TAimAdjust, Mathf.Sin(player.MovementCounter * 2) * Intensity * TAimAdjust, 0);
            player.IdleCounter += Time.deltaTime;
            player.WeaponParent.localPosition = Vector3.MoveTowards(player.WeaponParent.localPosition, player.TargetWeaponBobPosition, Time.deltaTime * 2f * 0.1f);
        }
    }

    public class HeadBobJump : HeadBob
    {
        public HeadBobJump(Player p)
        {
            player = p;

        }

        public override void doHeadBob()
        {
            
            player.TargetWeaponBobPosition = player.WeaponParentCurrentPos + new Vector3(Mathf.Cos(player.MovementCounter) * Intensity * TAimAdjust, Mathf.Sin(player.MovementCounter * 2) * Intensity * TAimAdjust, 0);
            player.IdleCounter += Time.deltaTime * 0.5f;
            player.WeaponParent.localPosition = Vector3.MoveTowards(player.WeaponParent.localPosition, player.TargetWeaponBobPosition, Time.deltaTime * 2f * 0.2f);
        }
    }

    public class HeadBobRun : HeadBob
    {
        private new readonly float intensity = 0.02f;
        public HeadBobRun(Player p)
        {
            player = p;
        }

        public override void doHeadBob()
        {
            player.TargetWeaponBobPosition = player.WeaponParentCurrentPos + new Vector3(Mathf.Cos(player.MovementCounter) * intensity * TAimAdjust, Mathf.Sin(player.MovementCounter * 2) * intensity * TAimAdjust, 0);
            player.MovementCounter += Time.deltaTime * 6.75f;
            player.WeaponParent.localPosition = Vector3.MoveTowards(player.WeaponParent.localPosition, player.TargetWeaponBobPosition, Time.deltaTime * 12f * 0.25f);
        }
    }

    public class HeadBobWalk : HeadBob
    {
        public HeadBobWalk(Player p)
        {
            player = p;
        }

        public override void doHeadBob()
        {
            player.TargetWeaponBobPosition = player.WeaponParentCurrentPos + new Vector3(Mathf.Cos(player.MovementCounter) * 0.02f * TAimAdjust, Mathf.Sin(player.MovementCounter * 2) * 0.02f * TAimAdjust, 0);
            player.MovementCounter += Time.deltaTime * 5f;
            player.WeaponParent.localPosition = Vector3.MoveTowards(player.WeaponParent.localPosition,
                player.TargetWeaponBobPosition, Time.deltaTime * 12f * 0.15f);
        }

    }
}   
