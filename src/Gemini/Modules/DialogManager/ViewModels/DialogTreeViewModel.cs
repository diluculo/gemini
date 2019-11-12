using Caliburn.Micro;
using System.Collections.Generic;

namespace Gemini.Modules.DialogManager.ViewModels
{
    public class DialogTreeViewModel : PropertyChangedBase
    {
        public DialogTreeViewModel()
        {
            Children = new List<DialogTreeViewModel>();
        }

        public string Name { get; set; }
        public List<DialogTreeViewModel> Children { get; private set; }
    }
}
