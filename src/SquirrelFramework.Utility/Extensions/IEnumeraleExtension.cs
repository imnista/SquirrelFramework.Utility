namespace SquirrelFramework.Utility.Extensions
{
    /// <Summary>
    /// Extended the IEnumerable interface
    /// </Summary>
    public static class IEnumerableExtension
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            // Is using Random and OrderBy a good shuffle algorithm?
            // https://stackoverflow.com/questions/1287567/is-using-random-and-orderby-a-good-shuffle-algorithm
            return source.OrderBy(i => Guid.NewGuid());
        }
    }
}