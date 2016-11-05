using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToDo.Models;
using WhatToDo.ViewModels;
using Xamarin.Forms;

namespace WhatToDo.Views
{
    public partial class Tasklists : ContentPage
    {
        public Tasklists()
        {
            InitializeComponent();

            BindingContext = GetTasklistsViewModel();
        }

        private TasklistsViewModel GetTasklistsViewModel()
        {
            var viewModel = new TasklistsViewModel();

            // Add fake model.
            viewModel.Tasklists.Add(new Tasklist
            {
                Title = "First List"
            });
            viewModel.Tasklists.Add(new Tasklist
            {
                Title = "Second List"
            });
            viewModel.Tasklists.Add(new Tasklist
            {
                Title = "Third List"
            });

            return viewModel;
        }
    }
}
