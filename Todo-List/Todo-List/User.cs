﻿// User.cs
// <copyright file="User.cs"> This code is protected under the MIT License. </copyright>
using System.Collections.Generic;
using System.Linq;

namespace Todo_List
{
    /// <summary>
    /// A class containing data that represents a user and their notes.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes members of the <see cref="User" /> class.
        /// </summary>
        public User()
        {
            Name = string.Empty;
            Password = string.Empty;
            Notes = new List<Note>();
        }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the list of notes stored for the user.
        /// </summary>
        public List<Note> Notes { get; set; }

        /// <summary>
        /// Checks all notes have a category.
        /// </summary>
        public void CheckCategorys()
        {
            for (int i = 0; i < Notes.Count; i++)
            {
                if (Notes[i].Categories.Length == 0)
                {
                    Notes[i].Categories = new string[1] { "UNCATEGORISED" };
                }
            }
        }

        /// <summary>
        /// Gets all the notes with certain category tags.
        /// </summary>
        /// <param name="categoryNames"> The list of categories. </param>
        /// <returns> The list of notes under the category. </returns>
        public List<Note> GetNotesByCategory(string[] categoryNames)
        {
            IEnumerable<Note> notesUnderCategories = Notes;
            foreach (string catergory in categoryNames)
            {
                notesUnderCategories = notesUnderCategories.Where(n => n.Categories.Contains(catergory.ToUpper()));
            }

            return notesUnderCategories.ToList();
        }

        /// <summary>
        /// Gets all notes under all categories.
        /// </summary>
        /// <returns> A dictionary of category to a list of notes. </returns>
        public Dictionary<string, List<Note>> GetAllCategories()
        {
            // Get all category names
            List<string> categories = new List<string>();
            foreach (Note n in Notes)
            {
                foreach (string category in n.Categories)
                {
                    if (!categories.Contains(category))
                    {
                        categories.Add(category);
                    }
                }
            }

            // Get each note for each category
            Dictionary<string, List<Note>> categoryDictionary = new Dictionary<string, List<Note>>();
            foreach (string category in categories)
            {
                List<Note> notes = Notes.Where(n => n.Categories.Contains(category)).ToList();
                categoryDictionary.Add(category, notes);
            }

            return categoryDictionary;
        }

        /// <summary>
        /// Adds a note to the collection of notes.
        /// </summary>
        /// <param name="note"> The note to add. </param>
        public void AddNote(Note note)
        {
            Notes.Add(note);
        }

        /// <summary>
        /// Removes a note from the collection of notes.
        /// </summary>
        /// <param name="note"> The note to remove. </param>
        public void RemoveNote(Note note)
        {
            Notes.Remove(note);
        }

        /// <summary>
        /// Finds the index of a note by the title.
        /// </summary>
        /// <param name="title"> The title of the note. </param>
        /// <returns> The index of the note in the list. </returns>
        public int NoteIndex(string title)
        {
            // Get the note by the title
            Note[] notesWithTitle = Notes.Where(n => n.Title == title).ToArray();

            // Return the index of it if it was picked up, otherwise return -1
            return notesWithTitle.Length > 0 ? Notes.IndexOf(notesWithTitle[0]) : -1;
        }
    }
}
