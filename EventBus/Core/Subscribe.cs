//
//  Subscribe.cs
//  EventBus
//
//  Created by LunarEclipse on 2018-11-29.
//  Copyright © 2018 LunarEclipse. All rights reserved.
//

using System;

namespace Sakura.Core.Event
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
        public Subscribe()
        {

        }

        ~Subscribe()
        {
            Console.WriteLine("Subscribe Deinit");
        }
    }
}