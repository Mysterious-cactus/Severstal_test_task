using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Data
{
    public class DbActions
    {
        private static DbActions _db;
        private readonly DataContext _context;
        private static object _syncRoot = new();
        private DbActions() 
        {
            _context = new DataContext();
            if(_context.Database.EnsureCreated())
            {
                _db.AddFirstNote();
            }
            //_context.Database.Migrate();
        }

        public static DbActions GetDb()
        {
            if (_db == null)
            {
                lock (_syncRoot)
                {
                    if (_db == null)
                    {
                        _db = new DbActions();
                    }
                }
            }
            return _db;
        }

        public Task<List<Note>> GetNotesAsync()
        {
            var notes = _context.Notes.ToListAsync();
            return notes;
        }

        public async Task<Note> GetNoteByIdAsync(int noteId)
        {
            var note = await _context.Notes
                .Where(x => x.Id == noteId)
                .FirstOrDefaultAsync();
            return note;
        }

        public async Task CreateUpdateNoteAsync(Note note)
        {
            if (note.Id != 0)
            {
                _context.Notes.Update(note);
                await _context.SaveChangesAsync();
            }
            else
            {
                await _context.Notes.AddAsync(note);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteNoteAsync(Note note)
        {
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }

        public void AddFirstNote()
        {
            _context.Add(new Note { Text = "First note", Date = DateTimeOffset.UtcNow });
            _context.SaveChanges();
        }
    }
}
