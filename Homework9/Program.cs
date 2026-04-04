using System;
using System.Collections.Generic;

// ================== FACADE ==================

// --- TV ---
class TV
{
    public void On()
    {
        Console.WriteLine("TV is ON");
    }

    public void Off()
    {
        Console.WriteLine("TV is OFF");
    }

    public void SetChannel(string channel)
    {
        Console.WriteLine("TV channel: " + channel);
    }
}

// --- AUDIO ---
class AudioSystem
{
    public void On()
    {
        Console.WriteLine("Audio system ON");
    }

    public void Off()
    {
        Console.WriteLine("Audio system OFF");
    }

    public void SetVolume(int volume)
    {
        Console.WriteLine("Volume set to " + volume);
    }
}

// --- DVD ---
class DVDPlayer
{
    public void Play()
    {
        Console.WriteLine("DVD playing...");
    }

    public void Pause()
    {
        Console.WriteLine("DVD paused");
    }

    public void Stop()
    {
        Console.WriteLine("DVD stopped");
    }
}

// --- GAME CONSOLE ---
class GameConsole
{
    public void On()
    {
        Console.WriteLine("Console ON");
    }

    public void PlayGame(string game)
    {
        Console.WriteLine("Playing game: " + game);
    }
}

// ===== FACADE =====

class HomeTheaterFacade
{
    private TV tv = new TV();
    private AudioSystem audio = new AudioSystem();
    private DVDPlayer dvd = new DVDPlayer();
    private GameConsole console = new GameConsole();

    public void WatchMovie()
    {
        Console.WriteLine("\n--- Watch Movie ---");

        tv.On();
        tv.SetChannel("HDMI");

        audio.On();
        audio.SetVolume(10);

        dvd.Play();
    }

    public void StopMovie()
    {
        Console.WriteLine("\n--- Stop Movie ---");

        dvd.Stop();
        audio.Off();
        tv.Off();
    }

    public void PlayGame(string game)
    {
        Console.WriteLine("\n--- Play Game ---");

        tv.On();
        tv.SetChannel("Game Mode");

        console.On();
        console.PlayGame(game);
    }

    public void ListenMusic()
    {
        Console.WriteLine("\n--- Music ---");

        tv.On();
        tv.SetChannel("Music");

        audio.On();
        audio.SetVolume(7);
    }

    public void SetVolume(int volume)
    {
        audio.SetVolume(volume);
    }
}

// ================== COMPOSITE ==================

abstract class FileSystemComponent
{
    protected string name;

    public FileSystemComponent(string name)
    {
        this.name = name;
    }

    public abstract void Display(int level);
    public abstract int GetSize();
}

// --- FILE ---
class MyFile : FileSystemComponent
{
    private int size;

    public MyFile(string name, int size) : base(name)
    {
        this.size = size;
    }

    public override void Display(int level)
    {
        Console.WriteLine(new string('-', level) + "File: " + name + " (" + size + " KB)");
    }

    public override int GetSize()
    {
        return size;
    }
}

// --- DIRECTORY ---
class Directory : FileSystemComponent
{
    private List<FileSystemComponent> list = new List<FileSystemComponent>();

    public Directory(string name) : base(name) { }

    public void Add(FileSystemComponent comp)
    {
        if (!list.Contains(comp))
        {
            list.Add(comp);
        }
        else
        {
            Console.WriteLine("Already exists: " + name);
        }
    }

    public void Remove(FileSystemComponent comp)
    {
        if (list.Contains(comp))
        {
            list.Remove(comp);
        }
        else
        {
            Console.WriteLine("Not found: " + name);
        }
    }

    public override void Display(int level)
    {
        Console.WriteLine(new string('-', level) + "Folder: " + name);

        for (int i = 0; i < list.Count; i++)
        {
            list[i].Display(level + 2);
        }
    }

    public override int GetSize()
    {
        int total = 0;

        for (int i = 0; i < list.Count; i++)
        {
            total += list[i].GetSize();
        }

        return total;
    }
}

// ================== MAIN ==================

class Program
{
    static void Main()
    {
        // ===== FACADE =====
        HomeTheaterFacade home = new HomeTheaterFacade();

        home.WatchMovie();
        home.SetVolume(15);
        home.StopMovie();

        home.PlayGame("FIFA 25");

        home.ListenMusic();

        // ===== COMPOSITE =====
        Console.WriteLine("\n=== FILE SYSTEM ===");

        Directory root = new Directory("Root");

        MyFile file1 = new MyFile("file1.txt", 10);
        MyFile file2 = new MyFile("file2.txt", 20);

        Directory folder1 = new Directory("Documents");
        Directory folder2 = new Directory("Images");

        MyFile img = new MyFile("photo.jpg", 50);

        folder1.Add(file1);
        folder1.Add(file2);

        folder2.Add(img);

        root.Add(folder1);
        root.Add(folder2);

        root.Display(0);

        Console.WriteLine("Total size: " + root.GetSize() + " KB");

        Console.ReadLine();
    }
}