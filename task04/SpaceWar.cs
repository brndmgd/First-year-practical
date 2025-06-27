namespace task04;

public interface ISpaceship
{
    void MoveForward();      // Движение вперед
    void Rotate(int angle);  // Поворот на угол (градусы)
    void Fire();             // Выстрел ракетой
    int Speed { get; }       // Скорость корабля
    int FirePower { get; }   // Мощность выстрела
}

public class Cruiser : ISpaceship
{
    public int Speed { get; }
    public int FirePower { get; }
    public (double X, double Y) Position;
    public int Angle;
    public bool Fired;

    public Cruiser()
    {
        Speed = 50;
        FirePower = 100;
        Position = (0, 0);
        Angle = 0;
        Fired = false;
    }

    public void Fire()
    {
        Fired = true;
    }

    public void MoveForward()
    {
        double radians = Angle * (Math.PI / 180);
        Position.X += Math.Cos(radians) * Speed;
        Position.Y += Math.Sin(radians) * Speed;
    }

    public void Rotate(int angle)
    {
        Angle = (Angle + angle) % 360;
    }
}

public class Fighter : ISpaceship
{
    public int Speed { get; }
    public int FirePower { get; }
    public (double X, double Y) Position;
    public int Angle;
    public bool Fired;

    public Fighter()
    {
        Speed = 100;
        FirePower = 50;
        Position = (0, 0);
        Angle = 0;
        Fired = false;
    }

    public void Fire()
    {
        Fired = true;
    }

    public void MoveForward()
    {
        double radians = Angle * (Math.PI / 180);
        Position.X += Math.Cos(radians) * Speed;
        Position.Y += Math.Sin(radians) * Speed;
    }

    public void Rotate(int angle)
    {
        Angle = Math.Abs(Angle + angle) % 360;
    }
}
