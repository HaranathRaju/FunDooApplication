using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Entities;
using ModelLayer.DTO;
using AutoMapper;

namespace BusinessLogicLayer.Mapping
{
    public class NotesMapper : Profile 
    {
        public NotesMapper() {
            CreateMap<NoteRequest, Notes>();
            CreateMap<UpdateNoteRequest, Notes>();
            CreateMap<Notes, NoteResponse>();
           
        }    
    }
}
