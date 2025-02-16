using UnityEngine;
using DG.Tweening;

namespace Units.OnSpawn
{
    public class AnimateScaleOnUnitSpawn : MonoBehaviour, IOnThisUnitSpawn
    {
        [SerializeField] private Transform _body;
        [SerializeField] private Vector3 _endScale = Vector3.one;
        
        [SerializeField] private Vector3 _startScale = new(0.15f, 0.15f, 0.15f);
        
        [Space]
        [SerializeField] private float _animationDuration = 0.5f;
        [SerializeField] private Ease _animationEase = Ease.OutBounce;
        
        public void OnThisUnitSpawn()
        {
            if(!_body) return;
            
            _body.localScale = _startScale;
            _body.DOScale(_endScale, _animationDuration)
                .SetEase(_animationEase);
        }
    }
}