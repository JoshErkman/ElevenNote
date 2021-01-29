using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenNote.WebAPI.Controllers
{
    [Authorize]
    public class NoteController : ApiController
    {
        //GET
        public IHttpActionResult Get()
        {
            NoteService noteService = CreatedNoteService();
            var notes = noteService.GetNotes();
            return Ok(notes);
        }

        public IHttpActionResult Get(int id)
        {
            NoteService noteService = CreatedNoteService();
            var note = noteService.GetNoteById(id);
            return Ok(note);
        }

        //POST
        public IHttpActionResult Post(NoteCreate note)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatedNoteService();

            if (!service.CreateNote(note))
                return InternalServerError();

            return Ok();  
        }

        //PUT
        public IHttpActionResult Put(NoteEdit note)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatedNoteService();

            if (!service.UpdateNote(note))
                return InternalServerError();

            return Ok();
        }

        //DELETE
        public IHttpActionResult Delete(int id)
        {
            var service = CreatedNoteService();

            if (!service.DeleteNote(id))
                return InternalServerError();

            return Ok();
        }

        // Helper Method    
        private NoteService CreatedNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var noteService = new NoteService(userId);
            return noteService;
        }
    }
}
