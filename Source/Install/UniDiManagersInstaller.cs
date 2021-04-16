
namespace UniDi
{
    // This is installed by default in ProjectContext, however, if you are using UniDi outside
    // of Unity then you might want to call this
    //
    // In this case though, you will have to manually call InitializableManager.Initialize,
    // DisposableManager.Dispose, TickableManager.Tick, etc. when appropriate for the environment
    // you are working in
    //
    // You might also want to use this installer in a UniDiUnitTestFixture
    public class UniDiManagersInstaller : Installer<UniDiManagersInstaller>
    {
        [Inject]
        public UniDiManagersInstaller()
        {
        }

        public override void InstallBindings()
        {
            Container.Bind(typeof(TickableManager), typeof(InitializableManager), typeof(DisposableManager))
                .ToSelf().AsSingle().CopyIntoAllSubContainers();
        }
    }
}

