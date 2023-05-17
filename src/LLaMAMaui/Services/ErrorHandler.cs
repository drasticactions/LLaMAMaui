// <copyright file="ErrorHandler.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Services;

namespace LLaMAMaui.Services
{
    public class ErrorHandler : IErrorHandlerService
    {
        public void HandleError(Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
            System.Diagnostics.Debugger.Break();
        }
    }
}
