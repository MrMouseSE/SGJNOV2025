namespace TileScript
{
    public static class TileModelFactory
    {
        public static TileModelNormalModel Create(TileContainer container)
        {
            return container.Mode switch
            {
                TileMode.Normal => new TileModelNormalModel(container)
            };
        }
    }
}