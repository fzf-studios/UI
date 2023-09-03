using FZFUI.Interfaces;
using UnityEngine;

namespace FZFUI.Autogen
{
    /// <summary>
    /// Base class for presenters (MVP like pattern).
    /// Has functionality to autogenerate fields of this view (include child views) and assigning it.
    /// Designed to simplify work with UGui views.
    /// Derived class must be partial for autogen work.
    /// </summary>
    public partial class BasePresenter : MonoBehaviour, IView
    {
        public GameObject GameObject => gameObject;

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }
    }
}