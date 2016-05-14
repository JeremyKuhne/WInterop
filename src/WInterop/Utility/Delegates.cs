// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Reflection;

namespace WInterop.Utility
{
    public static class Delegates
    {
        public static string DesktopLibrary = "WInterop.Desktop, Version=0.1.0.0, Culture=neutral, PublicKeyToken=1ea30a2edb2b5ea2";
        public static string BaseLibrary = "WInterop, Version=0.1.0.0, Culture=neutral, PublicKeyToken=1ea30a2edb2b5ea2";

        public static T CreateDelegate<T>(string typeName, string methodName) where T : class
        {
            Type type = Type.GetType(typeName);
            Type[] parameters = GetDelegateParameters<T>();
            var method = type.GetRuntimeMethod(methodName, parameters);
            if (method == null)
                throw new NotImplementedException(nameof(methodName));

            return (T)(object)method.CreateDelegate(typeof(T));
        }

        public static Type[] GetDelegateParameters<T>() where T : class
        {
            Type delegateType = typeof(T);
            if (!typeof(Delegate).GetTypeInfo().IsAssignableFrom(delegateType.GetTypeInfo()))
                throw new InvalidOperationException("The type isn't a delegate.");
            foreach (var mi in delegateType.GetRuntimeMethods())
            {
                if (mi.Name == "Invoke")
                    return mi.GetParameters().Select(p => p.ParameterType).ToArray();
            }

            throw new InvalidOperationException("The type isn't a delegate.");
        }
    }
}
