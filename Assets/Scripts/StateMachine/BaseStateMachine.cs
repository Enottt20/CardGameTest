using System;
using System.Collections.Generic;

namespace StateMachine
{
    public abstract class BaseStateMachine
    {

        #region Constructor
        
        protected BaseStateMachine(IStateMachineHandler handler = null)
        {
            Handler = handler;
        }

        #endregion
        

        #region Properties
        
        public bool IsInitialized { get; protected set; }
        
        private readonly Stack<IState> stack = new Stack<IState>();
        
        private readonly Dictionary<Type, IState> register = new Dictionary<Type, IState>();
        
        public IStateMachineHandler Handler { get; set; }
        
        public IState Current => PeekState();

        #endregion
        

        #region Initialization
        
        public void RegisterState(IState state)
        {
            if (state == null)
                throw new ArgumentNullException("Null is not a valid state");

            var type = state.GetType();
            register.Add(type, state);
        }
        
        public void Initialize()
        {
            //create states
            OnBeforeInitialize();

            //register all states
            foreach (var state in register.Values)
                state.OnInitialize();

            IsInitialized = true;

            OnInitialize();
        }
        
        protected virtual void OnBeforeInitialize()
        {
        }
        
        protected virtual void OnInitialize()
        {
        }

        #endregion
        

        #region Operations
        
        public void Update()
        {
            Current?.OnUpdate();
        }
        
        public bool IsCurrent<T>() where T : IState
        {
            return Current?.GetType() == typeof(T);
        }
        
        public bool IsCurrent(IState state)
        {
            if (state == null)
                throw new ArgumentNullException();

            return Current?.GetType() == state.GetType();
        }
        
        public void PushState<T>(bool isSilent = false) where T : IState
        {
            var stateType = typeof(T);
            var state = register[stateType];
            PushState(state, isSilent);
        }
        
        public void PushState(IState state, bool isSilent = false)
        {
            var type = state.GetType();
            if (!register.ContainsKey(type))
                throw new ArgumentException("State " + state + " not registered yet.");
            
            if (stack.Count > 0 && !isSilent)
                Current?.OnExitState();

            stack.Push(state);
            state.OnEnterState();
        }
        
        public IState PeekState()
        {
            return stack.Count > 0 ? stack.Peek() : null;
        }
        
        public void PopState(bool isSilent = false)
        {
            if (Current == null)
                return;

            var state = stack.Pop();
            state.OnExitState();

            if (!isSilent)
                Current?.OnEnterState();
        }
        
        public virtual void Clear()
        {
            foreach (var state in register.Values)
                state.OnClear();

            stack.Clear();
            register.Clear();
        }

        #endregion
        
    }
}