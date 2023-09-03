using System;
using System.Collections.Generic;
using FZFUI.Autogen;
using FZFUI.Interfaces;

namespace FZFUI.Management
{
    /// <summary>
    /// Container of views to allow from any interested controller manipulate needed views.
    /// It must have only complex views (like presenters) that has only 1 instance at game.
    /// If presenters creates dynamically through factory - it must take care about previous instance (destroy it) and bind new with override parameter set to true.
    /// </summary>
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<Type, IView> _views = new();

        public void BindInstance<TView>(TView instance, bool overrideInstance = false) where TView : class, IView
        {
            if (_views.ContainsKey(typeof(TView)) && !overrideInstance)
                throw new InvalidOperationException("Attempt to override view instance " +
                                                    "while overrideInstance parameter was false." +
                                                    $"\nView type: {typeof(TView)}");
            _views[typeof(TView)] = instance;
        }

        public TView Get<TView>() where TView : class, IView
        {
            return _views[typeof(TView)] as TView;
        }

        public void Show<TView>() where TView : class, IView
        {
            var view = Get<TView>();
            view.Show();
        }

        public void Close<TView>() where TView : class, IView
        {
            var view = Get<TView>();
            view.Close();
        }
    }
}