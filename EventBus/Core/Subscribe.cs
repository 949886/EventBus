//
//  Subscribe.cs
//  EventBus
//
//  Created by LunarEclipse on 2018-11-29.
//  Copyright © 2018 LunarEclipse. All rights reserved.
//

using System;

namespace Event
{
    [AttributeUsage(AttributeTargets.Class |
                    AttributeTargets.Constructor |
                    AttributeTargets.Field |
                    AttributeTargets.Method |
                    AttributeTargets.Property,
        Inherited = false,
        AllowMultiple = true)]
    public class Subscribe : Attribute
    {
        public readonly ThreadMode threadMode = ThreadMode.DEFAULT;

        public Subscribe() {}

        public Subscribe(ThreadMode threadMode)
        {
            this.threadMode = threadMode;
        }

        ~Subscribe()
        {
            Console.WriteLine("Subscribe Deinit");
        }
    }

    /// <summary>
    /// Class using to store info of subscriber.
    /// </summary>
    public struct Subscription
    {
        public object Subscriber => subscriber.Target;
        public Type SubscribeType => subscribeType;
        public Subscribe Subscribe => subscribe;
        public Delegate Handler => handler;

        private WeakReference subscriber;
        private Type subscribeType;
        private Subscribe subscribe;
        private Delegate handler;

        public Subscription(object subscriber, Type subscribeType, Subscribe subscribe, Delegate handler)
        {
            this.subscriber = new WeakReference(subscriber);
            this.subscribeType = subscribeType;
            this.subscribe = subscribe;
            this.handler = handler;
        }
    }
}