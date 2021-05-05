using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Venue_Management_System.Models
{
    public class DistinctItemComparer : IEqualityComparer<GroupMember>
    {
        public bool Equals(GroupMember x, GroupMember y)
        {
            return x.GroupId == y.GroupId &&
                   x.UserId == y.UserId;
        }

        public int GetHashCode(GroupMember obj)
        {
            return obj.GroupId.GetHashCode() ^
                   obj.UserId.GetHashCode();
        }
    }

    public class DistinctIVenueComparer : IEqualityComparer<BookVenue>
    {
        public bool Equals(BookVenue x, BookVenue y)
        {
            return x.VenueId == y.VenueId &&
                  x.UserId == y.UserId;
        }

        public int GetHashCode(BookVenue obj)
        {
            return obj.VenueId.GetHashCode() ^
                   obj.UserId.GetHashCode();
        }
    }
}