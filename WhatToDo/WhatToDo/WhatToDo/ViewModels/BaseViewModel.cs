namespace WhatToDo.ViewModels
{
    /// <summary>
    /// The base view model from which other view models will derive.
    /// </summary>
    public class BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        public BaseViewModel()
        {
            this.Title = "Page Title";
        }

        /// <summary>
        /// The page title.
        /// </summary>
        public string Title { get; set; }
    }
}