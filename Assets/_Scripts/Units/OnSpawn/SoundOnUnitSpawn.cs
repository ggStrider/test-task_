using Audio;
using UnityEngine;

namespace Units.OnSpawn
{
    public class SoundOnUnitSpawn : MonoBehaviour, IOnThisUnitSpawn
    {
        [SerializeField] private AudioPlayer _audioPlayer;
        
        public void OnThisUnitSpawn()
        {
            _audioPlayer?.PlayShot();
        }
    }
}