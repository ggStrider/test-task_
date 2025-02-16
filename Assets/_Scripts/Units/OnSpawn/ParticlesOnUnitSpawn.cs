using UnityEngine;

namespace Units.OnSpawn
{
    public class ParticlesOnUnitSpawn : MonoBehaviour, IOnThisUnitSpawn
    {
        [SerializeField] private ParticleSystem _particles;
        
        public void OnThisUnitSpawn()
        {
            _particles?.Play();
        }
    }
}