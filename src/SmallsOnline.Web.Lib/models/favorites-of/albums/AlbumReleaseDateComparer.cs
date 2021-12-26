namespace SmallsOnline.Web.Lib.Models.FavoritesOf.Albums;

public class AlbumReleaseDateComparer : IComparer<AlbumData>
{
    public int Compare(AlbumData album1, AlbumData album2)
    {
        return DateTimeOffset.Compare(album1.ReleaseDate.Value, album2.ReleaseDate.Value);
    }
}