using Gemini.Modules.DialogManager.ViewModels;
using System.Threading.Tasks;

namespace Gemini.Modules.DialogManager
{
    /// <summary>
    /// Contract that should be inherited for other custom DialogManagers.
    /// </summary>
    public interface IDialogManager
    {
        /// <summary>
        /// Show a dialog that performs a Task with generic return type. 
        /// </summary>
        /// <typeparam name="TResult">The result to be returned from the dialog task.</typeparam>
        /// <param name="viewModel">The DialogViewModel type to be displayed.</param>
        /// <returns>The Task to be awaited.</returns>
        IResult Show<IResult>(DialogViewModel<IResult> viewModel);
        
        /// <summary>
        /// Show a dialog that performs as Task.
        /// </summary>
        /// <param name="viewModel">The result to be returned from the dialog task.</param>
        /// <returns>The Task to be awaited.</returns>
        Task<IResult> ShowAsync<IResult>(DialogViewModel<IResult> viewModel);
    }
}
