using SDTCore;
using Zenject;


namespace SDTBootstrapService 
{
    public class BootInstallerDemo : MonoInstaller<BootInstallerDemo>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IBootstrap>()
                .To<Bootstrap>()
                .AsSingle();

            Container
                .Bind<CommandStorage>()
                .AsSingle();
        }
    }
}