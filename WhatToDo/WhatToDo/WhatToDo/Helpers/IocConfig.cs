namespace WhatToDo.Helpers
{
    using DAL.IRepositories;
    using DAL.Repositories;
    using XLabs.Ioc;

    /// <summary>
    /// Inversion of control configuration class.
    /// </summary>
    public static class IocConfig
    {
        /// <summary>
        /// Sets up the inversion of control configuration.
        /// </summary>
        public static void SetIoc()
        {
            var resolverContainer = new SimpleContainer();

            resolverContainer.Register<ITaskListRepository>(typeof(TaskListRepository));
            resolverContainer.Register<ITaskRepository>(typeof(TaskRepository));
            resolverContainer.Register<IUserRepository>(typeof(UserRepository));

            Resolver.SetResolver(resolverContainer.GetResolver());
        }
    }
}