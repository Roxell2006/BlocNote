using BlocNote.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlocNote.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotePage : ContentPage
    {
        List<Note> Notes = new List<Note>();
        Note note = null;

        string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "notes.json");
        public NotePage()
        {
            InitializeComponent();
            // Charge les Notes
            Notes = Helper.LoadNotes(_fileName);
            EraseButton.IsVisible = false;
        }
        // surcharge de l'initiation de la page pour afficher une note existante
        public NotePage(Note note)
        {
            InitializeComponent();
            // Charge les Notes
            Notes = Helper.LoadNotes(_fileName);
            // récuppère la note sélectionné et l'affiche
            this.note = note;
            Titre.Text = note.Titre;
            Contenu.Text = note.Contenu;
            EraseButton.IsVisible = true;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (!string.IsNullOrWhiteSpace(Titre.Text))
            {
                // vérifie si c'est une nouvelle note
                if(note == null)
                {
                    // Crée un Id 
                    Guid guid = Guid.NewGuid();
                    string key = guid.ToString();
                    // Enregistre la nouvelle note
                    Notes.Add(new Note { Id = key, Titre = Titre.Text, Contenu = Contenu.Text });
                    Helper.SaveNotes(_fileName, Notes);
                }
                else
                {
                    // si c'est une note qu'on modifie:
                    // on efface l'ancienne note, et on enregistre la nouvelle avec l'id existant
                    Note eraseNote = Notes.Where(n => n.Id == note.Id).FirstOrDefault();
                    Notes.Remove(eraseNote);
                    Notes.Add(new Note { Id = note.Id, Titre = Titre.Text, Contenu = Contenu.Text });
                    Helper.SaveNotes(_fileName, Notes);
                }       
            }              
        }
        void EraseButton_Click(object sender, EventArgs e)
        {
            // efface la note courante
            Note eraseNote = Notes.Where(n => n.Id == note.Id).FirstOrDefault();
            Notes.Remove(eraseNote);
            Helper.SaveNotes(_fileName, Notes);
            Titre.Text = ""; // empêche de réenregistrer la note
            Navigation.PopAsync();
        }
    }
}