//
//  DictionaryExtension.cs
//  EventBus
//
//  Created by LunarEclipse on 2018-11-30.
//  Copyright © 2018 LunarEclipse. All rights reserved.
//

using System.Collections.Generic;

namespace EventBus.Extension
{
    internal static class DictionaryExtension
    {
        public static Value Get<Key, Value>(this Dictionary<Key, Value> dict, Key key)
        {
            if (dict == null || !dict.ContainsKey(key))
                return default(Value);
            return dict[key];
        }
    }
}