using System;

namespace Oleg_ivo.Base.Utils
{
    /// <summary>
    /// ��������� ��������� ����� ���������, �������� ������, � ��� ������ <see cref="Dispose"/> - ��������������� ���
    /// </summary>
    /// <typeparam name="TState">��� ���������</typeparam>
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