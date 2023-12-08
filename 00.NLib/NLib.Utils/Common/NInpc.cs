#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

#endregion

namespace NLib
{
    #region NInpc

    /// <summary>
    /// The NInpc abstract class.
    /// Provide basic implementation of INotifyPropertyChanged interface.
    /// </summary>
    public abstract class NInpc : INotifyPropertyChanged
    {
        #region Internal classes

        abstract class NProperty { }

        class NProperty<T> : NProperty
        {
            #region Public Properties

            /// <summary>Gets or sets Value.</summary>
            public T Value { get; set; }

            #endregion
        }

        class NProperties
        {
            #region Internal Variables

            private object olock = new object();
            private Dictionary<string, NProperty> _properties = new Dictionary<string, NProperty>();

            #endregion

            #region Public Methods

            /// <summary>
            /// Gets Value.
            /// </summary>
            /// <typeparam name="T">The target proeprty type.</typeparam>
            /// <param name="proopertyName">The Property Name.</param>
            /// <returns>Returns Property value.</returns>
            /// <exception cref="ArgumentNullException"></exception>
            public T Get<T>(string proopertyName)
            {
                if (string.IsNullOrWhiteSpace(proopertyName))
                    throw new ArgumentNullException(nameof(proopertyName));

                lock (olock)
                {
                    if (!_properties.TryGetValue(proopertyName, out var _))
                    {
                        var p = new NProperty<T>() { Value = default };
                        _properties.Add(proopertyName, p);
                    }

                    var inst = _properties[proopertyName] as NProperty<T>;
                    return (null != inst) ? inst.Value : default;
                }
            }
            /// <summary>
            /// Set Value.
            /// </summary>
            /// <typeparam name="T">The target proeprty type.</typeparam>
            /// <param name="proopertyName">The Property Name.</param>
            /// <param name="value"></param>
            /// <returns>Returns True if assigned value is not equal to original value.</returns>
            /// <exception cref="ArgumentNullException"></exception>
            public bool Set<T>(string proopertyName, T value)
            {
                if (string.IsNullOrWhiteSpace(proopertyName))
                    throw new ArgumentNullException(nameof(proopertyName));

                lock (olock)
                {
                    bool bChanged = false;
                    if (!_properties.TryGetValue(proopertyName, out _))
                    {
                        var p = new NProperty<T>() { Value = value };
                        _properties.Add(proopertyName, p);
                        bChanged = true;
                    }
                    else
                    {
                        var inst = _properties[proopertyName] as NProperty<T>;
                        if (null != inst)
                        {
                            if (null == inst.Value && value == null)
                            {
                                bChanged = false;
                            }
                            else 
                            {
                                if (null == inst.Value || !inst.Value.Equals(value))
                                {
                                    inst.Value = value;
                                }
                                bChanged = true;
                            }
                        }
                    }
                    return bChanged;
                }
            }

            #endregion
        }

        #endregion

        #region Internal Variables

        private bool _lock = false;
        private NProperties _properties = new NProperties();

        #endregion

        #region Private Methods

        /// <summary>
        /// Internal Raise Property Changed event (Lamda function).
        /// </summary>
        /// <param name="selectorExpression">The Expression function.</param>

        private void InternalRaise<T>(Expression<Func<T>> selectorExpression)
        {
            if (_lock) return; // if lock do nothing
            if (null == selectorExpression)
            {
                throw new ArgumentNullException("selectorExpression");
                // return;
            }
            var me = selectorExpression.Body as MemberExpression;

            // Nullable properties can be nested inside of a convert function
            if (null == me)
            {
                var ue = selectorExpression.Body as UnaryExpression;
                if (null != ue)
                {
                    me = ue.Operand as MemberExpression;
                }
            }

            if (null == me)
            {
                throw new ArgumentException("The body must be a member expression");
                // return;
            }
            Raise(me.Member.Name);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Get Value.
        /// </summary>
        /// <typeparam name="T">The Target Type.</typeparam>
        /// <param name="propertyName">The Property Name.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        protected T Get<T>([CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            return _properties.Get<T>(propertyName);
        }
        /// <summary>
        /// Set Value.
        /// </summary>
        /// <typeparam name="T">The Target Type.</typeparam>
        /// <param name="Value">The Value to set.</param>
        /// <param name="action">The action to execute when Value is change.</param>
        /// <param name="propertyName">The Property Name.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected void Set<T>(T Value, Action action = null,
            [CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            if (_properties.Set<T>(propertyName, Value))
            {
                Raise(propertyName); // Raise PropertyChanged
                if (null != action)
                {
                    // do some extra action.
                    action.Call();
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Enable Notify Change Event.
        /// </summary>
        public void EnableNotify()
        {
            _lock = true;
        }
        /// <summary>
        /// Disable Notify Change Event.
        /// </summary>
        public void DisableNotify()
        {
            _lock = false;
        }
        /// <summary>
        /// Raise Property Changed event.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        public void Raise([CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName)) return;
            if (_lock) return; // if lock do nothing
            // raise event.
            PropertyChanged.Call(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// Raise Property Changed event (Lamda function).
        /// </summary>
        /// <param name="actions">The array of lamda expression's functions.</param>
        public void Raise(params Expression<Func<object>>[] actions)
        {
            if (_lock) return; // if lock do nothing
            if (null != actions && actions.Length > 0)
            {
                foreach (var item in actions)
                {
                    if (null != item) InternalRaise(item);
                }
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Checks is notifiy enabled or disable.
        /// </summary>
        /// <returns>Returns true if enabled.</returns>
        public bool IsLocked { get { return _lock; } set { } }

        #endregion

        #region Public Events

        /// <summary>
        /// The PropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    #endregion
}
