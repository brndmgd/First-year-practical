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

    

    public Cruiser()
    {
        Speed = 50;
        FirePower = 100;
    }

    public void Fire()
    {
        throw new NotImplementedException();
    }

    public void MoveForward()
    {
        throw new NotImplementedException();
    }

    public void Rotate(int angle)
    {
        throw new NotImplementedException();
    }
}

public class Fighter : ISpaceship
{
    public int Speed { get; }

    public int FirePower { get; }

    public Fighter()
    {
        Speed = 100;
        FirePower = 50;
    }

    public void Fire()
    {
        throw new NotImplementedException();
    }

    public void MoveForward()
    {
        throw new NotImplementedException();
    }

    public void Rotate(int angle)
    {
        throw new NotImplementedException();
    }
}