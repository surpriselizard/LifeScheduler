using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderProjectWPF
{
    public class Reminder
    {
        public string ReminderName { get; set; }
        public string ReminderDesc { get; set; }
        public DateTime DateDue { get; set; }

        public Reminder(DateTime dueDate, string name)
        {
            ReminderName = name;
            DateDue = dueDate;
        }
    }
}
