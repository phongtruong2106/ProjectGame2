using System;
using UnityEngine;

public class Death : CoreComponent
{
        [SerializeField ] 
        private GameObject[] deathParticles;

        private ParticleManage ParticleManager =>particleManage ? particleManage : core.GetCoreComponent(ref particleManage);
        private ParticleManage particleManage;

        private Stats Stats => stats ? stats : core.GetCoreComponent(ref stats);
        private Stats stats;
        public void Die()
        {
            foreach (var particle in deathParticles)
            {
                ParticleManager.StartParticles(particle);
            }
            core.transform.parent.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            Stats.OnHealthZero += Die;
        }

        private void OnDisable()
        {
            Stats.OnHealthZero -= Die;
        }
}
