using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Game2
{
    class Controller
    {
        private Block[] block;
        private BasePlayer[] player;
        private Marker[] marker;
        private Crawler[] crawler;
        private FlyEnemy[] flyenemy;
        private Toppler[] toppler;
        private Jelly[] jelly;
        private NextLevel[] nextlevel;
        private Death[] death;
        private Component[] cols;
        private Terrain[] terrain;
        private Foreground[] foreground;
        private Camera cam;
        private int typesOfObj; //The number of different object types
        private int[] noOfObj;
        private int typesOfTerr;
        private int[] noOfTerr;
        private int typesOfFore;
        private int[] noOfFore;
        private int total_obj;
        private int total_terr;
        private int total_fore;
        private int level;
        private int maxLevels;

        public Controller()
        {
            String filename;
            maxLevels = 0;
            while (true)
            {
                try
                {
                    filename = "./../../level/level" + maxLevels + ".txt";
                    StreamReader sr = new StreamReader(filename);
                    maxLevels++;
                    filename = "";
                }
                catch (FileNotFoundException)
                {
                    break;
                }
            }
            level = 0;
            typesOfObj = 9;
            typesOfTerr = typesOfFore = 2;
            for (int i = 0; i < maxLevels; i++)
            {
                filename = "./../../level/level" + i + ".txt";
                createFromFile(filename);
                filename = "./../../level/level" + i + "terr.txt";
                createTerrainFromFile(filename);
                filename = "./../../level/level" + i + "fore.txt";
                createForegroundFromFile(filename);
            }
            //Create the instances based on the data
            createInstances();
            createTerrain();
            createForeground();
        }
        public void createFromFile(String filename)
        {
            //Start the variables
            String input;
            String temp = "";
            int pos = 0;
            if (noOfObj == null)
            {
                noOfObj = new int[typesOfObj];
                for (int i = 0; i < typesOfObj; i++)
                    noOfObj[i] = 0;
            }
            int[] objCount = new int[typesOfObj];
            for (int i = 0; i < typesOfObj; i++)
                objCount[i] = 0;

            //Read the whole stream to an input file
            StreamReader sr = new StreamReader(filename);
            input = sr.ReadToEnd();
            //While there is still input...
            while (pos < input.Length)
            {
                //Read the input
                while (pos < input.Length && input[pos] > 32)
                {
                    temp += input[pos];
                    pos++;
                }
                //Convert the input to the object type
                if (Convert.ToInt16(temp) > 0 && Convert.ToInt16(temp) <= objCount.Length)
                    objCount[Convert.ToInt16(temp) - 1]++;
                //Clear temp, and advance position.
                temp = "";
                pos++;
                //Check to see if there is a new line, and ignore it.
                while (pos < input.Length && (input[pos] < 32))
                {
                    pos++;
                }
            }
            //Find the max amount of objects
            for (int i = 0; i < typesOfObj; i++)
            {
                if (noOfObj[i] < objCount[i]) noOfObj[i] = objCount[i];
            }

        }
        public void createTerrainFromFile(String filename)
        {
            //Start the variables
            String input;
            String temp = "";
            int pos = 0;
            if (noOfTerr == null)
            {
                noOfTerr = new int[typesOfTerr];
                for (int i = 0; i < typesOfTerr; i++)
                    noOfTerr[i] = 0;
            }
            int terrCount = 0;

            //Read the whole stream to an input file
            StreamReader sr = new StreamReader(filename);
            input = sr.ReadToEnd();
            //While there is still input...
            while (pos < input.Length)
            {
                //Read the input
                while (pos < input.Length && input[pos] > 32)
                {
                    temp += input[pos];
                    pos++;
                }
                //Convert the input to the object type
                if (Convert.ToInt16(temp) > 0)
                    terrCount++;
                //Clear temp, and advance position.
                temp = "";
                pos++;
                //Check to see if there is a new line, and ignore it.
                while (pos < input.Length && (input[pos] < 32))
                {
                    pos++;
                }
            }
            //Find the max amount of objects
            if (terrCount > total_terr) total_terr = terrCount;
        }
        public void createForegroundFromFile(String filename)
        {
            //Start the variables
            String input;
            String temp = "";
            int pos = 0;
            if (noOfFore == null)
            {
                noOfFore = new int[typesOfFore];
                for (int i = 0; i < typesOfFore; i++)
                    noOfFore[i] = 0;
            }
            int foreCount = 0;
            //Read the whole stream to an input file
            StreamReader sr = new StreamReader(filename);
            input = sr.ReadToEnd();
            //While there is still input...
            while (pos < input.Length)
            {
                //Read the input
                while (pos < input.Length && input[pos] > 32)
                {
                    temp += input[pos];
                    pos++;
                }
                //Convert the input to the object type
                if (Convert.ToInt16(temp) > 0)
                    foreCount++;
                //Clear temp, and advance position.
                temp = "";
                pos++;
                //Check to see if there is a new line, and ignore it.
                while (pos < input.Length && (input[pos] < 32))
                {
                    pos++;
                }
            }
            //Find the max amount of objects
            if (total_fore < foreCount) total_fore = foreCount;
        }
        public void createInstances()
        {
            //Count up all of the objects
            foreach (int n in noOfObj)
                total_obj += n;
            int c = 0;
            block = new Block[noOfObj[c++]];
            player = new BasePlayer[noOfObj[c++]];
            marker = new Marker[noOfObj[c++]];
            crawler = new Crawler[noOfObj[c++]];
            flyenemy = new FlyEnemy[noOfObj[c++]];
            toppler = new Toppler[noOfObj[c++]];
            jelly = new Jelly[noOfObj[c++]];
            nextlevel = new NextLevel[noOfObj[c++]];
            death = new Death[noOfObj[c++]];

            cols = new Component[total_obj];
            cam = new Camera();
            cam.init(640, 480);

            //Set ids
            int id_gen = 0;
            c = 0;
            for (int i = 0; i < noOfObj[c]; i++)
                block[i] = new Block(id_gen++); c++;
            for (int i = 0; i < noOfObj[c]; i++)
                player[i] = new BasePlayer(id_gen++); c++;
            for (int i = 0; i < noOfObj[c]; i++)
                marker[i] = new Marker(id_gen++); c++;
            for (int i = 0; i < noOfObj[c]; i++)
                crawler[i] = new Crawler(id_gen++); c++;
            for (int i = 0; i < noOfObj[c]; i++)
                flyenemy[i] = new FlyEnemy(id_gen++); c++;
            for (int i = 0; i < noOfObj[c]; i++)
                toppler[i] = new Toppler(id_gen++); c++;
            for (int i = 0; i < noOfObj[c]; i++)
                jelly[i] = new Jelly(id_gen++); c++;
            for (int i = 0; i < noOfObj[c]; i++)
                nextlevel[i] = new NextLevel(id_gen++); c++;
            for (int i = 0; i < noOfObj[c]; i++)
                death[i] = new Death(id_gen++); c++;

        }
        public void createTerrain()
        {
            terrain = new Terrain[total_terr];
            for (int i = 0; i < total_terr; i++)
                terrain[i] = new Terrain();
        }
        public void createForeground()
        {
            foreground = new Foreground[total_fore];
            for (int i = 0; i < total_fore; i++)
                foreground[i] = new Foreground();
        }
        public void init()
        {
            //Set the file to the file destination
            String filename = "./../../level/level" + Convert.ToString(level) + ".txt";
            //Start the variables
            String input;
            String temp = "";
            int pos = 0;
            int x = 0, y = 0;
            int[] objCount = new int[typesOfObj];
            for (int i = 0; i < typesOfObj; i++)
                objCount[i] = 0;
            //Initialise each object
            if (level == 0)
            {
                int c = 0;
                for (int i = 0; i < noOfObj[c]; i++)
                    block[i].init(-100, -100, 16, 16); c++;
                for (int i = 0; i < noOfObj[c]; i++)
                    player[i].init(-100, -100, 16, 16); c++;
                for (int i = 0; i < noOfObj[c]; i++)
                    marker[i].init(-100, -100, 16, 16); c++;
                for (int i = 0; i < noOfObj[c]; i++)
                    crawler[i].init(-100, -100, 16, 16); c++;
                for (int i = 0; i < noOfObj[c]; i++)
                    flyenemy[i].init(-100, -100, 16, 16); c++;
                for (int i = 0; i < noOfObj[c]; i++)
                    toppler[i].init(-100, -100, 16, 16); c++;
                for (int i = 0; i < noOfObj[c]; i++)
                    jelly[i].init(-100, -100, 16, 16); c++;
                for (int i = 0; i < noOfObj[c]; i++)
                    nextlevel[i].init(-100, -100, 16, 16); c++;
                for (int i = 0; i < noOfObj[c]; i++)
                    death[i].init(-100, -100, 16, 16); c++;
            }
            else
            {
                int c = 0;
                for (int i = 0; i < noOfObj[c]; i++)
                    block[i].reInit(-100, -100); c++;
                for (int i = 0; i < noOfObj[c]; i++)
                    player[i].reInit(-100, -100); c++;
                for (int i = 0; i < noOfObj[c]; i++)
                    marker[i].reInit(-100, -100); c++;
                for (int i = 0; i < noOfObj[c]; i++)
                    crawler[i].reInit(-100, -100); c++;
                for (int i = 0; i < noOfObj[c]; i++)
                    flyenemy[i].reInit(-100, -100); c++;
                for (int i = 0; i < noOfObj[c]; i++)
                    toppler[i].reInit(-100, -100); c++;
                for (int i = 0; i < noOfObj[c]; i++)
                    jelly[i].reInit(-100, -100); c++;
                for (int i = 0; i < noOfObj[c]; i++)
                    nextlevel[i].reInit(-100, -100); c++;
                for (int i = 0; i < noOfObj[c]; i++)
                    death[i].reInit(-100, -100); c++;
            }
            //Read the whole stream to an input file
            StreamReader sr = new StreamReader(filename);
            input = sr.ReadToEnd();
            //While there is still input...
            while (pos < input.Length)
            {
                //Read the input
                while (pos < input.Length && input[pos] > 32)
                {
                    temp += input[pos];
                    pos++;
                }
                //Check if it contains 1, thus is a block
                if (Convert.ToInt16(temp) == 1)
                {
                    block[objCount[Convert.ToInt16(temp) - 1]].reInit(x * 16, y * 16);
                    block[objCount[Convert.ToInt16(temp) - 1]++].Level = level;
                }
                //Check if it contains 2, thus is a player
                if (Convert.ToInt16(temp) == 2)
                {
                    player[objCount[Convert.ToInt16(temp) - 1]].reInit(x * 16, y * 16);
                    player[objCount[Convert.ToInt16(temp) - 1]++].Level = level;
                }
                //Check if it contains 3, thus is a marker
                if (Convert.ToInt16(temp) == 3)
                {
                    marker[objCount[Convert.ToInt16(temp) - 1]].reInit(x * 16, y * 16);
                    marker[objCount[Convert.ToInt16(temp) - 1]++].Level = level;
                }
                //Check if it contains 4, thus is a crawler
                if (Convert.ToInt16(temp) == 4)
                {
                    crawler[objCount[Convert.ToInt16(temp) - 1]].reInit(x * 16, y * 16);
                    crawler[objCount[Convert.ToInt16(temp) - 1]++].Level = level;
                }
                //Check if it contains 5, thus is a fly enemy
                if (Convert.ToInt16(temp) == 5)
                {
                    flyenemy[objCount[Convert.ToInt16(temp) - 1]].reInit(x * 16, y * 16);
                    flyenemy[objCount[Convert.ToInt16(temp) - 1]++].Level = level;
                }
                //Check if it contains 6, thus is a toppler
                if (Convert.ToInt16(temp) == 6)
                {
                    toppler[objCount[Convert.ToInt16(temp) - 1]].reInit(x * 16, y * 16);
                    toppler[objCount[Convert.ToInt16(temp) - 1]++].Level = level;
                }
                //Check if it contains 7, thus is a jelly
                if (Convert.ToInt16(temp) == 7)
                {
                    jelly[objCount[Convert.ToInt16(temp) - 1]].reInit(x * 16, y * 16);
                    jelly[objCount[Convert.ToInt16(temp) - 1]++].Level = level;
                }
                //Check if it contains 8, thus is a level exit
                if (Convert.ToInt16(temp) == 8)
                {
                    nextlevel[objCount[Convert.ToInt16(temp) - 1]].reInit(x * 16, y * 16);
                    nextlevel[objCount[Convert.ToInt16(temp) - 1]++].Level = level;
                }
                //Check if it contains 9, thus is a death
                if (Convert.ToInt16(temp) == 9)
                {
                    death[objCount[Convert.ToInt16(temp) - 1]].reInit(x * 16, y * 16);
                    death[objCount[Convert.ToInt16(temp) - 1]++].Level = level;
                }
                //Clear temp, and advance position.
                temp = "";
                pos++;
                x++;
                //Check to see if there is a new line, and advance y.
                while (pos < input.Length && (input[pos] < 32))
                {
                    x = 0;
                    y++;
                    pos++;
                }
            }
            initWorlds();
            initTerrain();
            initForeground();
        }
        public void initTerrain()
        {
            //Set the file to the file destination
            String filename = "./../../level/level" + Convert.ToString(level) + "terr.txt";
            //Start the variables
            String input;
            String temp = "";
            int pos = 0;
            int x = 0, y = 0;
            int terrCount = 0;
            //Initialise each terrain
            foreach (Terrain t in terrain)
                t.init(-100, -100, 0);
            //Read the whole stream to an input file
            StreamReader sr = new StreamReader(filename);
            input = sr.ReadToEnd();
            //While there is still input...
            while (pos < input.Length)
            {
                //Read the input
                while (pos < input.Length && input[pos] > 32)
                {
                    temp += input[pos];
                    pos++;
                }
                //Convert
                if (Convert.ToInt16(temp) > 0)
                {
                    terrain[terrCount++].init(x * 16, y * 16, Convert.ToInt16(temp) - 1);
                }

                //Clear temp, and advance position.
                temp = "";
                pos++;
                x++;
                //Check to see if there is a new line, and advance y.
                while (pos < input.Length && (input[pos] < 32))
                {
                    x = 0;
                    y++;
                    pos++;
                }
            }
        }
        public void initForeground()
        {
            //Set the file to the file destination
            String filename = "./../../level/level" + Convert.ToString(level) + "fore.txt";
            //Start the variables
            String input;
            String temp = "";
            int pos = 0;
            int x = 0, y = 0;
            int foreCount = 0;
            //Initialise each terrain
            foreach (Foreground f in foreground)
                f.init(-100, -100, 0);
            //Read the whole stream to an input file
            StreamReader sr = new StreamReader(filename);
            input = sr.ReadToEnd();
            //While there is still input...
            while (pos < input.Length)
            {
                //Read the input
                while (pos < input.Length && input[pos] > 32)
                {
                    temp += input[pos];
                    pos++;
                }
                //Convert
                if (Convert.ToInt16(temp) > 0)
                {
                    foreground[foreCount++].init(x * 16, y * 16, Convert.ToInt16(temp) - 1);
                }

                //Clear temp, and advance position.
                temp = "";
                pos++;
                x++;
                //Check to see if there is a new line, and advance y.
                while (pos < input.Length && (input[pos] < 32))
                {
                    x = 0;
                    y++;
                    pos++;
                }
            }
        }
        public void initWorlds()
        {
            //Get all of the components
            int c = 0;
            foreach (Block b in block)
                cols[c++] = b.Comp;
            foreach (BasePlayer p in player)
                cols[c++] = p.Comp;
            foreach (Marker m in marker)
                cols[c++] = m.Comp;
            foreach (Crawler cr in crawler)
                cols[c++] = cr.Comp;
            foreach (FlyEnemy fe in flyenemy)
                cols[c++] = fe.Comp;
            foreach (Toppler t in toppler)
                cols[c++] = t.Comp;
            foreach (Jelly j in jelly)
                cols[c++] = j.Comp;
            foreach (NextLevel nl in nextlevel)
                cols[c++] = nl.Comp;
            foreach (Death d in death)
                cols[c++] = d.Comp;

            //Initialise all of the worlds.
            foreach (BasePlayer p in player)
                p.initWorld(cols);
            foreach (Crawler cr in crawler)
                cr.initWorld(cols);
            foreach (FlyEnemy fe in flyenemy)
                fe.initWorld(cols);
            foreach (Toppler t in toppler)
                t.initWorld(cols);
            foreach (Jelly j in jelly)
                j.initWorld(cols);
        }
        public void setSprite(Texture2D sprite, Texture2D tileset)
        {
            foreach (Block b in block)
                b.Sprite = sprite;
            foreach (BasePlayer p in player)
                p.Sprite = sprite;
            foreach (Crawler cr in crawler)
                cr.Sprite = sprite;
            foreach (FlyEnemy fe in flyenemy)
                fe.Sprite = sprite;
            foreach (Toppler t in toppler)
                t.Sprite = sprite;
            foreach (Jelly j in jelly)
                j.Sprite = sprite;
            foreach (Terrain ter in terrain)
                ter.Sprite = tileset;
            foreach (Foreground f in foreground)
                f.Sprite = tileset;
        }
        public void setClip()
        {
            foreach (Block b in block)
                b.setClips(0, 16, 1);
            foreach (BasePlayer p in player)
                p.setClips(0, 0, 7);
            foreach (Crawler cr in crawler)
                cr.setClips(0, 32, 4);
            foreach (FlyEnemy fe in flyenemy)
                fe.setClips(0, 48, 4);
            foreach (Toppler t in toppler)
                t.setClips(0, 64, 4);
            foreach (Jelly j in jelly)
                j.setClips(0, 80, 5);
        }
        public void handleInput(KeyboardState keyboard)
        {
            foreach (BasePlayer p in player)
                p.handleInput(keyboard);
        }
        public void update(Controller cont)
        {
            //Get all of the components
            int c = 0;
            foreach (Block b in block)
                cols[c++] = b.Comp;
            foreach (BasePlayer p in player)
                cols[c++] = p.Comp;
            foreach (Marker m in marker)
                cols[c++] = m.Comp;
            foreach (Crawler cr in crawler)
                cols[c++] = cr.Comp;
            foreach (FlyEnemy fe in flyenemy)
                cols[c++] = fe.Comp;
            foreach (Toppler t in toppler)
                cols[c++] = t.Comp;
            foreach (Jelly j in jelly)
                cols[c++] = j.Comp;
            foreach (NextLevel nl in nextlevel)
                cols[c++] = nl.Comp;
            foreach (Death d in death)
                cols[c++] = d.Comp;

            //Initialise all of the worlds.
            foreach (BasePlayer p in player)
                p.initWorld(cols);
            foreach (Crawler cr in crawler)
                cr.initWorld(cols);
            foreach (FlyEnemy fe in flyenemy)
                fe.initWorld(cols);
            foreach (Toppler t in toppler)
                t.initWorld(cols);
            foreach (Jelly j in jelly)
                j.initWorld(cols);
            //Camera
            foreach (Block b in block)
                b.Cam = cam;
            foreach (BasePlayer p in player)
                p.Cam = cam;
            foreach (Marker m in marker)
                m.Cam = cam;
            foreach (Crawler cr in crawler)
                cr.Cam = cam;
            foreach (FlyEnemy fe in flyenemy)
                fe.Cam = cam;
            foreach (Toppler t in toppler)
                t.Cam = cam;
            foreach (Jelly j in jelly)
                j.Cam = cam;
            foreach (NextLevel nl in nextlevel)
                nl.Cam = cam;
            foreach (Death d in death)
                d.Cam = cam;
            foreach (Terrain t in terrain)
                t.Cam = cam;
            foreach (Foreground fg in foreground)
                fg.Cam = cam;
            //Make camera follow player
            Component toFollow = null;
            foreach (BasePlayer p in player)
                if (p.Level == level) { toFollow = p.Comp; break; }
            cam.centreAround(toFollow, 3);
            //Update each object
            foreach (BasePlayer p in player)
            {
                p.update();
                p.updateWorld(cols);
            }
            foreach (Crawler cr in crawler)
            {
                cr.update();
                cr.updateWorld(cols);
            }
            foreach (FlyEnemy fe in flyenemy)
            {
                fe.update();
                fe.updateWorld(cols);
            }
            foreach (Toppler t in toppler)
            {
                t.update();
                t.updateWorld(cols);
            }
            foreach (Jelly j in jelly)
            {
                j.update();
                j.updateWorld(cols);
            }
            //Next level
            if (level != toFollow.Level)
            {
                try
                {
                    level = toFollow.Level;
                    init();
                }
                catch (FileNotFoundException)
                {
                    level = 0;
                    init();
                }
            }
        }
        public void draw(SpriteBatch sb)
        {
            //Foreground
            foreach (Foreground f in foreground)
                f.draw(sb);
            foreach (Block b in block)
                b.draw(sb);
            foreach (BasePlayer p in player)
                p.draw(sb);
            foreach (Crawler cr in crawler)
                cr.draw(sb);
            foreach (FlyEnemy fe in flyenemy)
                fe.draw(sb);
            foreach (Toppler t in toppler)
                t.draw(sb);
            foreach (Jelly j in jelly)
                j.draw(sb);
            //Terrain
            foreach (Terrain ter in terrain)
                ter.draw(sb);
        }
    }
}
