using GuitarNoteTeacher.UI.MainModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace GuitarNoteTeacher.UI.MainModule
{
    public class MainModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public MainModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegion region = _regionManager.Regions["MainGameRegion"];

            var view = containerProvider.Resolve<MainModuleView>();
            region.Add(view);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}