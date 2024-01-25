using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PruebaHerencia
{
    // Enums
    public enum PlayerState
    {
        waiting,
        running,
        corner,
        jumping,
        falling

    }

    public enum GameState
    {
        waiting,
        playing,
        over
    }

    public enum Facing
    {
        left,
        right
    }

    // Structs

    // Constants

    public class Constants
    {
        public static int gameMaxObjects = 1000;
        public static int gameInitialWait = 5;
    }

    // Classes

    public class GameScreen
    {
        public char[,] characters;

        public GameScreen(int width, int height)
        {
            characters = new char[width, height];
        }

        public void Clear()
        {
            for(int i = 0; i < characters.GetLength(0); i ++)
            {
                for (int j = 0; j < characters.GetLength(1); j++)
                {
                    characters[i, j] = ' ';
                }

            }
        }

        public void Draw(int x, int y, char c)
        {
            if(x < 0 || x > characters.GetLength(0) - 1) { return; }
            if(y < 0 || y > characters.GetLength(1) - 1) { return; }

            characters[x, y] = c;

        }

        public void Render()
        {
            Console.Clear();
            for (int i = 0; i < characters.GetLength(0); i++)
            {
                for (int j = 0; j < characters.GetLength(1); j++)
                {
                    Console.Write(characters[i, j]);
                }

                Console.WriteLine();

            }

        }
    }

    public class GameManager
    {
        int waitTimer;
        GameState state;
        GameState nextState;

        GameObject[] gameObjects;

        Player player;

        public GameManager()
        {
            state = GameState.waiting;
            waitTimer = Constants.gameInitialWait;

            gameObjects = new GameObject[Constants.gameMaxObjects];

        }

        public void Update()
        {
            if(state == GameState.waiting)
            {
                waitTimer -= 1;

                if(waitTimer <= 0)
                {
                    nextState = GameState.playing;
                }
            }

            if(state != nextState)
            {
                if(nextState == GameState.playing)
                {
                    
                }
            }

            for(int i = 0; i < gameObjects.Length; i ++)
            {
                gameObjects[i].Update();

            }
        }

        public bool IsGameOver()
        {
            return state == GameState.over;
        }

        public bool HasObject(GameObject _object)
        {
            bool result = false;

            for (int i = 0; i < gameObjects.Length && !result; i++)
            {
                if (gameObjects[i] == _object) { result = true; }
            }

            return result;

        }

        public bool AddObject(GameObject _object)
        {
            bool added = false;

            for (int i = 0; i < gameObjects.Length && !added; i++)
            {
                if (gameObjects[i] == null) { gameObjects[i] = _object; added = true; }
            }

            return added;
        }

        public bool RemoveObject(GameObject _object)
        {
            bool removed = false;

            for (int i = 0; i < gameObjects.Length && !removed; i++)
            {
                if (gameObjects[i] == _object) { gameObjects[i] = null; removed = true; }
            }

            return removed;
        }

    }

    public class GameObject
    {
        protected GameManager manager;

        public GameObject(GameManager _manager)
        {
            manager = _manager;
        }

        public virtual void Update()
        {

        }

        public virtual void Render(GameScreen screen)
        {

        }

    }


    public class Player : GameObject
    {
        PlayerState state;
        PlayerState nextState;

        bool onWaitOver;

        public Player(GameObject _manager) : GameObject(_manager)
        {
            state = PlayerState.waiting;
        }

        public override void Update()
        {
            if(state == PlayerState.waiting)
            {
                if(onWaitOver)
                {
                    nextState = PlayerState.running
                }
            }

            if(state != nextState)
            {
                state = nextState;
            }

        }

        public override void Render(GameScreen screen)
        {
            if(state == PlayerState.running) { }
        }

        public void OnWaitOver()
        {
            onWaitOver = true;

            Update();
        }

        

    }

    public class Persona
    {
        protected string nombre;
        protected int edad;
        protected string dni;

        public Persona(string _nombre, int _edad, string _dni)
        {
            Console.WriteLine("-> Constructor de Persona");
            nombre = _nombre;
            edad = _edad;
            dni = _dni;
        }

        public void PonNombre(string n)
        {
            Console.WriteLine("-> Pon nombre de Persona");
            nombre = n;
        }

        virtual public void Imprime()
        {
            Console.WriteLine("-> Imprime de Persona");
            Console.WriteLine("Nombre " + nombre);
            Console.WriteLine("Edad " + edad);
            Console.WriteLine("Dni " + dni);
        }
    }

    public class Empleado : Persona
    {
        int sueldo;

        public Empleado(string _nombre, int _edad, string _dni, int _sueldo):base(_nombre, _edad, _dni)
        {
            Console.WriteLine("-> Constructor de Empleado");
            sueldo = _sueldo;
        }

        public void IngresaNomina()
        {
            Console.WriteLine("-> Ingresa nomina de Empleado");
            Console.WriteLine("Ingresa " + sueldo + " euros");
        }

        override public void Imprime()
        {
            Console.WriteLine("-> Imprime de Empleado");
            Console.WriteLine("Nombre " + nombre);
            Console.WriteLine("Edad " + edad);
            Console.WriteLine("Dni " + dni);
            Console.WriteLine("Sueldo " + sueldo);
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Persona p = new Persona("Pepe", 30, "467");

            p.Imprime();
            p.PonNombre("Juan");
            p.Imprime();


            Empleado e = new Empleado("Ana", 40, "589", 600);

            e.Imprime();
            e.PonNombre("Maria");
            e.IngresaNomina();
            e.Imprime();

            Persona p2 = e;

            p2.Imprime();
            p2.PonNombre("Marta");
            p2.Imprime();

            p2 = p;

            p2.Imprime();
            p2.PonNombre("Antonio");
            p2.Imprime();

            Persona[] personas = new Persona[5];

            personas[0] = new Persona("Julia", 20, "564");
            personas[1] = new Persona("Antonio", 40, "554");
            personas[2] = new Empleado("Martin", 70, "254", 900);
            personas[3] = new Persona("Paco", 20, "234");
            personas[4] = new Empleado("Anabel", 30, "256", 1200);

            for(int i = 0; i < personas.Length; i ++)
            {
                personas[i].Imprime();
            }



            Console.ReadLine();
        }
    }
}
