//
//  EventBus.cs
//  EventBus
//
//  Created by LunarEclipse on 2018-11-29.
//  Copyright © 2018 LunarEclipse. All rights reserved.
//

using System;
using System.Collections.Generic;
using System.Reflection;
using EventBus.Extension;

namespace Sakura.Core.Event
{
    public class EventBus
    {
        public static EventBus Default = new EventBus();

        private Dictionary<Type, List<Delegate>> eventHandlers = new Dictionary<Type, List<Delegate>>();

        public EventBus() { }

        ~EventBus() { }

        public void Register(object listener)
        {
            bool hasAttribute = false;

            // Check [Subscribe] attributes of listener.
            foreach (MethodInfo method in listener.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                foreach (Attribute attribute in method.GetCustomAttributes(true))
                {
                    Console.WriteLine("Register: " + method);

                    Subscribe subscribeInfo = attribute as Subscribe;
                    if (subscribeInfo == null)
                        continue;
                    hasAttribute = true;

                    var parameters = method.GetParameters();
                    if (parameters.Length == 1)
                    {
                        var action = method.CreateDelegate(listener);

                        if (!eventHandlers.ContainsKey(parameters[0].ParameterType))
                            eventHandlers[parameters[0].ParameterType] = new List<Delegate>();
                        eventHandlers[parameters[0].ParameterType].Add(action);

                    }
                    else throw new Exception("Subscribe method must have only one parameter!");
                }

            if (!hasAttribute)
                throw new Exception("Subscribe method not found!");
        }

        public void Unregister(object listener)
        {
            foreach (MethodInfo method in listener.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                foreach (Attribute attribute in method.GetCustomAttributes(true))
                {
                    Console.WriteLine("Unregister: " + method);

                    var parameters = method.GetParameters();
                    if (parameters.Length == 1)
                    {
                        var action = method.CreateDelegate(listener);

                        if (eventHandlers.ContainsKey(parameters[0].ParameterType))
                            eventHandlers[parameters[0].ParameterType].Remove(action);
                    }
                    else throw new Exception("Subscribe method must have only one parameter!");
                }
        }

        public void UnregisterAll()
        {
            eventHandlers.Clear();
        }

        public void Post(object newEvent)
        {
            var delegates = eventHandlers.Get(newEvent.GetType());

            if (delegates == null)
                return;
            
            foreach (Delegate del in delegates)
            {
                try { del.DynamicInvoke(newEvent); }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        

    }
    
}
