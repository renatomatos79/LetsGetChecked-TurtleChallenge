using System;
using System.Collections.Generic;
using System.Text;
using TurtleChallenge.Enums;

namespace TurtleChallenge.Helper
{
    public class EngineOperationHelper
    {
        public static EngineOperation? From(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            switch (value.ToUpper())
            { 
                case "T": return EngineOperation.Turn;
                case "M": return EngineOperation.Move;
                case "P": return EngineOperation.Play;
                default: return null;
            }
        }
    }
}
