//
//  MethodInfoExtension.cs
//  EventBus
//
//  Created by LunarEclipse on 2018-11-30.
//  Copyright © 2018 LunarEclipse. All rights reserved.
//

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EventBus.Extension
{
    internal static class MethodInfoExtension
    {
        //Reference: https://stackoverflow.com/questions/940675/getting-a-delegate-from-methodinfo
        internal static Delegate CreateDelegate(this MethodInfo method, object firstArgument = null)
        {
            Func<Type[], Type> getType;
            var types = method.GetParameters().Select(p => p.ParameterType);
            var isAction = method.ReturnType.Equals((typeof(void)));
            if (isAction)
                getType = Expression.GetActionType;
            else
            {
                getType = Expression.GetFuncType;
                types = types.Concat(new[] { method.ReturnType });
            }

            if (firstArgument == null)
                return Delegate.CreateDelegate(getType(types.ToArray()), method);
            return Delegate.CreateDelegate(getType(types.ToArray()), firstArgument, method);
        }
    }
}
