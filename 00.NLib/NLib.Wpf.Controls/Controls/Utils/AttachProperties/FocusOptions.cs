#region Using

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

#endregion

namespace NLib.Wpf.Controls.Utils
{
    #region FocusOptions

    /// <summary>
    /// The Focus Options class.
    /// </summary>
    public class FocusOptions : DependencyObject
    {
        #region SelectAll

        /// <summary>The SelectAllProperty variable</summary>
        public static readonly DependencyProperty SelectAllProperty = DependencyProperty.RegisterAttached(
            "SelectAll",
            typeof(bool),
            typeof(FocusOptions),
            new UIPropertyMetadata(false, SelectAllPropertyChanged));

        /// <summary>
        /// Gets SelectAll Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <returns>Returns current proeprty value.</returns>
        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        [AttachedPropertyBrowsableForType(typeof(PasswordBox))]
        //[AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
        public static bool GetSelectAll(DependencyObject obj)
        {
            return (bool)obj.GetValue(SelectAllProperty);
        }
        /// <summary>
        /// Sets SelectAll Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="value">The new value.</param>
        public static void SetSelectAll(DependencyObject obj, bool value)
        {
            obj.SetValue(SelectAllProperty, value);
        }

        #endregion

        #region SelectAll Changed Handler

        private static void SelectAllPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is TextBox || obj is PasswordBox)
            {
                if ((e.NewValue as bool?).GetValueOrDefault(false))
                {
                    HookEvents(obj as TextBox);
                    HookEvents(obj as PasswordBox);
                }
                else
                {
                    UnhookEvents(obj as TextBox);
                    UnhookEvents(obj as PasswordBox);
                }
            }
        }

        #region TextBox

        private static void HookEvents(TextBox ctrl)
        {
            if (null == ctrl) return;
            ctrl.GotKeyboardFocus += OnGotKeyboardFocus;
            ctrl.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
        }
        private static void UnhookEvents(TextBox ctrl)
        {
            if (null == ctrl) return;
            ctrl.GotKeyboardFocus -= OnGotKeyboardFocus;
            ctrl.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
        }

        #endregion

        #region PasswordBox

        private static void HookEvents(PasswordBox ctrl)
        {
            if (null == ctrl) return;
            ctrl.GotKeyboardFocus += OnGotKeyboardFocus;
            ctrl.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
        }
        private static void UnhookEvents(PasswordBox ctrl)
        {
            if (null == ctrl) return;
            ctrl.GotKeyboardFocus -= OnGotKeyboardFocus;
            ctrl.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
        }

        #endregion

        #endregion

        #region Control Event Handlers

        private static void OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.OriginalSource is TextBox || e.OriginalSource is PasswordBox)
            {
                KeyboardFocusSelectText(e.OriginalSource as TextBox, e);
                KeyboardFocusSelectText(e.OriginalSource as PasswordBox, e);
            }
        }

        private static void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is TextBox || e.OriginalSource is PasswordBox)
            {
                MouseLeftButtonDownFocus(e.OriginalSource as TextBox, e);
                MouseLeftButtonDownFocus(e.OriginalSource as PasswordBox, e);
            }
        }

        #region TextBox

        private static void KeyboardFocusSelectText(TextBox ctrl, KeyboardFocusChangedEventArgs e)
        {
            if (null != ctrl && ctrl.IsKeyboardFocusWithin)
            {
                ctrl.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ctrl.SelectAll();
                }));
            }
        }

        private static void MouseLeftButtonDownFocus(TextBox ctrl, MouseButtonEventArgs e)
        {
            if (ctrl == null) return;
            if (!ctrl.IsFocused)
            {
                ctrl.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ctrl.Focus();
                    ctrl.ReleaseMouseCapture();
                }));
                e.Handled = true;
            }
        }

        #endregion

        #region PasswordBox

        private static void KeyboardFocusSelectText(PasswordBox ctrl, KeyboardFocusChangedEventArgs e)
        {
            if (null != ctrl && ctrl.IsKeyboardFocusWithin)
            {
                ctrl.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ctrl.SelectAll();
                }));
            }
        }

        private static void MouseLeftButtonDownFocus(PasswordBox ctrl, MouseButtonEventArgs e)
        {
            if (null == ctrl) return;
            if (!ctrl.IsFocused)
            {
                ctrl.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ctrl.Focus();
                    ctrl.ReleaseMouseCapture();
                }));
                e.Handled = true;
            }
        }

        #endregion

        #endregion
    }

    #endregion
}
