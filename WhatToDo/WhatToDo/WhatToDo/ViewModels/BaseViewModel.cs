using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatToDo.ViewModels
{
    public class BaseViewModel
    {
        /// <summary>
        /// The page title.
        /// </summary>
        public string Title { get; set; }

        public BaseViewModel()
        {
            Title = "Page Title";
        }
    }
}
