using System;
using JUtils;

namespace DefaultNamespace
{
    public class EventBus : AutoSingletonBehaviour<EventBus>
    {
        public Action onOutOfBalance;
    }
}