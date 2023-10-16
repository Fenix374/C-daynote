using System;
using System.Collections.Generic;

namespace DailyPlanner
{
    class Program
    {
        static List<Note> notes = new List<Note>();
        static int currentDateIndex = 0;

        static void Main(string[] args)
        {
            InitializeNotes();

            Console.CursorVisible = false;

            while (true)
            {
                Console.Clear();
                DisplayMenu();
                HandleInput();
            }
        }

        static void InitializeNotes()
        {
            Note note1 = new Note("Много практосов", "На сегодня очень много практососов", new DateTime(2023, 10, 6));
            Note note2 = new Note("Еще больше", "Опять много практосов даже еще больше", new DateTime(2023, 10, 8));
            Note note3 = new Note("Можно я отчислюсь", "Я так отчислюсь я серьезно ", new DateTime(2023, 10, 13));

            notes.Add(note1);
            notes.Add(note2);
            notes.Add(note3);
        }

        static void DisplayMenu()
        {
            Console.WriteLine("Ежедневник\n");

            DateTime currentDate = notes[currentDateIndex].Date;
            Console.WriteLine($"Дата: {currentDate.ToShortDateString()}\n");

            int noteNumber = 1;
            foreach (Note note in notes)
            {
                string noteTitle = note.Title;
                if (noteNumber - 1 == currentDateIndex)
                {
                    noteTitle = $"=> {noteTitle} <=";
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                Console.WriteLine(noteTitle);
                Console.ResetColor();
                noteNumber++;
            }

            Console.WriteLine("\nИспользуйте стрелки Влево и Вправо для переключения между датами.");
            Console.WriteLine("Нажмите Enter для просмотра подробной информации о заметке.");
            Console.WriteLine("Нажмите A, чтобы добавить новую заметку.");
        }

        static void HandleInput()
        {
            ConsoleKeyInfo key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    MoveToPreviousDate();
                    break;
                case ConsoleKey.DownArrow:
                    MoveToNextDate();
                    break;
                case ConsoleKey.Enter:
                    DisplayNoteDetails();
                    break;
                case ConsoleKey.A:
                    AddNote();
                    break;
            }
        }

        static void MoveToPreviousDate()
        {
            currentDateIndex--;
            if (currentDateIndex < 0)
                currentDateIndex = notes.Count - 1;
        }

        static void MoveToNextDate()
        {
            currentDateIndex++;
            if (currentDateIndex >= notes.Count)
                currentDateIndex = 0;
        }

        static void DisplayNoteDetails()
        {
            Console.Clear();
            Note selectedNote = notes[currentDateIndex];
            Console.WriteLine($"Название: {selectedNote.Title}");
            Console.WriteLine($"Дата: {selectedNote.Date.ToShortDateString()}");
            Console.WriteLine($"Описание: {selectedNote.Description}");
            Console.WriteLine("Нажмите любую клавишу для продолжения.");
            Console.ReadKey();
        }

        static void AddNote()
        {
            Console.Clear();
            Console.WriteLine("Введите название заметки:");
            string title = Console.ReadLine();

            Console.WriteLine("Введите описание заметки:");
            string description = Console.ReadLine();

            Console.WriteLine("Введите дату заметки (гггг-мм-дд):");
            DateTime date = DateTime.Parse(Console.ReadLine());

            Note newNote = new Note(title, description, date);
            notes.Add(newNote);

            Console.WriteLine("Заметка успешно добавлена! Нажмите любую клавишу для продолжения.");
            Console.ReadKey();
        }
    }

    class Note
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public Note(string title, string description, DateTime date)
        {
            Title = title;
            Description = description;
            Date = date;
        }
    }
}
