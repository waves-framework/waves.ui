using System;
using System.Collections.Generic;
using Waves.Core.Base.Interfaces;
using Waves.Core.Base.Interfaces.Services;
using Waves.UI.Drawing.Base.Interfaces;

namespace Waves.UI.Drawing.Services.Interfaces
{
    /// <summary>
    ///     Interface for drawing service.
    /// </summary>
    public interface IDrawingService : IMefLoaderService<IDrawingEngine>
    {
        /// <summary>
        ///     Gets or sets current drawing engine.
        /// </summary>
        IDrawingEngine CurrentEngine { get; set; }
        
        /// <summary>
        ///     Event for engine changed handing.
        /// </summary>
        event EventHandler EngineChanged;
    }
}