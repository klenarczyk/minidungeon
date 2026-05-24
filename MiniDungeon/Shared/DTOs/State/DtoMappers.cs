using MiniDungeon.Server.Model.Actors;
using MiniDungeon.Server.Model.Loot;
using MiniDungeon.Server.Model.Loot.Items;
using MiniDungeon.Server.Model.Loot.Items.Weapons;
using MiniDungeon.Server.Model.World;

namespace MiniDungeon.Shared.DTOs.State;

public static class DtoMappers
{
    public static PositionDto ToDto(this Position position) => new PositionDto(position.X, position.Y);

    public static ItemDto ToDto(this IItem item, int x = -1, int y = -1, bool isTwoHanded = false)
        => new ItemDto(x, y, item.Name, isTwoHanded);

    public static ItemDto ToDto(this IWeaponItem weapon, int x = -1, int y = -1)
        => new ItemDto(x, y, weapon.Name, weapon.IsTwoHanded);
    
    public static ActorDto ToDto(this IEntity entity)
        => new ActorDto(entity.Position.X, entity.Position.Y, entity.Name);
    
    public static ActorDto ToActorDto(this Player player)
        => new ActorDto(player.Position.X, player.Position.Y, player.Name);

    public static AttributesDto ToAttributesDto(this Player player)
        => new AttributesDto(player.Health, player.MaxHealth, player.Strength, player.Defense,
            player.Intelligence, player.Dexterity, player.Luck);

    public static EquipmentDto ToDto(this Equipment equipment)
    {
        var left = equipment[EquipmentSlot.LeftHand];
        var right = equipment[EquipmentSlot.RightHand];
        var isTwoHanded = left != null && left == right;

        return new EquipmentDto(
            left?.ToDto(isTwoHanded: isTwoHanded),
            right?.ToDto(isTwoHanded: isTwoHanded));
    }

    public static InventoryDto ToDto(this Inventory inventory)
    {
        List<ItemDto> itemDtos = [];
        
        foreach (var item in inventory.Items)
        {
            itemDtos.Add(item.ToDto());
        }

        return new InventoryDto(itemDtos, itemDtos.Count, inventory.Capacity);
    }

    public static PurseDto ToDto(this Purse purse)
        => new PurseDto(purse.Gold, purse.Coins);

    public static PlayerDto ToDto(this Player player)
        => new PlayerDto(
            player.Id,
            player.Position.X,
            player.Position.Y,
            player.ToAttributesDto(),
            player.Inventory.ToDto(),
            player.Equipment.ToDto(),
            player.Purse.ToDto(),
            player.IsBattling);
    
    // ---
    
    public static List<PositionDto> ToWallsDto(this Board board)
    {
        List<PositionDto> walls = [];
        
        for (var x = 0; x < Board.Columns; x++)
        for (var y = 0; y < Board.Rows; y++)
        {
            if (board[x, y].Type != CellType.Wall) continue;
            
            walls.Add(board[x, y].Position.ToDto());
        }
        
        return walls;
    }

    public static List<ItemDto> ToItemsDto(this Board board)
    {
        List<ItemDto> items = [];
        
        for (var x = 0; x < Board.Columns; x++)
        for (var y = 0; y < Board.Rows; y++)
        {
            foreach (var item in board[x, y].Items)
            {
                items.Add(item.ToDto(x, y));
            }
        }

        return items;
    }

    public static List<ActorDto> ToDto(this List<IEntity> entities)
    {
        List<ActorDto> entitiesDto = [];

        foreach (var entity in entities)
        {
            entitiesDto.Add(entity.ToDto());
        }
        
        return entitiesDto;
    }

    public static Dictionary<int, ActorDto> ToDto(this Dictionary<int, Player> players)
    {
        Dictionary<int, ActorDto> playersDto = [];

        foreach (var (id, player) in players)
        {
            playersDto.Add(id, player.ToActorDto());
        }
        
        return playersDto;
    }
}