namespace Infrastructure
{
    using Zenject;
    using Input;

    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SwipeInput>().AsSingle();
            Container.Bind<IInputService>().To<InputService>().AsSingle();
        }
    }
}