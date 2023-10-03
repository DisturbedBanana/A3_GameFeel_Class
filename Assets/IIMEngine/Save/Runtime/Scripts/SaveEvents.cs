using System;

namespace IIMEngine.Save
{
    public static class SaveEvents
    {
        public static Action<string> OnKeyChanged { get; set; }
    }
}