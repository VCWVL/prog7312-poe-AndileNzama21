using PROG_POE.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PROG_POE.Service
{

    public class EventService : IEventService
    {
        private readonly SortedDictionary<DateTime, List<Event>> _eventsByDate;
        private readonly Dictionary<string, List<Event>> _eventsByCategory;
        private readonly HashSet<string> _uniqueCategories;
        private readonly HashSet<DateTime> _uniqueDates;
        private readonly Stack<Event> _recentEvents;
        private readonly Queue<Event> _eventNotifications;
        private readonly SortedDictionary<int, List<Event>> _featuredEvents;
        private readonly Dictionary<string, UserSearch> _userSearches;
        private readonly Dictionary<string, UserProfile> _userProfiles;
        private readonly Dictionary<string, List<UserInteraction>> _userInteractions;
        private readonly Dictionary<string, double[]> _eventVectors;
        private readonly string _dataFilePath = "events_data.dat";

        public EventService()
        {
            _eventsByDate = new SortedDictionary<DateTime, List<Event>>();
            _eventsByCategory = new Dictionary<string, List<Event>>();
            _uniqueCategories = new HashSet<string>();
            _uniqueDates = new HashSet<DateTime>();
            _recentEvents = new Stack<Event>();
            _eventNotifications = new Queue<Event>();
            _featuredEvents = new SortedDictionary<int, List<Event>>(new DescendingComparer());
            _userSearches = new Dictionary<string, UserSearch>();
            _userProfiles = new Dictionary<string, UserProfile>();
            _userInteractions = new Dictionary<string, List<UserInteraction>>();
            _eventVectors = new Dictionary<string, double[]>();

            InitializeSampleData();
            LoadData();
        }

        private void InitializeSampleData()
        {
            var sampleEvents = new List<Event>
            {
                new Event {
                    Title = "Community Clean-up Day",
                    Description = "Join your neighbors for a community-wide clean-up event.",
                    Category = "Community",
                    EventDate = DateTime.Now.AddDays(7),
                    Location = "City Park",
                    Organizer = "Municipal Environmental Dept",
                    AttendeeCount = 150,
                    IsFeatured = true,
                    Tags = new List<string> { "environment", "volunteer", "community" }
                },
                new Event {
                    Title = "Budget Planning Meeting",
                    Description = "Public meeting to discuss the municipal budget.",
                    Category = "Government",
                    EventDate = DateTime.Now.AddDays(3),
                    Location = "Town Hall",
                    Organizer = "Municipal Finance Dept",
                    AttendeeCount = 75,
                    IsFeatured = true,
                    Tags = new List<string> { "government", "finance", "planning" }
                },
                new Event {
                    Title = "Summer Festival",
                    Description = "Annual summer festival with food, music, and activities.",
                    Category = "Entertainment",
                    EventDate = DateTime.Now.AddDays(14),
                    Location = "Main Street",
                    Organizer = "Cultural Affairs Committee",
                    AttendeeCount = 500,
                    IsFeatured = false,
                    Tags = new List<string> { "festival", "music", "food", "family" }
                }
            };

            foreach (var eventItem in sampleEvents)
            {
                AddEvent(eventItem);
            }
            InitializeEventVectors();
        }

        private void InitializeEventVectors()
        {
            var allEvents = GetAllEvents();
            var allTags = allEvents.SelectMany(e => e.Tags).Distinct().ToList();

            foreach (var eventItem in allEvents)
            {
                var vector = new double[allTags.Count];
                for (int i = 0; i < allTags.Count; i++)
                {
                    vector[i] = eventItem.Tags.Contains(allTags[i]) ? 1.0 : 0.0;
                }
                _eventVectors[eventItem.Id] = vector;
            }
        }

        public void AddEvent(Event eventItem)
        {
            if (!_eventsByDate.ContainsKey(eventItem.EventDate.Date))
                _eventsByDate[eventItem.EventDate.Date] = new List<Event>();
            _eventsByDate[eventItem.EventDate.Date].Add(eventItem);

            if (!_eventsByCategory.ContainsKey(eventItem.Category))
                _eventsByCategory[eventItem.Category] = new List<Event>();
            _eventsByCategory[eventItem.Category].Add(eventItem);

            _uniqueCategories.Add(eventItem.Category);
            _uniqueDates.Add(eventItem.EventDate.Date);
            _recentEvents.Push(eventItem);

            if (eventItem.EventDate >= DateTime.Now)
                _eventNotifications.Enqueue(eventItem);

            if (eventItem.IsFeatured)
            {
                int priority = eventItem.AttendeeCount;
                if (!_featuredEvents.ContainsKey(priority))
                    _featuredEvents[priority] = new List<Event>();
                _featuredEvents[priority].Add(eventItem);
            }
        }

        public List<Event> GetAllEvents()
        {
            var allEvents = new List<Event>();
            foreach (var dateEvents in _eventsByDate.Values)
                allEvents.AddRange(dateEvents);
            return allEvents.OrderBy(e => e.EventDate).ToList();
        }

        public List<Event> SearchEvents(string searchTerm, string category, DateTime? date)
        {
            RecordUserSearch(searchTerm, category);
            var results = new List<Event>();

            if (!string.IsNullOrEmpty(category) && _eventsByCategory.ContainsKey(category))
                results.AddRange(_eventsByCategory[category]);
            else
                results = GetAllEvents();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                string searchTermLower = searchTerm.ToLower();
                results = results.Where(e =>
                    e.Title.ToLower().Contains(searchTermLower) ||
                    e.Description.ToLower().Contains(searchTermLower) ||
                    e.Location.ToLower().Contains(searchTermLower)
                ).ToList();
            }

            if (date.HasValue)
                results = results.Where(e => e.EventDate.Date == date.Value.Date).ToList();

            return results.OrderBy(e => e.EventDate).ToList();
        }

        public List<Event> GetRecommendedEvents(string currentSearchTerm, string currentCategory)
        {
            try
            {
                var recommendations = new List<Event>();
                var allEvents = GetAllEvents();

                var contentBasedRecs = GetContentBasedRecommendations(currentSearchTerm, currentCategory, allEvents);
                recommendations.AddRange(contentBasedRecs);

                var collaborativeRecs = GetCollaborativeRecommendations("current_user");
                recommendations.AddRange(collaborativeRecs);

                if (recommendations.Count < 3)
                {
                    var popularRecs = allEvents
                        .Where(e => e.IsFeatured || e.AttendeeCount > 100)
                        .OrderByDescending(e => e.AttendeeCount)
                        .Take(5 - recommendations.Count)
                        .ToList();
                    recommendations.AddRange(popularRecs);
                }

                return recommendations
                    .GroupBy(e => e.Id)
                    .Select(g => g.First())
                    .Take(5)
                    .ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Recommendation error: {ex.Message}");
                return GetFeaturedEvents().Take(3).ToList();
            }
        }

        private List<Event> GetContentBasedRecommendations(string searchTerm, string category, List<Event> allEvents)
        {
            if (string.IsNullOrEmpty(searchTerm) && string.IsNullOrEmpty(category))
                return new List<Event>();

            var recommendations = new List<Event>();
            var searchVector = CreateSearchVector(searchTerm, category);

            foreach (var eventItem in allEvents)
            {
                if (_eventVectors.ContainsKey(eventItem.Id))
                {
                    var eventVector = _eventVectors[eventItem.Id];
                    var similarity = CalculateCosineSimilarity(searchVector, eventVector);

                    if (similarity > 0.3)
                        recommendations.Add(eventItem);
                }
            }

            return recommendations.OrderByDescending(e => e.AttendeeCount).Take(3).ToList();
        }

        public List<Event> GetCollaborativeRecommendations(string userId)
        {
            try
            {
                if (!_userInteractions.ContainsKey(userId) || _userInteractions[userId].Count < 3)
                    return new List<Event>();

                var userInteractions = _userInteractions[userId];
                var similarUsers = FindSimilarUsers(userId);
                var allEvents = GetAllEvents();

                var recommendations = new List<Event>();
                foreach (var similarUser in similarUsers)
                {
                    var theirLikedEvents = _userInteractions[similarUser.Key]
                        .Where(i => i.InteractionType == InteractionType.Register || i.InteractionType == InteractionType.Click)
                        .Select(i => i.EventId)
                        .Distinct();

                    foreach (var eventId in theirLikedEvents)
                    {
                        var eventItem = allEvents.FirstOrDefault(e => e.Id == eventId);
                        if (eventItem != null && !userInteractions.Any(i => i.EventId == eventId))
                            recommendations.Add(eventItem);
                    }
                }

                return recommendations
                    .GroupBy(e => e.Id)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.First())
                    .Take(2)
                    .ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Collaborative filtering error: {ex.Message}");
                return new List<Event>();
            }
        }

        private Dictionary<string, double> FindSimilarUsers(string userId)
        {
            var similarities = new Dictionary<string, double>();
            var userInteractions = _userInteractions[userId];

            foreach (var otherUser in _userInteractions.Keys)
            {
                if (otherUser != userId)
                {
                    var otherInteractions = _userInteractions[otherUser];
                    var similarity = CalculateUserSimilarity(userInteractions, otherInteractions);
                    if (similarity > 0.5)
                        similarities[otherUser] = similarity;
                }
            }

            return similarities
                .OrderByDescending(kv => kv.Value)
                .Take(3)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        private double CalculateCosineSimilarity(double[] vector1, double[] vector2)
        {
            if (vector1.Length != vector2.Length) return 0;

            double dotProduct = 0, magnitude1 = 0, magnitude2 = 0;

            for (int i = 0; i < vector1.Length; i++)
            {
                dotProduct += vector1[i] * vector2[i];
                magnitude1 += Math.Pow(vector1[i], 2);
                magnitude2 += Math.Pow(vector2[i], 2);
            }

            magnitude1 = Math.Sqrt(magnitude1);
            magnitude2 = Math.Sqrt(magnitude2);

            return (magnitude1 == 0 || magnitude2 == 0) ? 0 : dotProduct / (magnitude1 * magnitude2);
        }

        private double CalculateUserSimilarity(List<UserInteraction> interactions1, List<UserInteraction> interactions2)
        {
            var commonEvents = interactions1.Select(i => i.EventId)
                .Intersect(interactions2.Select(i => i.EventId))
                .ToList();

            if (!commonEvents.Any()) return 0;

            double similarity = 0;
            foreach (var eventId in commonEvents)
            {
                var interaction1 = interactions1.First(i => i.EventId == eventId);
                var interaction2 = interactions2.First(i => i.EventId == eventId);

                double weight1 = GetInteractionWeight(interaction1.InteractionType);
                double weight2 = GetInteractionWeight(interaction2.InteractionType);

                similarity += Math.Min(weight1, weight2);
            }

            return similarity / commonEvents.Count;
        }

        private double GetInteractionWeight(InteractionType type)
        {
            switch (type)
            {
                case InteractionType.View:
                    return 0.3;
                case InteractionType.Click:
                    return 0.6;
                case InteractionType.Register:
                    return 1.0;
                case InteractionType.Share:
                    return 0.8;
                default:
                    return 0.1;
            }
        }

        private double[] CreateSearchVector(string searchTerm, string category)
        {
            var allEvents = GetAllEvents();
            var allTags = allEvents.SelectMany(e => e.Tags).Distinct().ToList();
            var vector = new double[allTags.Count];

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var searchWords = searchTerm.ToLower().Split(' ');
                for (int i = 0; i < allTags.Count; i++)
                {
                    if (searchWords.Any(word => allTags[i].Contains(word)))
                        vector[i] = 1.0;
                }
            }

            if (!string.IsNullOrEmpty(category))
            {
                for (int i = 0; i < allTags.Count; i++)
                {
                    if (allTags[i].Contains(category.ToLower()))
                        vector[i] = Math.Max(vector[i], 0.8);
                }
            }

            return vector;
        }

        public List<string> GetUniqueCategories() => _uniqueCategories.OrderBy(c => c).ToList();
        public List<DateTime> GetUniqueDates() => _uniqueDates.OrderBy(d => d).ToList();

        public void RecordUserSearch(string searchTerm, string category)
        {
            if (string.IsNullOrEmpty(searchTerm) && string.IsNullOrEmpty(category)) return;

            string key = $"{searchTerm}_{category}";

            if (_userSearches.ContainsKey(key))
            {
                _userSearches[key].SearchCount++;
                _userSearches[key].SearchDate = DateTime.Now;
            }
            else
            {
                _userSearches[key] = new UserSearch
                {
                    SearchTerm = searchTerm,
                    Category = category,
                    SearchDate = DateTime.Now,
                    SearchCount = 1
                };
            }
        }

        public void RecordUserInteraction(string userId, string eventId, InteractionType interaction)
        {
            try
            {
                if (!_userInteractions.ContainsKey(userId))
                    _userInteractions[userId] = new List<UserInteraction>();

                _userInteractions[userId].Add(new UserInteraction
                {
                    UserId = userId,
                    EventId = eventId,
                    InteractionType = interaction,
                    Timestamp = DateTime.Now
                });

                if (!_userProfiles.ContainsKey(userId))
                    _userProfiles[userId] = new UserProfile { UserId = userId };

                SaveData();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Record interaction error: {ex.Message}");
            }
        }

        public void SaveData()
        {
            try
            {
                var data = new EventServiceData
                {
                    Events = GetAllEvents(),
                    UserSearches = _userSearches,
                    UserProfiles = _userProfiles,
                    UserInteractions = _userInteractions
                };

                using (var stream = File.Open(_dataFilePath, FileMode.Create))
                {
                    new BinaryFormatter().Serialize(stream, data);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Save data error: {ex.Message}");
            }
        }

        public void LoadData()
        {
            try
            {
                if (File.Exists(_dataFilePath))
                {
                    using (var stream = File.Open(_dataFilePath, FileMode.Open))
                    {
                        var data = (EventServiceData)new BinaryFormatter().Deserialize(stream);

                        foreach (var eventItem in data.Events)
                            AddEvent(eventItem);

                        foreach (var kvp in data.UserSearches)
                            _userSearches[kvp.Key] = kvp.Value;

                        foreach (var kvp in data.UserProfiles)
                            _userProfiles[kvp.Key] = kvp.Value;

                        foreach (var kvp in data.UserInteractions)
                            _userInteractions[kvp.Key] = kvp.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Load data error: {ex.Message}");
            }
        }

        public List<Event> GetFeaturedEvents()
        {
            var featured = new List<Event>();
            foreach (var priorityList in _featuredEvents.Values)
                featured.AddRange(priorityList);
            return featured.Take(5).ToList();
        }
    }

    public class DescendingComparer : IComparer<int>
    {
        public int Compare(int x, int y) => y.CompareTo(x);
    }
}

