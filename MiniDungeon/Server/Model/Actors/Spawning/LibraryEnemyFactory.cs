using MiniDungeon.Server.Model.Actors.Enemies.Behaviors;

namespace MiniDungeon.Server.Model.Actors.Spawning;

public class LibraryEnemyFactory : EnemyFactory
{
    public LibraryEnemyFactory()
    {
        AddSpecies("Flying Book", 24, 8, 2, new CowardlyBehavior());
        AddSpecies("Librarian", 35, 9, 1, new CowardlyBehavior());
        AddSpecies("Wizard", 16, 7, 4, new VengefulBehavior());
    }
}