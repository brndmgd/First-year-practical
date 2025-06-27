using Xunit;
using task04;

public class SpaceshipTests
{
    [Fact]
    public void Cruiser_ShouldHaveCorrectStats()
    {
        ISpaceship cruiser = new Cruiser();
        Assert.Equal(50, cruiser.Speed);
        Assert.Equal(100, cruiser.FirePower);
    }

    [Fact]
    public void Fighter_ShouldBeFasterThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.Speed > cruiser.Speed);
    }

    [Fact]
    public void Fighter_ShouldBeWeakerThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.FirePower < cruiser.FirePower);
    }

    [Fact]
    public void Rotate_SetsCorrectAngle()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        fighter.Rotate(30);
        cruiser.Rotate(60);
        Assert.Equal(30, fighter.Angle);
        Assert.Equal(60, cruiser.Angle);
    }

    [Fact]
    public void Move_SetsCorrectCoordinates()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        fighter.Rotate(30);
        cruiser.Rotate(60);
        fighter.MoveForward();
        cruiser.MoveForward();
        (double x, double y) fighterDifference = (Math.Abs(fighter.Position.X - 100 * Math.Sqrt(3) / 2), Math.Abs(fighter.Position.Y - 50));
        (double x, double y) cruiserDifference = (Math.Abs(cruiser.Position.X - 25), Math.Abs(cruiser.Position.Y - 50 * Math.Sqrt(3) / 2));
        Assert.True(fighterDifference.x <= Math.Pow(10, -13) && fighterDifference.y <= Math.Pow(10, -13));
        Assert.True(cruiserDifference.x <= Math.Pow(10, -13) && cruiserDifference.y <= Math.Pow(10, -13));
    }

    [Fact]
    public void Fire_SetsCorrectBool()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        fighter.Fire();
        cruiser.Fire();
        Assert.True(fighter.Fired);
        Assert.True(cruiser.Fired);
    }
}
