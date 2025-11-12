using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_POE.Model
{
    using System;
    using System.Collections.Generic;

    
        [Serializable]
        public class Event
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            public DateTime EventDate { get; set; }
            public string Location { get; set; }
            public string Organizer { get; set; }
            public int AttendeeCount { get; set; }
            public bool IsFeatured { get; set; }
            public List<string> Tags { get; set; } = new List<string>();

            public Event()
            {
                Id = Guid.NewGuid().ToString();
                AttendeeCount = 0;
            }

            public override string ToString()
            {
                return $"{Title} - {EventDate.ToShortDateString()} at {Location}";
            }
        }
    }


