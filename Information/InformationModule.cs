using GuitarNoteTeacher.UI.InformationModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace GuitarNoteTeacher.UI.InformationModule
{
    public class InformationModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public InformationModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegion region = _regionManager.Regions["InformationRegion"];

            var view = containerProvider.Resolve<InformationModuleView>();
            region.Add(view);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}