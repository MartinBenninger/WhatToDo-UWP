using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatToDo.Helpers;
using WhatToDo.Models;
using WhatToDo.ViewModels;
using Xamarin.Forms;

namespace WhatToDo.Views
{
    public partial class Tasklists : BaseContentPage
    {
        public Tasklists()
        {
            InitializeComponent();

            if (Device.OS == TargetPlatform.Android)
            {
                // Subscribe the login event for android because the OnAppearing event is not called after PopModalAsync.
                MessagingCenter.Subscribe<Welcome>(this, "LoggedIn", (sender) => BindingContext = GetTasklistsViewModel());
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = GetTasklistsViewModel();
        }

        private TasklistsViewModel GetTasklistsViewModel()
        {
            var viewModel = new TasklistsViewModel();

            // Only set the model data if the user is logged in.
            if (!Settings.IsLoggedIn)
            {
                return viewModel;
            }

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
