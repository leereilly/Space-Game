using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.

namespace Completed

{

    public class BoardManager : MonoBehaviour
    {
        // Using Serializable allows us to embed a class with sub properties in the inspector.
        [Serializable]
        public class Count
        {
            public int Minimum;             //Minimum value for our Count class.
            public int Maximum;             //Maximum value for our Count class.


            //Assignment constructor.
            public Count(int min, int max)
            {
                Minimum = min;
                Maximum = max;
            }
        }


        public int Columns = 8;                                         //Number of columns in our game board.
        public int Rows = 8;                                            //Number of rows in our game board.
        public Count WallCount = new Count(5, 9);                      //Lower and upper limit for our random number of walls per level.
        public Count ObjectCount = new Count(1, 5);                      //Lower and upper limit for our random number of objects items per level.
        public GameObject Exit;                                         //Prefab to spawn for exit.
        public GameObject[] FloorTiles;                                 //Array of floor prefabs.
        public GameObject[] WallTiles;                                  //Array of wall prefabs.
        public GameObject[] ObjectTiles;                                  //Array of Object prefabs.
        public GameObject[] EnemyTiles;                                 //Array of enemy prefabs.
        public GameObject[] OuterWallTiles;                             //Array of outer tile prefabs.

        private Transform BoardHolder;                                  //A variable to store a reference to the transform of our Board object.
        private List<Vector3> gridPositions = new List<Vector3>();   //A list of possible locations to place tiles.


        //Clears our list gridPositions and prepares it to generate a new board.
        void InitialiseList()
        {
            //Clear our list gridPositions.
            gridPositions.Clear();

            //Loop through x axis (columns).
            for (int x = 1; x < Columns - 1; x++)
            {
                //Within each column, loop through y axis (rows).
                for (int y = 1; y < Rows - 1; y++)
                {
                    //At each index add a new Vector3 to our list with the x and y coordinates of that position.
                    gridPositions.Add(new Vector3(x, y, 0f));
                }
            }
        }


        //Sets up the outer walls and floor (background) of the game board.
        void BoardSetup()
        {
            //Instantiate Board and set boardHolder to its transform.
            BoardHolder = new GameObject("Board").transform;

            //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
            for (int x = -1; x < Columns + 1; x++)
            {
                //Loop along y axis, starting from -1 to place floor or outerwall tiles.
                for (int y = -1; y < Rows + 1; y++)
                {
                    //Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
                    GameObject toInstantiate = FloorTiles[Random.Range(0, FloorTiles.Length)];

                    //Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
                    if (x == -1 || x == Columns || y == -1 || y == Rows)
                        toInstantiate = OuterWallTiles[Random.Range(0, OuterWallTiles.Length)];

                    //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                    GameObject instance =
                        Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                    //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                    instance.transform.SetParent(BoardHolder);
                }
            }
        }


        //RandomPosition returns a random position from our list gridPositions.
        Vector3 RandomPosition()
        {
            //Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
            int randomIndex = Random.Range(0, gridPositions.Count);

            //Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
            Vector3 randomPosition = gridPositions[randomIndex];

            //Remove the entry at randomIndex from the list so that it can't be re-used.
            gridPositions.RemoveAt(randomIndex);

            //Return the randomly selected Vector3 position.
            return randomPosition;
        }


        //LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
        void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
        {
            //Choose a random number of objects to instantiate within the minimum and maximum limits
            int objectCount = Random.Range(minimum, maximum + 1);

            //Instantiate objects until the randomly chosen limit objectCount is reached
            for (int i = 0; i < objectCount; i++)
            {
                //Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
                Vector3 randomPosition = RandomPosition();

                //Choose a random tile from tileArray and assign it to tileChoice
                GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];

                //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
                Instantiate(tileChoice, randomPosition, Quaternion.identity);
            }
        }


        //SetupScene initializes our level and calls the previous functions to lay out the game board
        public void SetupScene(int Level)
        {
            //Creates the outer walls and floor.
            BoardSetup();

            //Reset our list of gridpositions.
            InitialiseList();

            //Instantiate a random number of wall tiles based on minimum and maximum, at randomized positions.
            LayoutObjectAtRandom(WallTiles, WallCount.Minimum, WallCount.Maximum);

            //Instantiate a random number of food tiles based on minimum and maximum, at randomized positions.
            LayoutObjectAtRandom(ObjectTiles, ObjectCount.Minimum, ObjectCount.Maximum);

            //Determine number of enemies based on current level number, based on a logarithmic progression
            int EnemyCount = (int)Mathf.Log(Level, 2f);

            //Instantiate a random number of enemies based on minimum and maximum, at randomized positions.
            LayoutObjectAtRandom(EnemyTiles, EnemyCount, EnemyCount);

            //Instantiate the exit tile in the upper right hand corner of our game board
            Instantiate(Exit, new Vector3(Columns - 1, Rows - 1, 0f), Quaternion.identity);
        }
    }
}