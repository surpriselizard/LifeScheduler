using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ReminderProjectWPF
{
    public class ReminderDbManager
    {
        private string _path = "";

        public ReminderDbManager()
        {
            var list = new List<string>(Directory.GetCurrentDirectory().Split('\\'));

            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i] == "ReminderProjectWPF")
                {
                    list.Add("ReminderFile.txt");
                    break;
                }
                else if (list[i] != "ReminderProjectWPF")
                    list.Remove(list[i]);
            }

            _path = string.Join(@"\", list);


            var reminders = new List<string>();
            var readStream = File.Open(_path, FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader reader;
            reader = new StreamReader(readStream);
            readStream.Dispose();

            foreach (var line in File.ReadAllLines(_path))
                reminders.Add(line);

            for (int i = 0; i < reminders.Count; ++i)
                if (string.IsNullOrWhiteSpace(reminders[i]))
                    reminders.Remove(reminders[i]);

            foreach (var line in reminders)
            {
                var values = line.Split(new string[] { "*||*" }, StringSplitOptions.None);
                var tempRem = new Reminder(Convert.ToDateTime(values[1]), values[0]);
                tempRem.ReminderDesc = values[2];
                ReminderDatabase.Add(tempRem);
            }

            reader.Close();
        }

        public List<Reminder> ReminderDatabase { get; private set; } = new List<Reminder>();

        public void AddReminder(Reminder reminder)
        {
            ReminderDatabase.Add(reminder);

            var tempEnumerable = File.ReadLines(_path);
            var tempLines = tempEnumerable.ToList();
            tempLines.Add($"{reminder.ReminderName}*||*{reminder.DateDue}*||*{reminder.ReminderDesc}\n");

            var writeStream = File.Open(_path, FileMode.Open, FileAccess.Write, FileShare.None);
            var writer = new StreamWriter(writeStream);

            foreach (var line in tempLines)
                writer.WriteLine(line);

            writer.Close();
        }

        public void DeleteReminder(Reminder reminder)
        {
            var writeStream = File.Open(_path, FileMode.Open, FileAccess.Write, FileShare.None);
            var writer = new StreamWriter(writeStream);
            writer.Dispose();
            writeStream.Dispose();

            var tempFile = Path.GetTempFileName();
            var linesToKeep = File.ReadLines(_path).Where(l => l != $"{reminder.ReminderName}*||*{reminder.DateDue}*||*{reminder.ReminderDesc}");

            File.WriteAllLines(tempFile, linesToKeep);

            File.Delete(_path);
            File.Move(tempFile, _path);

            writer.Close();
        }

        public List<List<Reminder>> RemindersIn7Days()
        {
            var RemsInWeek = new List<List<Reminder>>(7);
            for (int i = 0; i < RemsInWeek.Capacity; ++i)
            {
                RemsInWeek.Add(new List<Reminder>());
            }

            foreach (var reminder in ReminderDatabase)
            {
                for (int i = 0; i < 7; ++i)
                {
                    if (reminder.DateDue.Date == DateTime.Today.AddDays(i))
                    {
                        RemsInWeek[i].Add(reminder);
                    }
                }
            }

            return RemsInWeek;
        }

        public List<List<Reminder>> RemInMonth()
        {
            var NumOfDaysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var RemsInMonth = new List<List<Reminder>>(NumOfDaysInMonth);
            var DaysInMonth = new List<DateTime>(NumOfDaysInMonth);

            for (int i = 0; i < NumOfDaysInMonth; ++i)
                RemsInMonth.Add(new List<Reminder>());
            for (int i = 0; i < NumOfDaysInMonth; ++i)
                DaysInMonth.Add(new DateTime(DateTime.Now.Year, DateTime.Now.Month, i + 1));

            foreach (var reminder in ReminderDatabase)
            {
                for (int i = 0; i < NumOfDaysInMonth; ++i)
                    if (reminder.DateDue.Date == DaysInMonth[i].Date)
                        RemsInMonth[i].Add(reminder);
            }

            return RemsInMonth;
        }
    }
}
