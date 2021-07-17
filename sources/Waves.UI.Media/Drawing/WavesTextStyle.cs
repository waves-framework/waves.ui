﻿using System.Collections.Generic;
using MTL.UI.Media.Drawing.Enums;
using MTL.UI.Media.Drawing.Interfaces;

namespace MTL.UI.Media.Drawing
{
    /// <summary>
    ///     Text style.
    /// </summary>
    public class MtlTextStyle : IMtlTextStyle
    {
        /// <summary>
        ///     Gets font weight dictionary.
        /// </summary>
        public static readonly Dictionary<string, int> FontWeightMapping = new Dictionary<string, int>
        {
            { "Thin", 100 },
            { "Extra Light", 200 },
            { "Light", 300 },
            { "Regular", 400 },
            { "Medium", 500 },
            { "Semi Bold", 600 },
            { "Bold", 700 }, { "BoldMT", 700 },
            { "Extra Bold", 800 },
            { "Black", 900 },
        };

        /// <inheritdoc />
        public float FontSize { get; set; } = 12;

        /// <inheritdoc />
        public string FontFamily { get; set; } = "Segoe UI";

        /// <inheritdoc />
        public int Weight { get; set; } = FontWeightMapping["Regular"];

        /// <inheritdoc />
        public bool IsSubpixelText { get; set; } = true;

        /// <inheritdoc />
        public MtlTextAlignment Alignment { get; set; } = MtlTextAlignment.Left;
    }
}
