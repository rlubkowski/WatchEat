using System;
using System.Collections.Generic;
using System.Text;
using WatchEat.Models.Database;

namespace WatchEat.ViewModels.EventSelection
{
    public class JournalEntryEditViewModel : BaseViewModel
    {
        public JournalEntryEditViewModel(JournalEntry entry)
        {
            Entry = entry;
        }

        public JournalEntry Entry { get; private set; }
    }
}
