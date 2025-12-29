using DataLogicLayer.Context;
using DataLogicLayer.Interfaces;
using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLogicLayer.Repositories
{
    public class LabelRepository : ILabelRepository
    {
        private readonly FundooContext _context;

        public LabelRepository(FundooContext context)
        {
            _context = context;
        }

        public Label AddLabel(Label label)
        {
            _context.Labels.Add(label);
            _context.SaveChanges();
            return label;
        }

        public IEnumerable<Label> GetLabelsByUserId(Guid userId)
        {
            return _context.Labels.Where(l => l.UserId == userId).ToList();
        }

        public bool DeleteLabel(Guid labelId, Guid userId)
        {
            var label = _context.Labels
                .FirstOrDefault(l => l.LabelId == labelId && l.UserId == userId);

            if (label == null)
                return false;

            _context.Labels.Remove(label);
            _context.SaveChanges();
            return true;
        }

        public bool AddLabelToNote(Guid noteId, Guid labelId)
        {
            var exists = _context.NoteLabels
                .Any(nl => nl.NoteId == noteId && nl.LabelId == labelId);

            if (exists)
                return false;

            _context.NoteLabels.Add(new NoteLabel
            {
                NoteId = noteId,
                LabelId = labelId
            });

            _context.SaveChanges();
            return true;
        }

        public bool RemoveLabelFromNote(Guid noteId, Guid labelId)
        {
            var noteLabel = _context.NoteLabels
                .FirstOrDefault(nl => nl.NoteId == noteId && nl.LabelId == labelId);

            if (noteLabel == null)
                return false;

            _context.NoteLabels.Remove(noteLabel);
            _context.SaveChanges();
            return true;
        }

        public Label GetLabelByName(string labelName, Guid userId)
        {
            return _context.Labels.FirstOrDefault(l => l.LabelName == labelName && l.UserId == userId);
        }
    }
}
