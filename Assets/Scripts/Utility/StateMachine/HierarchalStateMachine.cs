namespace Utility.StateMachine
{
    public class HierarchalStateMachine
    {
        private HierarchalStateMachine _currentState = null;

        
        /*
         *  Called when this state becomes active. Guaranteed to happen after exit state.
         *  Make changes required for this state to be active.
         */
        protected virtual void OnEnterState()
        {
            if (_currentState != null)
            {
                _currentState.OnEnterState();
            }
        }

        /*
         *  Called when this state stops being active. Guaranteed to happen before the new state's enter state.
         *  Reset object state here. Objects should be in a neutral state during transition so every state is ready.
         */
        protected virtual void OnExitState()
        {
            if (_currentState != null)
            {
                _currentState.OnExitState();
            }
        }

        /*
         *  Called on Update()
         *  Must be called by class holding the top layer state machine.
         */
        public virtual void OnUpdateState()
        {
            if (_currentState != null)
            {
                _currentState.OnUpdateState();
            }
        }

        /*
         *  Called on FixedUpdate()
         *  Must be called by class holding the top layer state machine.
         */
        public virtual void OnFixedUpdateState()
        {
            if (_currentState != null)
            {
                _currentState.OnFixedUpdateState();
            }
        }

        /*
         *  Call to switch state
         *  I reccomend making methods for each state that call this method inside
         *  ie. SwitchToGroundedState, SwitchToAirState
         */
        public void SwitchState(HierarchalStateMachine newState)
        {
            if (_currentState == newState) return;
            if (_currentState != null && newState != null)
            {
                _currentState.OnExitState();
            }
            
            _currentState = newState;
            
            if (_currentState != null)
            {
                _currentState.OnEnterState();
            }
        }

        ~HierarchalStateMachine()
        {
            SwitchState(null);
        }
    }
}