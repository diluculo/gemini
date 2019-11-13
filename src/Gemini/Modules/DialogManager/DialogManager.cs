using Caliburn.Micro;
using Gemini.Modules.DialogManager.ViewModels;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace Gemini.Modules.DialogManager
{
    // https://github.com/MahApps/MahApps.Metro/issues/1129

    [Export(typeof(IDialogManager))]
    public class DialogManager : IDialogManager
    {
        /// <summary>
        /// Show the required dialog.
        /// </summary>
        /// <typeparam name="IResult">The type of result to return.</typeparam>
        /// <param name="viewModel">The view model ascociated with the view.</param>
        public IResult Show<IResult>(DialogViewModel<IResult> viewModel)
        {
            IWindowManager manager = new WindowManager();
            manager.ShowDialog(viewModel, null, null);

            return viewModel.DialogResult;
        }

        /// <summary>
        /// Show the required dialog.
        /// </summary>
        /// <param name="viewModel">The view model ascociated with the view.</param>
        public Task<IResult> ShowAsync<IResult>(DialogViewModel<IResult> viewModel)
        {
            var taskSource = new TaskCompletionSource<IResult>();
            try
            {
                var result = Show<IResult>(viewModel);
                taskSource.SetResult(result);
            }
            catch (Exception ex)
            {
                taskSource.SetException(ex);
            }

            return taskSource.Task;
        }
    }
}
