using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ShowTrackerService" in code, svc and config file together.
public class ShowTrackerService : IShowTrackerService
{
    ShowTrackerEntities db = new ShowTrackerEntities();
    public List<string> GetArtists()
    {
        var artistquery = from a in db.Artists
                    orderby a.ArtistName
                    select new { a.ArtistName };
        List<string> artistList = new List<string>();
        foreach (var a in artistquery)
        {
            artistList.Add(a.ArtistName.ToString());
        }
        return artistList;
    }

    public List<ShowInfo> GetShowByArtist(string artist)
    {
        var showByArtistQuery = from s in db.ShowDetails
                               where s.Artist.ArtistName.Equals(artist)
                               orderby s.Show.ShowName
                               select new
                               {
                                   s.Show.ShowName,
                                   s.Show.ShowDate,
                                   s.Show.ShowTime,
                                   s.Show.Venue.VenueName
                               };
        List<ShowInfo> info = new List<ShowInfo>();

        foreach (var s in showByArtistQuery)
        {
            ShowInfo si = new ShowInfo();
            si.ShowName = s.ShowName;
            si.ShowDate = s.ShowDate.ToString();
            si.ShowTime = s.ShowTime.ToString();
            si.VenueName = s.VenueName;

            info.Add(si);
        }
        return info;
    }

    public List<ShowInfo> GetShowByVenue(string venue)
    {
        var showByVenueQuery = from s in db.Shows
                               where s.Venue.VenueName.Equals(venue)
                               orderby s.ShowName
                               select new {
                                   s.ShowName,
                                   s.ShowDate,
                                   s.ShowTime
                               };
        List<ShowInfo> info = new List<ShowInfo>();

        foreach (var s in showByVenueQuery)
        {
            ShowInfo si = new ShowInfo();
            si.ShowName = s.ShowName;
            si.ShowDate = s.ShowDate.ToString();
            si.ShowTime = s.ShowTime.ToString();
           
            info.Add(si);
        }
        return info;
    }

    public List<string> GetShows()
    {
        var showquery = from s in db.Shows
                          orderby s.ShowName
                          select new { s.ShowName };
        List<string> showList = new List<string>();
        foreach (var s in showquery)
        {
            showList.Add(s.ShowName.ToString());
        }
        return showList;
    }

    public List<string> GetVenues()
    {
        var venuequery = from v in db.Venues
                        orderby v.VenueName
                        select new { v.VenueName };
        List<string> venueList = new List<string>();
        foreach (var v in venuequery)
        {
            venueList.Add(v.VenueName.ToString());
        }
        return venueList;
    }
}
