namespace WhatToDo.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using IRepositories;
    using Models;

    /// <summary>
    /// The task list repository class.
    /// </summary>
    /// <seealso cref="WhatToDo.DAL.IRepositories.ITasklistRepository"/>
    public class TasklistRepository : ITasklistRepository
    {
        /// <summary>
        /// Gets all task lists.
        /// </summary>
        /// <returns>A list of all task lists.</returns>
        public List<Tasklist> GetAllTasklists()
        {
            var tasklists = new List<Tasklist>();

            // Add fake model.
            tasklists.Add(new Tasklist
            {
                Title = "First List"
            });
            tasklists.Add(new Tasklist
            {
                Title = "Second List"
            });
            tasklists.Add(new Tasklist
            {
                Title = "Third List"
            });

            return tasklists;
        }
    }
}