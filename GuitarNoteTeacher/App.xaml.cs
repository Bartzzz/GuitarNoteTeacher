using GuitarNoteTeacher.Core.Services.NotesService;
using GuitarNoteTeacher.Core.Services.PointsService;
using GuitarNoteTeacher.Core.Services.TimerService;
using GuitarNoteTeacher.UI.Core;
using GuitarNoteTeacher.UI.Dialogs;
using GuitarNoteTeacher.UI.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Windows;
using System.Windows.Controls;


namespace GuitarNoteTeacher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<GameOverDialog, GameOverDialogViewModel>();
            containerRegistry.Register<INotesService, NotesService>();
            containerRegistry.RegisterSingleton<IPointsService, PointsService>();
            containerRegistry.RegisterSingleton<ITimerService, TimerService>();

        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(StackPanel),
                Container.Resolve<StackPanelRegionAdapter>());
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<UI.MainModule.MainModule>();
            moduleCatalog.AddModule<UI.ResultsModule.ResultsModule>();
            moduleCatalog.AddModule<UI.InformationModule.InformationModule>();
        }
    }
}
