﻿#region Using

using System;

#endregion

namespace C4rm4x.Services.Framework
{
    /// <summary>
    /// Flags the underlying class as a transformer
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TransformerAttribute : Attribute
    {
    }
}
