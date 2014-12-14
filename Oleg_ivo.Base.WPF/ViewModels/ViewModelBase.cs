using System;
using System.Reactive.Disposables;

namespace Oleg_ivo.Base.WPF.ViewModels
{
    public class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase, IDisposable
    {
        protected readonly CompositeDisposable Disposer = new CompositeDisposable();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Disposer.Dispose();
        }
    }
}