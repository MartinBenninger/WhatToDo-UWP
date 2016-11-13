using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToDo.Models;

namespace WhatToDo.ViewModels
{
    public class TasklistsViewModel : BaseViewModel
    {
        /// <summary>
        /// A list of all tasklists.
        /// </summary>
        public List<Tasklist> Tasklists { get; set; }

        /// <summary>
        /// Initializes a new instance of TasklistsViewModel.
        /// </summary>
        public TasklistsViewModel()
        {
            Title = "Lists";
            Tasklists = new List<Tasklist>();
        }
    }
}
