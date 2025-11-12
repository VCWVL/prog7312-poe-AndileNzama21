using PROG_POE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_POE.Service
{

    public interface IEventService
    {
        void AddEvent(Event eventItem);
        List<Event> GetAllEvents();
        List<Event> SearchEvents(string searchTerm, string category, DateTime? date);
        List<Event> GetRecommendedEvents(string currentSearchTerm, string currentCategory);
        List<Event> GetCollaborativeRecommendations(string userId);
        List<string> GetUniqueCategories();
        List<DateTime> GetUniqueDates();
        void RecordUserSearch(string searchTerm, string category);
        void RecordUserInteraction(string userId, string eventId, InteractionType interaction);
        void SaveData();
        void LoadData();
    }
}
