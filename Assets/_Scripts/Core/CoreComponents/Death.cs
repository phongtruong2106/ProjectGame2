using System;
using UnityEngine;

public class Death : CoreComponent, IDDataPersistence
{
        [SerializeField ] 
        private GameObject[] deathParticles;
        public static int deathCount = 0;

        private ParticleManage ParticleManager =>particleManage ? particleManage : core.GetCoreComponent(ref particleManage);
        private ParticleManage particleManage;
        private GameManager GM;
        private Stats Stats => stats ? stats : core.GetCoreComponent(ref stats);
        private Stats stats;
        private void Start() {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        public void Die()
        {
            foreach (var particle in deathParticles)
            {
                ParticleManager.StartParticles(particle);
            }
            core.transform.parent.gameObject.SetActive(false);
            GM.Respawn();
                //xoa di object
            Destroy(gameObject);
            deathCount++;
            Debug.Log("Số lần Nhân vật chết: " +    Death.deathCount);
        }

        private void OnEnable()
        {
            Stats.OnHealthZero += Die;
        }

        private void OnDisable()
        {
            Stats.OnHealthZero -= Die;
        }

    public void LoadData(GameData data)
    {   
        Death.deathCount = data.deadCount;
    }

    public void SaveData(ref GameData data)
    {
        data.deadCount = deathCount;
    }
}
