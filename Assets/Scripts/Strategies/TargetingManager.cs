using UnityEngine;

namespace Strategies
{
    public class TargetingManager : MonoBehaviour
    {
        public InputReader input;
        public Camera cam;
        
        private TargetingStrategy _currentStrategy;

        void Update()
        {
            if (_currentStrategy != null && _currentStrategy.IsTargeting)
            {
                _currentStrategy.Update();
            }
        }
        
        public void SetCurrentStrategy(TargetingStrategy strategy) => _currentStrategy = strategy;
        public void ClearCurrentStrategy() => _currentStrategy = null;

    }
}