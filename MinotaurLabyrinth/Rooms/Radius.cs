namespace MinotaurLabyrinth
{
    /// <summary>
    /// Represents a radius room that displays all room types within a certain radius.
    /// </summary>
    public class Radius : Room
    {
        static Radius()
        {
            RoomFactory.Instance.Register(RoomType.Radius, () => new Radius());
        }

        /// <inheritdoc/>
        public override RoomType Type { get; } = RoomType.Radius;

        /// <inheritdoc/>
        public override bool IsActive { get; protected set; } = true;

        /// <summary>
        /// Activates the radius room and displays all room types within a radius of 2.
        /// </summary>
        public override void Activate(Hero hero, Map map)
        {
            if (IsActive)
            {
                ConsoleHelper.WriteLine("You have  activated the radius room and sense the room types within a radius of 2:", ConsoleColor.Yellow);

                int heroX = hero.Location.Row;
                int heroY = hero.Location.Column;

                for (int x = heroX - 2; x <= heroX + 2; x++)
                {
                    for (int y = heroY - 2; y <= heroY + 2; y++)
                    {
                        Location location = new Location(x, y);
                        if (map.IsOnMap(location))
                        {
                            Room room = map.GetRoomAtLocation(location);
                            if (room != null && room.Type != RoomType.Radius)
                            {
                                ConsoleHelper.WriteLine($"- {room.Type}", ConsoleColor.Cyan);
                            }
                        }
                    }
                }
            }
        }

        /// <inheritdoc/>
        public override DisplayDetails Display()
        {
            return IsActive ? new DisplayDetails($"[{Type.ToString()[0]}]", ConsoleColor.Yellow)
                            : base.Display();
        }

        /// <summary>
        /// Displays sensory information about the radius room, based on the hero's distance from it.
        /// </summary>
        /// <param name="hero">The hero sensing the radius room.</param>
        /// <param name="heroDistance">The distance between the hero and the radius room.</param>
        /// <returns>Returns true if a message was displayed; otherwise, false.</returns>
        public override bool DisplaySense(Hero hero, int heroDistance)
        {
            if (!IsActive)
            {
                if (base.DisplaySense(hero, heroDistance))
                {
                    return true;
                }
                if (heroDistance == 0)
                {
                    ConsoleHelper.WriteLine("This room used to provide a radius of information, but it seems to have lost its power.", ConsoleColor.DarkGray);
                    return true;
                }
            }
            else if (heroDistance == 1 || heroDistance == 2)
            {
                ConsoleHelper.WriteLine(heroDistance == 1 ? "You sense a radius room nearby." : "Your senses indicate the presence of a radius room within reach.", ConsoleColor.DarkGray);
                return true;
            }
            return false;
        }
    }
}
