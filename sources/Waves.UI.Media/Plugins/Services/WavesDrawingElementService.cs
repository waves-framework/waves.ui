using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MTL.UI.Base;
using MTL.UI.Base.Attributes;
using MTL.UI.Base.Enums;
using MTL.UI.Base.Interfaces;
using MTL.UI.Media.Drawing.Interfaces;
using MTL.UI.Media.Plugins.Services.Interfaces;
using MTL.UI.Plugins.Services.Interfaces;

namespace MTL.UI.Media.Plugins.Services
{
    /// <summary>
    /// Service for resolving available drawing elements.
    /// </summary>
    [MtlService(typeof(IMtlDrawingElementService))]
    public class MtlDrawingElementService : MtlConfigurableService, IMtlDrawingElementService
    {
        private readonly IMtlContainerService _containerService;
        private readonly IMtlCore _core;

        /// <summary>
        /// Creates new instance of <see cref="MtlDrawingElementService"/>.
        /// </summary>
        /// <param name="core">Instance of core.</param>
        /// <param name="configurationService">Instance of configuration service.</param>
        /// <param name="containerService">Instance of container service.</param>
        public MtlDrawingElementService(
            IMtlCore core,
            IMtlConfigurationService configurationService,
            IMtlContainerService containerService)
            : base(configurationService)
        {
            _core = core;
            _containerService = containerService;
        }

        /// <summary>
        /// Gets working drawing element name.
        /// </summary>
        [MtlProperty]
        public string WorkingDrawingElementName { get; set; }

        /// <inheritdoc />
        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var availableElements = await _containerService.GetInstanceAsync<IEnumerable<IMtlDrawingElement>>();
            var mtlDrawingElements = availableElements as IMtlDrawingElement[] ?? availableElements.ToArray();

            if (string.IsNullOrEmpty(WorkingDrawingElementName) && mtlDrawingElements.Any())
            {
                WorkingDrawingElementName = mtlDrawingElements.First().Name;
            }

            if (!mtlDrawingElements.Any())
            {
                await _core.WriteLogAsync("Drawing Elements", "There are no drawing elements loaded", this, MessageType.Warning);
            }
        }

        /// <inheritdoc />
        public async Task<IMtlDrawingElement> GetDrawingElement()
        {
            var availableElements = await _containerService.GetInstanceAsync<IEnumerable<IMtlDrawingElement>>();
            var mtlDrawingElements = availableElements as IMtlDrawingElement[] ?? availableElements.ToArray();

            foreach (var element in mtlDrawingElements)
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
