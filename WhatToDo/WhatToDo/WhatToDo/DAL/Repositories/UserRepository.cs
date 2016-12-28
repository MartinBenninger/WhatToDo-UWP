namespace WhatToDo.DAL.Repositories
{
    using Helpers;
    using IRepositories;
    using Services.TasksAPI;

    /// <summary>
    /// The user repository class.
    /// </summary>
    /// <seealso cref="WhatToDo.DAL.IRepositories.IUserRepository"/>
    public class UserRepository : IUserRepository
    {
        private readonly OnlineData onlineData = new OnlineData();

        /// <summary>
        /// Logs the current user out.
        /// </summary>
        /// <returns>Null</returns>
        public async System.Threading.Tasks.Task Logout()
        {
            await this.onlineData.RevokeRefreshToken(Settings.RefreshToken);

            Settings.RefreshToken = string.Empty;
            Settings.AccessToken = string.Empty;
        }
    }
}