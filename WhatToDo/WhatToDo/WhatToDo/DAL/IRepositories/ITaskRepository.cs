namespace WhatToDo.DAL.IRepositories
{
    using System.Collections.Generic;
    using Google.Apis.Tasks.v1.Data;

    /// <summary>
    /// The interface for the task repository class.
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        /// Gets all task lists.
        /// </summary>
        /// <param name="taskListId">The task list identifier.</param>
        /// <returns>A list of all task lists.</returns>
        System.Threading.Tasks.Task<List<Task>> GetAllTasksFromTaskList(string taskListId);

        /// <summary>
        /// Inserts the task.
        /// </summary>
        /// <param name="taskList">The task list to insert the task into.</param>
        /// <param name="task">The task to insert.</param>
        /// <returns>An awaitable System.Threading.Tasks.Task.</returns>
        System.Threading.Tasks.Task InsertTask(TaskList taskList, Task task);

        /// <summary>
        /// Updates the task.
        /// </summary>
        /// <param name="taskList">The task list containing the task.</param>
        /// <param name="task">The task to update.</param>
        /// <returns>An awaitable System.Threading.Tasks.Task.</returns>
        System.Threading.Tasks.Task UpdateTask(TaskList taskList, Task task);

        /// <summary>
        /// Deletes the task.
        /// </summary>
        /// <param name="taskList">The task list containing the task.</param>
        /// <param name="task">The task to delete.</param>
        /// <returns>An awaitable System.Threading.Tasks.Task.</returns>
        System.Threading.Tasks.Task DeleteTask(TaskList taskList, Task task);
    }
}