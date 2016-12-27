namespace WhatToDo.DAL.IRepositories
{
    /// <summary>
    /// The interface for the user repository class.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Logs the current user out.
        /// </summary>
        /// <returns>Null</returns>
        System.Threading.Tasks.Task Logout();
    }
}