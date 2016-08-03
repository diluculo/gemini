using System.Windows.Controls;
using System.Windows.Input;
using ICSharpCode.AvalonEdit;

namespace Gemini.Modules.CodeEditor.Views
{
    public partial class CodeEditorView : UserControl, ICodeEditorView
    {   
        public TextEditor TextEditor
        {
            get { return CodeEditor; }
        }

        public CodeEditorView()
        {
            InitializeComponent();
            Loaded += (sender, e) => MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

            // Ignore intrinsic Undo/Redo shortcuts of AvalonEdit.
            // TODO: Better logic is needed.
            this.TextEditor.TextArea.CommandBindings.RemoveAt(1); // Redo
            this.TextEditor.TextArea.CommandBindings.RemoveAt(0); // Undo
        }
    }
}
