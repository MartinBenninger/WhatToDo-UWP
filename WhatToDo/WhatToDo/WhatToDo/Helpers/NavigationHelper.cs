namespace WhatToDo.Helpers
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Navigation helper. Especially useful when writing platform specific code.
    /// </summary>
    public class NavigationHelper
    {
        /// <summary>
        /// Gets the current navigation helper.
        /// </summary>
        public static NavigationHelper Current { get; } = new NavigationHelper();

        /// <summary>
        /// Performs the navigation necessary when the login was successful.
        /// </summary>
        public Func<Task> NavigateLoginSuccess { get; set; }

        /// <summary>
        /// Performs the navigation necessary when the login failed or was canceled.
        /// </summary>
        public Func<Task> NavigateLoginFailure { get; set; }
    }
}