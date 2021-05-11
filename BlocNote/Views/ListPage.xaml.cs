using BlocNote.Model;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlocNote.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        List<Note> Notes = new List<Note>();

        string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "notes.json");
        public ListPage()
        {
            InitializeComponent();
            // si on clique sur une note on l'affiche
            listNotes.ItemSelected += (sender, e) =>
            {
                Note note = listNotes.SelectedItem as Note;
                Navigation.PushAsync(new NotePage(note));
            };
        }
        void NewNote_Click(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NotePage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Charge les Notes
            Notes = Helper.LoadNotes(_fileName);
            
            // Mise à jour de la liste
            // inverse la liste pour avoir la dernière note en première position dans la liste.
            if(Notes != null)
                Notes.Reverse();
            listNotes.ItemsSource = Notes;
        }
    }
}