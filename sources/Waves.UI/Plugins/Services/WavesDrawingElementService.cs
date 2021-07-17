using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Waves.Core.Base;
using Waves.Core.Base.Attributes;
using Waves.Core.Base.Enums;
using Waves.Core.Base.Interfaces;
using Waves.Core.Plugins.Services.Interfaces;
using Waves.UI.Drawing.Interfaces;
using Waves.UI.Plugins.Services.Interfaces;

namespace Waves.UI.Plugins.Services
{
    /// <summary>
    /// Service for resolving available drawing elements.
    /// </summary>
    [WavesService(typeof(IWavesDrawingElementService))]
    public class WavesDrawingElementService : WavesConfigurableService, IWavesDrawingElementService
    {
        private readonly IWavesContainerService _containerService;
        private readonly IWavesCore _core;

        /// <summary>
        /// Creates new instance of <see cref="WavesDrawingElementService"/>.
        /// </summary>
        /// <param name="core">Instance of core.</param>
        /// <param name="configurationService">Instance of configuration service.</param>
        /// <param name="containerService">Instance of container service.</param>
        public WavesDrawingElementService(
            IWavesCore core,
            IWavesConfigurationService configurationService,
            IWavesContainerService containerService)
            : base(configurationService)
        {
            _core = core;
            _containerService = containerService;
        }

        /// <summary>
        /// Gets working drawing element name.
        /// </summary>
        [WavesProperty]
        public string WorkingDrawingElementName { get; set; }

        /// <inheritdoc />
        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var availableElements = await _containerService.GetInstanceAsync<IEnumerable<IWavesDrawingElement>>();
            var wavesDrawingElements = availableElements as IWavesDrawingElement[] ?? availableElements.ToArray();

            if (string.IsNullOrEmpty(WorkingDrawingElementName) && wavesDrawingElements.Any())
            {
                WorkingDrawingElementName = wavesDrawingElements.First().Name;
            }

            if (!wavesDrawingElements.Any())
            {
                await _core.WriteLogAsync("Drawing Elements", "There are no drawing elements loaded", this, WavesMessageType.Warning);
            }
        }

        /// <inheritdoc />
        public async Task<IWavesDrawingElement> GetDrawingElement()
        {
            var availableElements = await _containerService.GetInstanceAsync<IEnumerable<IWavesDrawingElement>>();
            var wavesDrawingElements = availableElements as IWavesDrawingElement[] ?? availableElements.ToArray();

            foreach (var element in wavesDrawingElements)
            {
                if (element.Name != WorkingDrawingElementName)
                {
                    continue;
                }

                await element.InitializeAsync();
                return element;
            }

            return null;
        }

        /// <inheritdoc />
        public void SetWorkingDrawingElementName(string name)
        {
            WorkingDrawingElementName = name;
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // TODO: your code for release managed resources.
            }

            // TODO: your code for release unmanaged resources.
        }
    }
}
