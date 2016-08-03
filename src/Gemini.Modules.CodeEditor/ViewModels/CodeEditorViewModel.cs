using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Framework.Threading;
using Gemini.Modules.CodeEditor.Properties;
using Gemini.Modules.CodeEditor.Views;
using Gemini.Modules.StatusBar;
using Gemini.Modules.UndoRedo;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Utils;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Gemini.Modules.CodeEditor.ViewModels
{
    // TODO:
    // 1. Grouping simple Undos
    // 2. Adding edit commands(Cut, Copy, Paste, Delete, Select All) in menubar and context menu
    // 3. Add IsReadOnly property
    // 4. Add TextAreaOption properties
    // 5. Highlighting line where caret is
    // 6. Add FileSystemWatcher function
    
    [DisplayName("Code Editor")]
    [Export(typeof(CodeEditorViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
#pragma warning disable 659
    public class CodeEditorViewModel : PersistedDocument
#pragma warning restore 659
    {
        private ICodeEditorView _view;
        private IStatusBar _statusBar;
        private TextArea _textArea;

        private readonly LanguageDefinitionManager _languageDefinitionManager;

        #region TextContent
        
        // source: http://www.codeproject.com/Articles/570313/AvalonDock-Tutorial-Part-AvalonEdit-in-Avalo
        private TextDocument _document = null;
        /// <summary>
        /// wraps the document class provided by AvalonEdit.
        /// </summary>
        public TextDocument Document
        {
            get
            {
                return _document;
            }

            set
            {
                if (_document != value)
                {
                    _document = value;
                    NotifyOfPropertyChange(() => Document);
                }
            }
        }

        #endregion

        #region HighlightingDefinition

        private object lockThis = new object();
        private IHighlightingDefinition _highlightingDefinition = null;
        public IHighlightingDefinition HighlightingDefinition
        {
            get
            {
                lock (lockThis)
                {
                    return _highlightingDefinition;
                }
            }

            set
            {
                lock (lockThis)
                {
                    if (_highlightingDefinition != value)
                    {
                        _highlightingDefinition = value;

                        NotifyOfPropertyChange(() => HighlightingDefinition);
                    }
                }
            }
        }

        #endregion

        [ImportingConstructor]
        public CodeEditorViewModel(LanguageDefinitionManager languageDefinitionManager)
        {
            _languageDefinitionManager = languageDefinitionManager;
        }

        protected override void OnViewLoaded(object view)
        {
            _view = (ICodeEditorView) view;
            _textArea = _view.TextEditor.TextArea;
            _textArea.Caret.PositionChanged += Caret_PositionChanged;

            _statusBar = IoC.Get<IStatusBar>();
        }       

        public override bool Equals(object obj)
        {
            var other = obj as CodeEditorViewModel;
            return other != null
                && string.Equals(FilePath, other.FilePath, StringComparison.InvariantCultureIgnoreCase)
                && string.Equals(FileName, other.FileName, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override Task DoNew()
        {
            IsDirty = false;
            Document = new TextDocument();
            InitInstance();
            return TaskUtility.Completed;
        }

        protected override Task DoLoad(string filePath)
        {
            // Check file attributes and set to read-only if file attributes indicate that
            if ((System.IO.File.GetAttributes(filePath) & FileAttributes.ReadOnly) != 0)
            {
                // IsReadOnly = true;
            }

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (StreamReader reader = FileReader.OpenStream(fs, Encoding.UTF8))
                    {
                        TextDocument doc = new TextDocument(reader.ReadToEnd());
                        Document = doc;
                    }
                }
            }
            catch // File may be blocked by another process
            {
                // Try read-only shared method and set file access to read-only
            }

            InitInstance();

            return TaskUtility.Completed;
        }

        protected override Task DoSave(string filePath)
        {
            try
            {
                File.WriteAllText(filePath, Document.Text);
            }
            catch (Exception)
            {
                throw;
            }

            IsDirty = false;
            return TaskUtility.Completed;
        }

        private void InitInstance()
        {
            var fileExtension = Path.GetExtension(FileName).ToLower();
            ILanguageDefinition languageDefinition = _languageDefinitionManager.GetDefinitionByExtension(fileExtension);
            SetLanguage(languageDefinition);

            Document.Changed += Document_Changed;
        }
        
        private void SetLanguage(ILanguageDefinition languageDefinition)
        {
            HighlightingDefinition = (languageDefinition != null)
                ? languageDefinition.SyntaxHighlighting
                : null;
        }

        private void Caret_PositionChanged(object sender, EventArgs e)
        {
            // TODO: HighloghtBrackets

            // Update Column and Line position properties when caret position is changed
            int lineNumber;
            int colPosition;

            if (_textArea != null)
            {
                lineNumber = _textArea.Caret.Line; // _view.TextEditor.Document.GetLineByOffset(_view.TextEditor.CaretOffset).LineNumber;
                colPosition = _textArea.Caret.Column; // _view.TextEditor.TextArea.Caret.VisualColumn + 1;
            }
            else
            {
                lineNumber = 0;
                colPosition = 0;
            }
            // TODO: Now I don't know about Ch#
            //int charPosition = _view.TextEditor.CaretOffset;
            if (_statusBar != null && _statusBar.Items.Count >= 3)
            {
                _statusBar.Items[1].Message = string.Format("Ln {0}", lineNumber);
                _statusBar.Items[2].Message = string.Format("Col {0}", colPosition);
            }
        }        
        
        private void Document_Changed(object sender, DocumentChangeEventArgs e)
        {
            TextDocument _document = sender as TextDocument;
            if (UndoRedoManager.AcceptChanges) // false while an undo action is running
            {
                UndoRedoManager.UndoStack.Add(new DocumentChangeAction(_document, e));
            }
        }

        private class DocumentChangeAction : IUndoableAction
        {
            TextDocument _document;
            DocumentChangeEventArgs _change;

            public DocumentChangeAction(TextDocument document, DocumentChangeEventArgs change)
            {
                _document = document;
                _change = change;
            }

            // Name: Insert New Line / Paste / Insert Text / Cut Selection / Delete Text / AutoIndent(*)
            public string Name
            {
                get
                {
                    if (_change.InsertionLength > 0)
                    {
                        if (string.Equals(_change.InsertedText.Text, Environment.NewLine))
                            return Resources.InsertNewLine;
                        else if (string.Equals(_change.InsertedText.Text, Clipboard.GetData(DataFormats.Text)))
                            return Resources.Paste;
                        else
                            return Resources.InsertText;
                    }
                    else 
                    {
                        if (string.Equals(_change.RemovedText.Text, Clipboard.GetData(DataFormats.Text)))
                            return Resources.CutSelection;
                        else
                            return Resources.DeleteText;
                    }
                }
            }

            public void Execute()
            {
                _document.Replace(_change.Offset, _change.RemovalLength, _change.InsertedText, _change.OffsetChangeMap);
            }

            public void Undo()
            {
                OffsetChangeMap map = _change.OffsetChangeMap;
                _document.Replace(_change.Offset, _change.InsertionLength, _change.RemovedText, map != null ? map.Invert() : null);
            }
        }
    }
}