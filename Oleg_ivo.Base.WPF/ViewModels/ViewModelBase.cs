using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Oleg_ivo.Base.WPF.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        protected readonly CompositeDisposable Disposer = new CompositeDisposable();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Disposer.Dispose();
            if(Disposed!=null) Disposed(this, EventArgs.Empty);
        }

        public event EventHandler Disposed;

        protected void RaisePropertiesChanged(params string[] prorertyNames)
        {
            foreach (var prorertyName in prorertyNames)
            {
                RaisePropertyChanged(prorertyName);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName = null)
        {
            var onPropertyChanged = PropertyChanged;
            if (onPropertyChanged != null)
            {
                onPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event if needed.
        /// 
        /// </summary>
        /// <typeparam name="T">The type of the property that
        ///             changed.</typeparam><param name="propertyExpression">An expression identifying the property
        ///             that changed.</param>
        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var changedEventHandler = PropertyChanged;
            if (changedEventHandler == null)
                return;
            var propertyName = GetPropertyName(propertyExpression);
            changedEventHandler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Extracts the name of a property from an expression.
        /// 
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam><param name="propertyExpression">An expression returning the property's name.</param>
        /// <returns>
        /// The name of the property returned by the expression.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">If the expression is null.</exception><exception cref="T:System.ArgumentException">If the expression does not represent a property.</exception>
        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException("propertyExpression");
            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException("Invalid argument", "propertyExpression");
            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
                throw new ArgumentException("Argument is not a property", "propertyExpression");
            return propertyInfo.Name;
        }

        /// <summary>
        /// Assigns a new value to the property. Then, raises the
        ///             PropertyChanged event if needed.
        /// 
        /// </summary>
        /// <typeparam name="T">The type of the property that
        ///             changed.</typeparam><param name="propertyExpression">An expression identifying the property
        ///             that changed.</param><param name="field">The field storing the property's value.</param><param name="newValue">The property's value after the change
        ///             occurred.</param>
        /// <returns>
        /// True if the PropertyChanged event has been raised,
        ///             false otherwise. The event is not raised if the old
        ///             value is equal to the new value.
        /// </returns>
        protected bool Set<T>(Expression<Func<T>> propertyExpression, ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
                return false;
            field = newValue;
            this.RaisePropertyChanged<T>(propertyExpression);
            return true;
        }

        /// <summary>
        /// Assigns a new value to the property. Then, raises the
        ///             PropertyChanged event if needed.
        /// 
        /// </summary>
        /// <typeparam name="T">The type of the property that
        ///             changed.</typeparam><param name="propertyName">The name of the property that
        ///             changed.</param><param name="field">The field storing the property's value.</param><param name="newValue">The property's value after the change
        ///             occurred.</param>
        /// <returns>
        /// True if the PropertyChanged event has been raised,
        ///             false otherwise. The event is not raised if the old
        ///             value is equal to the new value.
        /// </returns>
        protected bool Set<T>(string propertyName, ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
                return false;
            field = newValue;
            this.RaisePropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Assigns a new value to the property. Then, raises the
        ///             PropertyChanged event if needed.
        /// 
        /// </summary>
        /// <typeparam name="T">The type of the property that
        ///             changed.</typeparam><param name="field">The field storing the property's value.</param><param name="newValue">The property's value after the change
        ///             occurred.</param><param name="propertyName">(optional) The name of the property that
        ///             changed.</param>
        /// <returns>
        /// True if the PropertyChanged event has been raised,
        ///             false otherwise. The event is not raised if the old
        ///             value is equal to the new value.
        /// </returns>
        protected bool Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            return Set(propertyName, ref field, newValue);
        }

    }
}