using System;
using System.Collections;
using System.Collections.Generic;

namespace _Project.Develop.Runtime.Utilities.ConfigManagement
{
    public interface IConfigLoader
    {
        IEnumerator LoadAsync(Action<Dictionary<Type, object>> callback);
    }
}