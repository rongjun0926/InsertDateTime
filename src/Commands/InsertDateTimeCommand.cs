using System;
using System.Linq;
using Community.VisualStudio.Toolkit;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Task = System.Threading.Tasks.Task;

namespace InsertDateTime
{
    [Command(PackageGuids.guidInsertDateTimeCmdSetString, PackageIds.cmdInsertDateTime)]
    public class InsertDateTimeCommand : BaseCommand<InsertDateTimeCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            DocumentView docView = await VS.Documents.GetActiveDocumentViewAsync();
            NormalizedSnapshotSpanCollection selections = docView.TextView?.Selection.SelectedSpans;

            if (selections == null)
                return;

            using (ITextEdit edit = docView.TextBuffer.CreateEdit())
            {
                var guid = DateTime.Now.ToString("yyyyMMddHH:mm:ss");

                foreach (SnapshotSpan selection in selections.Reverse())
                {
                    edit.Replace(selection, guid);
                }

                edit.Apply();
            }
        }
    }
}
