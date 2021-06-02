using GuitarNoteTeacher.UI.ResultsModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace GuitarNoteTeacher.UI.ResultsModule
{
    public class ResultsModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ResultsModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegion region = _regionManager.Regions["ResultsRegion"];

            var view = containerProvider.Resolve<ResultsModuleView>();
            region.Add(view);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}