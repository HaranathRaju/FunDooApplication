using BusinessLogicLayer.Interfaces;
using DataLogicLayer.Interfaces;
using ModelLayer.DTO;
using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Services
{
    public class LabelService : ILabelService
    {
        private readonly ILabelRepository _repository;

        public LabelService(ILabelRepository repository)
        {
            _repository = repository;
        }

        public LabelResponse AddLabel(LabelRequest request, Guid userId)
        {

            var label = _repository.GetLabelByName(request.LabelName, userId);

            if (label == null)
            {
  
                label = new Label
                {
                    LabelId = Guid.NewGuid(),
                    LabelName = request.LabelName,
                    UserId = userId
                };

                label = _repository.AddLabel(label);
            }

            if (request.NoteId != Guid.Empty)
            {
                _repository.AddLabelToNote(request.NoteId, label.LabelId);
            }


            return new LabelResponse
            {
                LabelId = label.LabelId,
                LabelName = label.LabelName
            };
        }

        public IEnumerable<LabelResponse> GetLabels(Guid userId)
        {
            return _repository.GetLabelsByUserId(userId)
                .Select(l => new LabelResponse
                {
                    LabelId = l.LabelId,
                    LabelName = l.LabelName
                });
        }

        public bool DeleteLabel(Guid labelId, Guid userId)
        {
            return _repository.DeleteLabel(labelId, userId);
        }

        public bool AddLabelToNote(Guid noteId, Guid labelId)
        {
            return _repository.AddLabelToNote(noteId, labelId);
        }

        public bool RemoveLabelFromNote(Guid noteId, Guid labelId)
        {
            return _repository.RemoveLabelFromNote(noteId, labelId);
        }
    }
}
