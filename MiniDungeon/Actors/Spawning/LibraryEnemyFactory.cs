namespace MiniDungeon.Actors.Spawning;

public class LibraryEnemyFactory : EnemyFactory
{
    public LibraryEnemyFactory()
    {
        Enemies.Add(() => new EnemyEntity("Flying Book", 24, 8, 2));
        Enemies.Add(() => new EnemyEntity("Librarian", 35, 9, 1));
        Enemies.Add(() => new EnemyEntity("Wizard", 16, 7, 4));
    }
}