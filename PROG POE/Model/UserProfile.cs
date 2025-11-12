using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_POE.Model
{
    
    [Serializable]
    public class UserProfile
    {
        public string UserId { get; set; }
        public List<string> PreferredCategories { get; set; } = new List<string>();
        public List<string> PreferredTags { get; set; } = new List<string>();
        public DateTime LastActive { get; set; } = DateTime.Now;
    }

    [Serializable]
    public class UserInteraction
    {
        public string UserId { get; set; }
        public string EventId { get; set; }
        public InteractionType InteractionType { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public enum InteractionType
    {
        View,
        Click,
        Register,
        Share
    }

    [Serializable]
    public class EventServiceData
    {
        public List<Event> Events { get; set; }
        public Dictionary<string, UserSearch> UserSearches { get; set; }
        public Dictionary<string, UserProfile> UserProfiles { get; set; }
        public Dictionary<string, List<UserInteraction>> UserInteractions { get; set; }
    }

}
