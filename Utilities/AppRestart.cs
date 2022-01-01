﻿// <copyright file="AppRestart.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SystemTrayMenu.Utilities
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    internal class AppRestart
    {
        public static event EventHandlerEmpty BeforeRestarting;

        internal static void ByThreadException()
        {
            Restart(GetCurrentMethod());
        }

        internal static void ByAppContextMenu()
        {
            Restart(GetCurrentMethod());
        }

        internal static void ByDisplaySettings()
        {
            Restart(GetCurrentMethod());
        }

        internal static void ByConfigChange()
        {
            Restart(GetCurrentMethod());
        }

        internal static void ByMenuButton()
        {
            Restart(GetCurrentMethod());
        }

        private static void Restart(string reason)
        {
            BeforeRestarting?.Invoke();
            Log.Info($"Restart by '{reason}'");
            Log.Close();

            using (Process p = new())
            {
                p.StartInfo = new ProcessStartInfo(
                    Process.GetCurrentProcess().MainModule.FileName,
                    "-r");
                try
                {
                    p.Start();
                }
                catch (Win32Exception ex)
                {
                    Log.Warn("Restart failed", ex);
                }
            }

            Application.Exit();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static string GetCurrentMethod()
        {
            StackTrace st = new();
            StackFrame sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }
    }
}
