#region Using

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

#endregion

namespace NLib
{
    /// <summary>
    /// WpfVisualHelper Extension Methods.
    /// </summary>
    public static class WpfVisualHelper
    {
        #region GetChildOfType

        public static T GetChildOfType<T>(this DependencyObject depObj)
            where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null) return result;
            }
            return null;
        }

        #endregion
    }

    /// <summary>
    /// WpfControlHelper Extension Methods.
    /// </summary>
    public static class WpfControlHelper
    {
        #region InvokeAction

        private static void InvokeAction(Action action)
        {
            if (null == action) return;
            var dispatcher = Application.Current.Dispatcher;
            try
            {

                if (null != dispatcher)
                {
                    dispatcher.BeginInvoke(action, DispatcherPriority.Render);
                }
                else
                {
                    action.Call();
                }
            }
            catch { }
        }

        public static void InvokeAction<T>(this T target, Action action)
            where T : Control
        {
            InvokeAction(action);
        }

        #endregion

        #region FocusControl

        public static void FocusControl<T>(this T target) 
            where T : Control
        {
            if (null == target) return;
            
            if (!target.Focusable) 
                return; // cannot focus
            if (target.IsFocused) 
                return; // already focused

            InvokeAction(() => 
            { 
                if (target is TextBox) 
                { 
                    (target as TextBox).SelectAll(); 
                }
                else if (target is PasswordBox)
                {
                    (target as PasswordBox).SelectAll();
                }
                target.Focus();
            });
        }

        #endregion
    }
}
