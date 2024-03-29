﻿using NetStateMachine.SampleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace NetStateMachine.SampleApp
{
    public class ViewModelResolver
    {
        private readonly List<string> viewsSuffixes = new List<string>
        {
            "View",
            "Window"
        };

        public Type Resolve(Assembly viewModelsAssembly, Type viewType)
        {
            var viewName = viewType.Name;
            var viewModelName = GetViewModelName(viewName);

            foreach (var type in viewModelsAssembly.GetTypes())
            {
                if (type.Name == viewModelName)
                {
                    return viewModelsAssembly.GetType(type.FullName, true);
                }
            }

            throw new Exception($"Can not find {viewModelName} in {viewModelsAssembly.GetName().FullName}");
        }

        private string GetViewModelName(string viewName)
        {
            foreach (var suffix in viewsSuffixes)
            {
                if (viewName.EndsWith(suffix))
                {
                    viewName = viewName.Replace(suffix, string.Empty);
                    break;
                }
            }

            return viewName + "ViewModel";
        }
    }
}