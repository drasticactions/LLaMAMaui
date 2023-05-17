// <copyright file="AppDispatcher.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Services;

namespace LLaMAMaui.Services
{
    public class AppDispatcher : IAppDispatcher
    {
        public bool Dispatch(Action action)
        {
            return App.Current!.Dispatcher.Dispatch(action);
        }
    }
}
