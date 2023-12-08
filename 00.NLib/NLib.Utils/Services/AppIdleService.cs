#region Using

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Windows;
using System.Windows.Threading;

using NLib;

#endregion

namespace NLib.Services
{
    /// <summary>
    /// Application Idle Service.
    /// </summary>
    public class AppIdleService : NSingelton<AppIdleService>
    {
        #region Win32 classes and apis

        /// <summary>
        /// WindowsHookHelper.
        /// </summary>
        class WindowsHookHelper
        {
            /// <summary>
            /// HookDelegate
            /// </summary>
            /// <param name="Code"></param>
            /// <param name="wParam"></param>
            /// <param name="lParam"></param>
            /// <returns></returns>
            public delegate IntPtr HookDelegate(
                Int32 Code, IntPtr wParam, IntPtr lParam);
            /// <summary>
            /// CallNextHookEx
            /// </summary>
            /// <param name="hHook"></param>
            /// <param name="nCode"></param>
            /// <param name="wParam"></param>
            /// <param name="lParam"></param>
            /// <returns></returns>
            [DllImport("User32.dll")]
            public static extern IntPtr CallNextHookEx(
                IntPtr hHook, Int32 nCode, IntPtr wParam, IntPtr lParam);
            /// <summary>
            /// UnhookWindowsHookEx
            /// </summary>
            /// <param name="hHook"></param>
            /// <returns></returns>
            [DllImport("User32.dll")]
            public static extern IntPtr UnhookWindowsHookEx(IntPtr hHook);
            /// <summary>
            /// SetWindowsHookEx
            /// </summary>
            /// <param name="idHook"></param>
            /// <param name="lpfn"></param>
            /// <param name="hmod"></param>
            /// <param name="dwThreadId"></param>
            /// <returns></returns>
            [DllImport("User32.dll")]
            public static extern IntPtr SetWindowsHookEx(
                Int32 idHook, HookDelegate lpfn, IntPtr hmod,
                Int32 dwThreadId);
        }
        /// <summary>
        /// KeyboardInput
        /// </summary>
        class KeyboardInput : IDisposable
        {
            /// <summary>
            /// KeyBoardKeyPressed event.
            /// </summary>
            public event EventHandler<EventArgs> KeyBoardKeyPressed;

            private WindowsHookHelper.HookDelegate keyBoardDelegate;
            private IntPtr keyBoardHandle;
            private const Int32 WH_KEYBOARD_LL = 13;
            private bool disposed;

            /// <summary>
            /// Constructor
            /// </summary>
            public KeyboardInput()
            {
                keyBoardDelegate = KeyboardHookDelegate;
                keyBoardHandle = WindowsHookHelper.SetWindowsHookEx(
                    WH_KEYBOARD_LL, keyBoardDelegate, IntPtr.Zero, 0);
            }

            private IntPtr KeyboardHookDelegate(
                Int32 Code, IntPtr wParam, IntPtr lParam)
            {
                if (Code < 0)
                {
                    return WindowsHookHelper.CallNextHookEx(
                        keyBoardHandle, Code, wParam, lParam);
                }

                if (KeyBoardKeyPressed != null)
                    KeyBoardKeyPressed(this, new EventArgs());

                return WindowsHookHelper.CallNextHookEx(
                    keyBoardHandle, Code, wParam, lParam);
            }

            /// <summary>
            /// Dispose
            /// </summary>
            public void Dispose()
            {
                Dispose(true);
            }
            /// <summary>
            /// Dispose.
            /// </summary>
            /// <param name="disposing"></param>
            protected virtual void Dispose(bool disposing)
            {
                if (!disposed)
                {
                    if (keyBoardHandle != IntPtr.Zero)
                    {
                        WindowsHookHelper.UnhookWindowsHookEx(
                            keyBoardHandle);
                    }

                    disposed = true;
                }
            }
            /// <summary>
            /// Destructor
            /// </summary>
            ~KeyboardInput()
            {
                Dispose(false);
            }
        }
        /// <summary>
        /// MouseInput
        /// </summary>
        class MouseInput : IDisposable
        {
            /// <summary>
            /// MouseMoved event.
            /// </summary>
            public event EventHandler<EventArgs> MouseMoved;

            private WindowsHookHelper.HookDelegate mouseDelegate;
            private IntPtr mouseHandle;
            private const Int32 WH_MOUSE_LL = 14;

            private bool disposed;

            /// <summary>
            /// Constructor.
            /// </summary>
            public MouseInput()
            {
                mouseDelegate = MouseHookDelegate;
                mouseHandle = WindowsHookHelper.SetWindowsHookEx(WH_MOUSE_LL, mouseDelegate, IntPtr.Zero, 0);
            }

            private IntPtr MouseHookDelegate(Int32 Code, IntPtr wParam, IntPtr lParam)
            {
                if (Code < 0)
                    return WindowsHookHelper.CallNextHookEx(mouseHandle, Code, wParam, lParam);

                if (MouseMoved != null)
                    MouseMoved(this, new EventArgs());

                return WindowsHookHelper.CallNextHookEx(mouseHandle, Code, wParam, lParam);
            }
            /// <summary>
            /// Dispose
            /// </summary>
            public void Dispose()
            {
                Dispose(true);
            }
            /// <summary>
            /// Dispose
            /// </summary>
            /// <param name="disposing"></param>
            protected virtual void Dispose(bool disposing)
            {
                if (!disposed)
                {
                    if (mouseHandle != IntPtr.Zero)
                        WindowsHookHelper.UnhookWindowsHookEx(mouseHandle);

                    disposed = true;
                }
            }
            /// <summary>
            /// Destructor
            /// </summary>
            ~MouseInput()
            {
                Dispose(false);
            }
        }
        /// <summary>
        /// AllInputSources
        /// </summary>
        class AllInputSources
        {
            [DllImport("User32.dll")]
            private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

            /// <summary>
            /// GetLastInputTime.
            /// </summary>
            /// <returns></returns>
            public DateTime GetLastInputTime()
            {
                var lastInputInfo = new LASTINPUTINFO();
                lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);

                GetLastInputInfo(ref lastInputInfo);

                return DateTime.Now.AddMilliseconds(-(Environment.TickCount - lastInputInfo.dwTime));
            }

            [StructLayout(LayoutKind.Sequential)]
            internal struct LASTINPUTINFO
            {
                public uint cbSize;
                public uint dwTime;
            }
        }

        /// <summary>
        /// GetCursorPos
        /// </summary>
        /// <param name="lpPoint"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
        private extern static bool GetCursorPos(out System.Drawing.Point lpPoint);
        /// <summary>
        /// SetCursorPos
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        protected AppIdleService() : base()
        {
            Application.Current.Activated += new EventHandler(Current_Activated);
            Application.Current.Deactivated += new EventHandler(Current_Deactivated);
            /*
            keyboard = new KeyboardInput();
            keyboard.KeyBoardKeyPressed += keyboard_KeyBoardKeyPressed;

            mouse = new MouseInput();
            mouse.MouseMoved += mouse_MouseMoved;
            */
            lastInput = new AllInputSources();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~AppIdleService()
        {
            /*
            if (null != keyboard)
            {
                keyboard.KeyBoardKeyPressed -= keyboard_KeyBoardKeyPressed;
            }
            keyboard = null;
            if (null != mouse)
            {
                mouse.MouseMoved -= mouse_MouseMoved;
            }
            mouse = null;
            */
            if (null != timer)
            {
                timer.Tick -= timer_Tick;
                timer.Stop();
            }
            timer = null;

            if (null != Application.Current)
            {
                Application.Current.Deactivated -= new EventHandler(Current_Deactivated);
                Application.Current.Activated -= new EventHandler(Current_Activated);
            }
        }

        #endregion

        #region Internal Variables

        private DispatcherTimer timer;
        private AllInputSources lastInput;
        //private KeyboardInput keyboard;
        //private MouseInput mouse;

        private bool _isActivated = true;

        private DateTime _lastSystemInputTime = DateTime.MinValue;
        private DateTime _lastInputTime = DateTime.Now;

        //private DateTime _lastMouseTime = DateTime.Now;
        //private DateTime _lastKbdTime = DateTime.Now;
        private DateTime _lastFreeMemory = DateTime.Now;

        #endregion

        #region Private Methods

        #region Event Handlers

        void Current_Deactivated(object sender, EventArgs e)
        {
            _isActivated = false;
        }

        void Current_Activated(object sender, EventArgs e)
        {
            _isActivated = true;
        }

        void keyboard_KeyBoardKeyPressed(object sender, EventArgs e)
        {
            if (_isActivated)
            {
                //_lastKbdTime = DateTime.Now;
            }
        }

        void mouse_MouseMoved(object sender, EventArgs e)
        {
            if (_isActivated)
            {
                //_lastMouseTime = DateTime.Now;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = DateTime.Now - _lastFreeMemory;

            if (ts.TotalMinutes > 5)
            {
                // Try to free memory every 5 minutes
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Optimized);
                GC.WaitForPendingFinalizers();

                // update last free memory time.
                _lastFreeMemory = DateTime.Now;
            }

            if (_isActivated)
            {
                DateTime sysInputTime = lastInput.GetLastInputTime();
                bool updateTime = (_lastSystemInputTime < sysInputTime);
                if (updateTime)
                {
                    // keyboard or mouse action detected
                    _lastSystemInputTime = sysInputTime; // update system time
                }

                if (updateTime)
                {
                    if (_lastInputTime < _lastSystemInputTime)
                    {
                        // Update time only when system input is occur after
                        // last input time assigned (by code or last system input time).
                        _lastInputTime = _lastSystemInputTime;
                    }
                }
            }
            if (null != Tick)
            {
                Tick.Call(this, EventArgs.Empty);
            }
        }

        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// IsTimeOut.
        /// </summary>
        /// <param name="timeoutInSecond">
        /// The timeout value in second.
        /// </param>
        /// <param name="resetTime">
        /// True for reset last input time to current time that timeout method called.
        /// </param>
        /// <param name="debug"></param>
        /// <returns>Returns true if timeout.</returns>
        public bool IsTimeOut(int timeoutInSecond, bool resetTime, bool debug = false)
        {
            bool result = true;

            DateTime now = DateTime.Now;
            if (debug)
            {
                Console.WriteLine("Last Time : {0:HH:mm:ss.ffff}", _lastInputTime);
                Console.WriteLine("  Current : {0:HH:mm:ss.ffff}", now);
            }
            //TimeSpan kbdTs = now - _lastKbdTime;
            //TimeSpan mouseTs = now - _lastMouseTime;
            TimeSpan sysTs = now - _lastInputTime;
            if (/*kbdTs.TotalSeconds > timeoutInSecond &&
                mouseTs.TotalSeconds > timeoutInSecond &&*/
                sysTs.TotalSeconds > timeoutInSecond)
            {
                if (resetTime)
                {
                    ResetLastInputTime();
                }
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }
        /// <summary>
        /// Reset Last Input Time to current time.
        /// </summary>
        public void ResetLastInputTime(bool debug = false)
        {
            lock (this)
            {
                _lastInputTime = DateTime.Now;
            }
            if (debug)
            {
                Console.WriteLine("Reset Time : {0:HH:mm:ss.ffff}", _lastInputTime);
            }
        }
        /// <summary>
        /// Reset Cursor. Force move cursor a little to make system input update it's time
        /// used when new page load.
        /// </summary>
        public void ResetCursor()
        {
            // shake cursor by one pixel
            this.Move(1, 0);
            this.Move(-1, 0);
        }
        /// <summary>
        /// Move the cursor by specificed amount of pixel
        /// </summary>
        /// <param name="dx">The pixel to move from the current position X</param>
        /// <param name="dy">The pixel to move from the current position Y</param>
        internal void Move(int dx, int dy)
        {
            System.Drawing.Point pt = System.Drawing.Point.Empty;
            if (GetCursorPos(out pt))
            {
                if (pt.IsEmpty)
                    return;
                SetCursorPos(pt.X + dx, pt.Y + dy);
            }
        }
        /// <summary>
        /// Move to specificed point (in screen coordinate)
        /// </summary>
        /// <param name="x">The position X to move to</param>
        /// <param name="y">The position Y to move to</param>
        internal void MoveTo(int x, int y)
        {
            SetCursorPos(x, y);
        }

        #endregion

        #region Public Properties

        /*
        /// <summary>
        /// Gets Last Keyboard Time.
        /// </summary>
        public DateTime LastKeyboardInputTime { get { return _lastKbdTime; } }
        /// <summary>
        /// Gets Last Mouse Time.
        /// </summary>
        public DateTime LastMouseInputTime { get { return _lastMouseTime; } }
        */
        /// <summary>
        /// Gets Last System Input Time.
        /// </summary>
        public DateTime LastSystemInputTime { get { return _lastInputTime; } }

        #endregion

        #region Events

        /// <summary>
        /// Tick event.
        /// </summary>
        public event EventHandler Tick;

        #endregion
    }
}
