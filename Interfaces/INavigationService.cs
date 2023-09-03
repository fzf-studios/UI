using FZFUI.Autogen;

namespace FZFUI.Interfaces
{
    public interface INavigationService
    {
        void BindInstance<TView>(TView instance, bool overrideInstance = false) where TView : class, IView;
        TView Get<TView>() where TView : class, IView;
        void Show<TView>() where TView : class, IView;
        void Close<TView>() where TView : class, IView;
    }
}