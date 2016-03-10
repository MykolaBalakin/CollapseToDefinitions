using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.CollapseToDefinitions {
    [TextViewRole("DOCUMENT")]
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("CSharp")]
    class CollapseToDefinitions : IWpfTextViewCreationListener {
        public void TextViewCreated(IWpfTextView textView) {
            textView.GotAggregateFocus += TextView_GotAggregateFocus;
            Trace.TraceInformation($"Created WpfTextView with content type {textView.TextBuffer.ContentType.TypeName}");
        }

        private void TextView_GotAggregateFocus(Object sender, EventArgs e) {
            var textView = sender as IWpfTextView;
            if (textView != null) {
                textView.GotAggregateFocus -= TextView_GotAggregateFocus;
            }
            InvokeCommand("Edit.CollapsetoDefinitions");
        }

        private void InvokeCommand(String commandName) {
            var dte = Package.GetGlobalService(typeof(DTE)) as DTE;
            Trace.TraceInformation($"Invoking command {commandName}");
            dte?.ExecuteCommand(commandName);
        }
    }
}
