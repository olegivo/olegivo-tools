using System;

namespace Oleg_ivo.Base.Utils
{
    /// <summary>
    /// Позволяет применять новое состояние, сохраняя старое, а при вызове <see cref="Dispose"/> - восстанавливать его
    /// </summary>
    /// <typeparam name="TState">Тип состояние</typeparam>
    public abstract class StateHolder<TState> : IDisposable
    {
        private Action<TState> restoreState;
        private readonly TState oldState;

        protected StateHolder(Func<TState> applyNewStateAndReturnOld, Action<TState> restoreState)
        {
            this.restoreState = restoreState;
            oldState = applyNewStateAndReturnOld();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (restoreState == null) return;
            restoreState(oldState);
            restoreState = null;
        }
    }
}