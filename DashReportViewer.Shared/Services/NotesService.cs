using DashReportViewer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DashReportViewer.Shared.Services
{
    public interface INotesService
    {
        Task<string> GetNote(Guid reportId, int cardId);
        Task SaveNotes(Guid reportId, int cardId, string message);
    }

    public class NotesService : INotesService
    {
        readonly DashReportViewerContext context;
        public NotesService(DashReportViewerContext context)
        {
            this.context = context;
        }

        public async Task<string> GetNote(Guid reportId, int cardId)
        {
            string message = "";
            var note = await context.Notes.Where(n => n.ReportId == reportId && n.CardId == cardId).FirstOrDefaultAsync();
            if (note != null)
            {
                message = note.Message;
            }
            return message;
        }

        public async Task SaveNotes(Guid reportId, int cardId, string message)
        {
            var note = await context.Notes.Where(n => n.ReportId == reportId && n.CardId == cardId).FirstOrDefaultAsync();
            if (note != null)
            {
                note.Message = message;
            }
            else
            {
                context.Notes.Add(new Models.Note()
                {
                    CardId = cardId,
                    ReportId = reportId,
                    Message = message
                });
            }
            await context.SaveChangesAsync();
        }
    }
}
