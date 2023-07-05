﻿namespace SpaceShip;

public class Pool<T> where T : new()
{

    public Pool(int initialContent)
    {
        pool = new Queue<T>(initialContent);
        for (int i = 0; i < initialContent; i++)
        {
            pool.Enqueue(new T());
        }
    }

    public T GetObject()
    {
        if (pool.Count != 0)
        {
            return pool.Dequeue();
        }
        else
        {
            return new T();
        }
    }

    public void ReturnObject(T obj)
    {
        pool.Enqueue(obj);
    }
}

public class PoolDefence<T> : IDisposable where T : new()
{

    public PoolDefence(Pool<T> pool)
    {
        this.pool = pool;
        obj = pool.GetObject();
    }

    public T Object => obj;

    public void Dispose()
    {
        pool.ReturnObject(obj);
    }
}

class Program
{
    static void Main(string[] args)
    {
        int countObject=40;
        Pool<SpaceShip> spaceShipPool = new Pool<SpaceShip>(countObject);
        using (PoolDefence<SpaceShip> guard = new PoolDefence<SpaceShip>(spaceShipPool))
        {
            SpaceShip spaceShip = guard.Object;
        }
    }
}