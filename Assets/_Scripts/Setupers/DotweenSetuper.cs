using DG.Tweening;
using UnityEngine;

namespace Setupers
{
    public class DotweenSetuper : MonoBehaviour
    {
        private const int MAX_TWEEN_CAPACITY = 10000;
        private const int MAX_SEQUENCE_CAPACITY = 100;
        
        private void Awake()
        {
            DOTween.SetTweensCapacity(MAX_TWEEN_CAPACITY, MAX_SEQUENCE_CAPACITY);
        }
    }
}
